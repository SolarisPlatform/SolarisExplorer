using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Solaris.BlockExplorer.DataAccess.Entities.Create;
using Solaris.BlockExplorer.DataAccess.Repositories;
using Solaris.BlockExplorer.Domain.Factories;
using Solaris.BlockExplorer.Domain.Mappings;
using Solaris.BlockExplorer.Domain.Services.Rpc;

namespace Solaris.BlockExplorer.Indexer
{
    public class Program
    {
        private static ServiceProvider _serviceProvider;
        private const string GenesisBlock = "0000000000000000000000000000000000000000000000000000000000000000";
        private static void ConfigureServiceProvider()
        {
            var mapperConfiguration = new MapperConfiguration(configurationExpression =>
            {
                configurationExpression.AddProfile(new DomainObjectsProfile());
            });
            var serviceCollection = new ServiceCollection();
            serviceCollection
            .AddSingleton(provider => mapperConfiguration.CreateMapper())
                .AddSingleton<IConfiguration>(p => new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build())
                .AddSingleton(typeof(IWalletRpcService<>), typeof(WalletRpcService<>))
                .AddSingleton<IRpcBlockCountService, RpcBlockCountService>()
                .AddSingleton<IRpcBlockHashService, RpcBlockHashService>()
                .AddSingleton<IRpcBlockService, RpcBlockService>()
                .AddSingleton<IRpcTransactionService, RpcTransactionService>()
                .AddSingleton<IDbConnectionFactory>(provider =>
                {
                    var configuration = provider.GetService<IConfiguration>();
                    return new DbConnectionFactory
                    { ConnectionString = configuration.GetConnectionString("SolarisExplorerDatabase") };
                })
                .AddScoped(provider =>
                {
                    var dbConnectionFactory = provider.GetService<IDbConnectionFactory>();
                    return dbConnectionFactory.CreateConnection();
                })
                .AddSingleton<IBlockRepository, BlockRepository>()
                .AddSingleton<ITransactionRepository, TransactionRepository>()
                .AddSingleton<ITransactionOutputRepository, TransactionOutputRepository>()
                .AddSingleton<IOutputScriptPublicKeyRepository, OutputScriptPublicKeyRepository>()
                .AddSingleton<IOutputScriptPublicKeyAddressRepository, OutputScriptPublicKeyAddressRepository>()
                .AddSingleton<ITransactionInputRepository, TransactionInputRepository>()
                .AddSingleton<IInputScriptSignatureRepository, InputScriptSignatureRepository>();


            serviceCollection.AddHttpClient("Daemon", (provider, client) =>
            {
                var configuration = provider.GetService<IConfiguration>();
                var section = configuration.GetSection("Daemon");
                var username = section.GetValue<string>("Username");
                var password = section.GetValue<string>("Password");
                var url = section.GetValue<string>("Url");

                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}")));
                client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.5");
            });
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public static async Task Main(string[] args)
        {
            ConfigureServiceProvider();

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var rpcBlockCountService = _serviceProvider.GetService<IRpcBlockCountService>();
            var rpcBlockService = _serviceProvider.GetService<IRpcBlockService>();
            var rpcTransactionService = _serviceProvider.GetService<IRpcTransactionService>();
            var rpcBlockCount = await rpcBlockCountService.GetBlockCount();

            var blockRepository = _serviceProvider.GetService<IBlockRepository>();
            var transactionRepository = _serviceProvider.GetService<ITransactionRepository>();
            var transactionOutputRepository = _serviceProvider.GetService<ITransactionOutputRepository>();
            var outputScriptPublicKeyRepository = _serviceProvider.GetService<IOutputScriptPublicKeyRepository>();
            var outputScriptPublicKeyAddressRepository = _serviceProvider.GetService<IOutputScriptPublicKeyAddressRepository>();
            var transactionInputRepository = _serviceProvider.GetService<ITransactionInputRepository>();
            var inputScriptSignatureRepository = _serviceProvider.GetService<IInputScriptSignatureRepository>();

            var blockCount = await blockRepository.GetBlockHeight();

            if (rpcBlockCount == blockCount && rpcBlockCount > 0)
                Console.WriteLine("No update required");

            for (var i = blockCount; i < rpcBlockCount; i++)
            {
                var rpcBlock = await rpcBlockService.GetBlock(i);

                var block = new Block
                {
                    Id = rpcBlock.Hash,
                    Bits = rpcBlock.Bits,
                    Chainwork = rpcBlock.Chainwork,
                    Difficulty = rpcBlock.Difficulty,
                    Height = rpcBlock.Height,
                    MedianTime = rpcBlock.MedianTime,
                    Time = rpcBlock.Time,
                    Merkleroot = rpcBlock.Merkleroot,
                    Nonce = rpcBlock.Nonce,
                    Size = rpcBlock.Size,
                    Version = rpcBlock.Version,
                    Weight = rpcBlock.Weight,
                    PreviousBlock = rpcBlock.PreviousBlock == GenesisBlock ? null : rpcBlock.PreviousBlock
                };

                await blockRepository.Insert(block);

                var blockOrder = 0L;
                foreach (var rpcBlockTransaction in rpcBlock.Transactions)
                {
                    var rpcTransaction = await rpcTransactionService.GetTransaction(rpcBlockTransaction);

                    if (rpcTransaction == null)
                        continue;

                    var transaction = new Transaction
                    {
                        BlockTime = rpcTransaction.BlockTime,
                        Hash = rpcTransaction.Hash,
                        Id = rpcTransaction.TxId,
                        Locktime = rpcTransaction.Locktime,
                        Size = rpcTransaction.Size,
                        Time = rpcTransaction.Time,
                        Version = rpcTransaction.Version,
                        Vsize = rpcTransaction.Vsize,
                        BlockId = rpcBlock.Hash,
                        BlockOrder = blockOrder
                    };

                    await transactionRepository.Insert(transaction);

                    blockOrder++;

                    foreach (var rpcTransactionOutput in rpcTransaction.Outputs)
                    {
                        var output = new Output
                        {
                            Value = rpcTransactionOutput.Value,
                            Index = rpcTransactionOutput.Index,
                            TransactionId = rpcTransaction.TxId
                        };

                        var outputId = await transactionOutputRepository.Insert(output);

                        var outputScriptPublicKey = new OutputScriptPublicKey
                        {
                            OutputId = outputId,
                            Asm = rpcTransactionOutput.ScriptPublicKey.Asm,
                            Hex = rpcTransactionOutput.ScriptPublicKey.Hex,
                            RequestedSignatures = rpcTransactionOutput.ScriptPublicKey.RequestedSignatures,
                            Type = rpcTransactionOutput.ScriptPublicKey.Type
                        };

                        await outputScriptPublicKeyRepository.Insert(outputScriptPublicKey);

                        foreach (var rpcScriptPublicKeyAddresses in rpcTransactionOutput.ScriptPublicKey.Addresses)
                        {
                            await outputScriptPublicKeyAddressRepository.Insert(new OutputScriptPublicKeyAddress
                            {
                                Address = rpcScriptPublicKeyAddresses,
                                OutputId = outputId
                            });
                        }

                    }

                    foreach (var rpcTransactionInput in rpcTransaction.Inputs)
                    {
                        var input = new Input
                        {
                            Coinbase = rpcTransactionInput.Coinbase,
                            OutputIndex = rpcTransactionInput.Vout,
                            Sequence = rpcTransactionInput.Sequence,
                            TransactionId = rpcTransaction.TxId,
                            OutputTransactionId = rpcTransactionInput.TxId
                        };

                        var inputId = await transactionInputRepository.Insert(input);

                        if (rpcTransactionInput.ScriptSignature != null)
                        {
                            var inputScriptSignature = new InputScriptSignature
                            {
                                InputId = inputId,
                                Asm = rpcTransactionInput.ScriptSignature.Asm,
                                Hex = rpcTransactionInput.ScriptSignature.Hex
                            };

                            await inputScriptSignatureRepository.Insert(inputScriptSignature);
                        }
                    }
                }

                Console.WriteLine($"{block.Height} - {block.Id}");
            }


            stopWatch.Stop();
            Console.WriteLine("Ready in {0} seconds", stopWatch.Elapsed.TotalSeconds);
            Console.Read();
        }
    }
}
