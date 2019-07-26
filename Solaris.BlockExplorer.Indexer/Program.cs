using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Solaris.BlockExplorer.DataAccess.Models;
using Solaris.BlockExplorer.Indexer.Domain.Services;

namespace Solaris.BlockExplorer.Indexer
{
    public class Program
    {
        private static ServiceProvider _serviceProvider;
        private const string GenesisBlock = "0000000000000000000000000000000000000000000000000000000000000000";
        private static void ConfigureServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddSingleton<IConfiguration>(p => new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build())
                .AddSingleton(typeof(IWalletRpcService<>), typeof(WalletRpcService<>))
                .AddSingleton<IRpcBlockCountService, RpcBlockCountService>()
                .AddSingleton<IRpcBlockHashService, RpcBlockHashService>()
                .AddSingleton<IRpcBlockService, RpcBlockService>()
                .AddSingleton<IRpcTransactionService, RpcTransactionService>()
                .AddDbContext<SolarisExplorerContext>((provider, builder) =>
                {
                    var configuration = provider.GetService<IConfiguration>();
                    var connectionString = configuration.GetConnectionString("SolarisExplorerDatabase");

                    builder.UseSqlServer(connectionString);
                });


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
            var blockCountService = _serviceProvider.GetService<IRpcBlockCountService>();
            var blockService = _serviceProvider.GetService<IRpcBlockService>();
            var transactionService = _serviceProvider.GetService<IRpcTransactionService>();
            var blockCount = await blockCountService.GetBlockCount();

            var solarisExplorerContext = _serviceProvider.GetService<SolarisExplorerContext>();

            var currentBlockHeight = await solarisExplorerContext.Blocks.LongCountAsync();

            if (currentBlockHeight == blockCount)
                Console.WriteLine("No update required");

            for (var i = currentBlockHeight; i <= blockCount; i++)
            {
                var rpcBlock = await blockService.GetBlock(i);

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

                await solarisExplorerContext.Blocks.AddAsync(block);

                var blockOrder = 0L;

                if (block.Height > 0)
                    foreach (var rpcBlockTransaction in rpcBlock.Transactions)
                    {
                        var rpcTransaction = await transactionService.GetTransaction(rpcBlockTransaction);
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

                        foreach (var rpcTransactionOutput in rpcTransaction.Outputs)
                        {
                            var output = new Output
                            {
                                Value = rpcTransactionOutput.Value
                            };
                            if (rpcTransactionOutput.ScriptPublicKey != null)
                                output.OutputScriptPublicKey = new OutputScriptPublicKey
                                {
                                    Asm = rpcTransactionOutput.ScriptPublicKey.Asm,
                                    Hex = rpcTransactionOutput.ScriptPublicKey.Hex,
                                    RequestedSignatures = rpcTransactionOutput.ScriptPublicKey.RequestedSignatures,
                                    Type = rpcTransactionOutput.ScriptPublicKey.Type,
                                    OutputScriptPublicKeyAddresses = rpcTransactionOutput.ScriptPublicKey.Addresses.Select(
                                        address => new OutputScriptPublicKeyAddress
                                        {
                                            Address = address
                                        }).ToArray()
                                };
                            var transactionOutput = new TransactionOutput
                            {
                                OutputIndex = rpcTransactionOutput.OutputIndex,
                                Output = output
                            };

                            transaction.TransactionOutputs.Add(transactionOutput);
                        }

                        foreach (var rpcTransactionInput in rpcTransaction.Inputs)
                        {
                            var input = new Input
                            {
                                Coinbase = rpcTransactionInput.Coinbase,
                                OutputIndex = rpcTransactionInput.Vout,
                                Sequence = rpcTransactionInput.Sequence,
                                TransactionId = rpcTransaction.TxId,
                                InputScriptSignature = rpcTransactionInput.ScriptSignature == null ? null : new InputScriptSignature
                                {
                                    Asm = rpcTransactionInput.ScriptSignature.Asm,
                                    Hex = rpcTransactionInput.ScriptSignature.Hex
                                }
                            };
                            var transactionInput = new TransactionInput
                            {
                                Input = input,
                                TransactionId = transaction.Id
                            };

                            await solarisExplorerContext.TransactionInputs.AddAsync(transactionInput);
                        }
                        await solarisExplorerContext.Transactions.AddAsync(transaction);
                        await solarisExplorerContext.BlockTransactions.AddAsync(new BlockTransaction
                        {
                            BlockId = rpcBlock.Hash,
                            TransactionId = rpcTransaction.TxId
                        });

                        blockOrder++;
                    }
                
                if (i % 100 == 1 && i > 0)
                    await solarisExplorerContext.SaveChangesAsync();

                Console.WriteLine($"{block.Height} - {block.Id}");
            }

            await solarisExplorerContext.SaveChangesAsync();

            stopWatch.Stop();
            Console.WriteLine("Ready in {0} seconds", stopWatch.Elapsed.TotalSeconds);
            Console.Read();
        }
    }
}
