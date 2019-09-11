using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
                .AddScoped(typeof(IWalletRpcService<>), typeof(WalletRpcService<>))
                .AddScoped<IRpcBlockCountService, RpcBlockCountService>()
                .AddScoped<IRpcBlockHashService, RpcBlockHashService>()
                .AddScoped<IRpcBlockService, RpcBlockService>()
                .AddScoped<IRpcTransactionService, RpcTransactionService>()
                .AddScoped<IDbConnectionFactory>(provider =>
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
                .AddScoped<IBlockRepository, BlockRepository>()
                .AddScoped<ITransactionRepository, TransactionRepository>()
                .AddScoped<ITransactionOutputRepository, TransactionOutputRepository>()
                .AddScoped<ITransactionInputRepository, TransactionInputRepository>();


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

            try
            {
                Console.SetWindowSize(100, 45);
            }
            catch (Exception)
            {
                
            }

            var ascii = File.ReadAllText("Ascii.txt");
            Console.WriteLine(ascii);

            ConfigureServiceProvider();

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var rpcBlockCountService = _serviceProvider.GetService<IRpcBlockCountService>();
            var rpcBlockService = _serviceProvider.GetService<IRpcBlockService>();
            var rpcTransactionService = _serviceProvider.GetService<IRpcTransactionService>();
            var rpcBlockCount = await rpcBlockCountService.GetBlockCount();

            var blockCountRepository = _serviceProvider.GetService<IBlockRepository>();


            var blockCount = await blockCountRepository.GetBlockHeight();

            if (blockCount > 0)
                blockCount++;

            if (rpcBlockCount == blockCount && rpcBlockCount > 0)
                Console.WriteLine("No update required");


            for (var i = blockCount; i < rpcBlockCount; i++)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var blockRepository = scope.ServiceProvider.GetService<IBlockRepository>();
                    var transactionRepository = scope.ServiceProvider.GetService<ITransactionRepository>();

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
                        PreviousBlock = rpcBlock.PreviousBlock == GenesisBlock ? null : rpcBlock.PreviousBlock,
                        Json = rpcBlock.Json
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
                            BlockId = rpcTransaction.BlockHash,
                            BlockOrder = blockOrder,
                            Json = rpcTransaction.Json
                        };

                        var inputs = rpcTransaction.Inputs.Select(p => new Input
                        {
                            Coinbase = p.Coinbase,
                            OutputIndex = p.Vout,
                            Sequence = p.Sequence,
                            TransactionId = rpcTransaction.TxId,
                            OutputTransactionId = p.TxId,
                            InputScriptSignature = p.ScriptSignature == null ? null : new InputScriptSignature
                            {
                                Asm = p.ScriptSignature.Asm,
                                Hex = p.ScriptSignature.Hex
                            }
                        });

                        var outputs = rpcTransaction.Outputs.Select(p => new Output
                        {
                            Value = p.Value,
                            Index = p.Index,
                            OutputScriptPublicKey = new OutputScriptPublicKey
                            {
                                Asm = p.ScriptPublicKey.Asm,
                                Hex = p.ScriptPublicKey.Hex,
                                RequestedSignatures = p.ScriptPublicKey.RequestedSignatures,
                                Type = p.ScriptPublicKey.Type,
                                OutputScriptPublicKeyAddresses = p.ScriptPublicKey.Addresses.Select(a => new OutputScriptPublicKeyAddress
                                {
                                    Address = a
                                })
                            }
                        });

                        await transactionRepository.Insert(transaction, inputs, outputs);

                        blockOrder++;
                    }

                    if (block.Height % 100 == 0 || i == blockCount || i == rpcBlockCount)
                        Console.WriteLine($"{block.Height} - {block.Id}");

                }
            }

            stopWatch.Stop();
            Console.WriteLine("Ready in {0} seconds", stopWatch.Elapsed.TotalSeconds);
            
        }
    }
}
