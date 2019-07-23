using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Solaris.BlockExplorer.Indexer.DataAccess.Models;
using Solaris.BlockExplorer.Indexer.DataAccess.Repositories;
using Solaris.BlockExplorer.Indexer.Domain.Models;
using Solaris.BlockExplorer.Indexer.Domain.Services;

namespace Solaris.BlockExplorer.Indexer
{
    public class Program
    {
        private static ServiceProvider _serviceProvider;

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
                .AddSingleton(provider =>
                {
                    var configuration = provider.GetService<IConfiguration>();
                    var section = configuration.GetSection("MongoDB");
                    var connectionString = section.GetValue<string>("ConnectionString");
                    return new MongoClient(connectionString);
                })
                .AddSingleton(provider =>
                {
                    var configuration = provider.GetService<IConfiguration>();
                    var section = configuration.GetSection("MongoDB");
                    var databaseName = section.GetValue<string>("DatabaseName");
                    var mongoClient = provider.GetService<MongoClient>();
                    return mongoClient.GetDatabase(databaseName);
                })
                .AddSingleton<IRpcTransactionService, RpcTransactionService>()
                .AddSingleton<IBlockRepository, BlockRepository>()
                .AddSingleton<ITransactionRepository, TransactionRepository>();

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

            var blockRepository = _serviceProvider.GetService<IBlockRepository>();
            var transactionRepository = _serviceProvider.GetService<ITransactionRepository>();

            var currentBlockHeight = await blockRepository.GetCurrent();

            Parallel.For(currentBlockHeight, blockCount, i =>
            {
                var task = Task.Run(async () =>
                {
                    var block = await blockService.GetBlock(i);

                    var transactions = new List<IRpcTransaction>();

                    if (i > 0)
                        foreach (var blockTransaction in block.Transactions)
                        {
                            var transaction = await transactionService.GetTransaction(blockTransaction);
                            transactions.Add(transaction);
                        }

                    await blockRepository.Insert(new Block
                    {
                        Hash = block.Hash,
                        Height = block.Height,
                        Id = block.Height,
                        Transactions = block.Transactions,
                        Difficulty = block.Difficulty,
                        Time = block.Time,
                        TransactionCount = block.TransactionCount,
                        ValueOut = transactions.SelectMany(p => p.Outputs).Sum(p => p.Value)
                    });

                    foreach (var transaction in transactions)
                    {
                        await transactionRepository.Insert(new Transaction
                        {
                            BlockHash = transaction.BlockHash,
                            BlockTime = transaction.BlockTime,
                            Hash = transaction.Hash,
                            Hex = transaction.Hex,
                            Outputs = transaction.Outputs.Select(p => new Vout
                            {
                                Index = p.Index,
                                ScriptPubKey = new ScriptPubKey
                                {
                                    Addresses = p.ScriptPubKey.Addresses,
                                    Asm = p.ScriptPubKey.Asm,
                                    Hex = p.ScriptPubKey.Hex,
                                    ReqSigs = p.ScriptPubKey.ReqSigs,
                                    Type = p.ScriptPubKey.Type
                                },
                                Value = p.Value
                            }).ToArray(),
                            Inputs = transaction.Inputs.Select(p => new Vin
                            {
                                ScriptSig = p.ScriptSig == null ? null : new ScriptSig
                                {
                                    Asm = p.ScriptSig.Asm,
                                    Hex = p.ScriptSig.Hex
                                },
                                Sequence = p.Sequence,
                                TxId = p.TxId,
                                Vout = p.Vout,
                                CoinBase = p.CoinBase
                            }).ToArray(), 
                            Time = transaction.Time,
                            LockTime = transaction.LockTime, 
                            Size = transaction.Size, 
                            TxId = transaction.TxId,
                            VSize = transaction.VSize, 
                            Version = transaction.Version
                        });
                    }

                    Console.WriteLine(i);
                });
                Task.WaitAll(task);
            });


            stopWatch.Stop();
            Console.WriteLine("Ready in {0} seconds", stopWatch.Elapsed.TotalSeconds);
            Console.Read();
        }
    }
}
