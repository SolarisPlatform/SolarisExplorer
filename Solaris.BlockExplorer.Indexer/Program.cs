using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NBitcoin;
using Solaris.BlockExplorer.DataAccess.Entities.Create;
using Solaris.BlockExplorer.DataAccess.Repositories;
using Solaris.BlockExplorer.Domain.Factories;
using Solaris.BlockExplorer.Domain.Mappings;
using Solaris.BlockExplorer.Domain.Models.Rpc;
using Solaris.BlockExplorer.Domain.Services.Rpc;
using Solaris.BlockExplorer.Indexer.ColdStaking;
using Solaris.BlockExplorer.Indexer.Networks;


using Transaction = Solaris.BlockExplorer.DataAccess.Entities.Create.Transaction;

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
                .AddScoped<DataAccess.Repositories.ITransactionRepository, TransactionRepository>()
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

            StandardScripts.RegisterStandardScriptTemplate(ColdStakingScriptTemplate.Instance);
        }

        private static readonly Network Network = GetNetwork();

        public static async Task Main(string[] args)
        {
            ShowAscii();
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
                    var transactionRepository = scope.ServiceProvider.GetService<DataAccess.Repositories.ITransactionRepository>();

                    var rpcBlock = await rpcBlockService.GetBlock(i);

                    var block = new DataAccess.Entities.Create.Block
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

                        var outputs = GetTransactionOutputs(rpcTransaction);

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

        private static IEnumerable<Output> GetTransactionOutputs(IRpcTransaction rpcTransaction)
        {
            foreach (var output in rpcTransaction.Outputs)
            {
                var addresses = new string[0];

                if (!string.IsNullOrEmpty(output.ScriptPublicKey.Hex))
                {
                    addresses = GetAddress(Network, DecodeScriptHex(output.ScriptPublicKey.Hex));
                }

                yield return new Output
                {
                    Value = output.Value,
                    Index = output.Index,
                    OutputScriptPublicKey = new OutputScriptPublicKey
                    {
                        Asm = output.ScriptPublicKey.Asm,
                        Hex = output.ScriptPublicKey.Hex,
                        RequestedSignatures = output.ScriptPublicKey.RequestedSignatures,
                        Type = output.ScriptPublicKey.Type,
                        OutputScriptPublicKeyAddresses = addresses.Select(address => new OutputScriptPublicKeyAddress
                        {
                            Address = address
                        })
                    }
                };
            }
        }

        private static Script DecodeScriptHex(string scriptHex)
        {
            return new Script(NBitcoin.DataEncoders.Encoders.Hex.DecodeData(scriptHex));
        }

        private static void ShowAscii()
        {
            try
            {
                var ascii = File.ReadAllText("Ascii.txt");

                Console.SetWindowSize(100, 45);
                Console.WriteLine(ascii);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private static Network GetNetwork()
        {
            return new ExplorerNetwork();
        }

        public static string[] GetAddress(Network network, Script script)
        {
            var template = StandardScripts.GetTemplateFromScriptPubKey(script);

            switch (template?.Type)
            {
                case TxOutType.TX_PUBKEY: case TxOutType.TX_MULTISIG:
                    var pubkeys = script.GetDestinationPublicKeys(network);
                    return pubkeys.Select(p => p.GetAddress(network).ToString()).ToArray();
                case TxOutType.TX_PUBKEYHASH: case TxOutType.TX_SCRIPTHASH: case TxOutType.TX_SEGWIT:
                    var bitcoinAddress = script.GetDestinationAddress(network);
                    return bitcoinAddress != null ? new[] { bitcoinAddress.ToString() } : new string[0];
                case TxOutType.TX_COLDSTAKE:
                    if (ColdStakingScriptTemplate.Instance.ExtractScriptPubKeyParameters(script, out _, out var coldPubKeyHash))
                    {
                        return new[]
                        {
                            coldPubKeyHash.GetAddress(network).ToString()
                        };
                    }

                    return new string[0];
                default:
                    return new string[0];
            }
        }
    }
}
