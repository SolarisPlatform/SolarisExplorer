using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Solaris.BlockExplorer.DataAccess.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public class BlockService : IBlockService
    {
        private readonly SolarisExplorerContext _solarisExplorerContext;

        public BlockService(SolarisExplorerContext solarisExplorerContext)
        {
            _solarisExplorerContext = solarisExplorerContext;
        }

        public Task<Block[]> GetBlocks(int limit = 150)
        {
            return
                _solarisExplorerContext
                    .Blocks
                    .Include("Transactions.TransactionOutputs.Output")
                    .OrderByDescending(p => p.Height)
                    .Take(limit)
                    .Select(p => new Block
                    {
                        Height = p.Height,
                        Id = p.Id,
                        Transactions = p.Transactions.Select(a => new Transaction
                        {
                            Id = a.Id,
                            TransactionOutputs = a.TransactionOutputs.Select(t => new TransactionOutput
                            {
                                TransactionId = t.TransactionId,
                                Output = new Output
                                {
                                    Value = t.Output.Value
                                }
                            }).ToArray()
                        }).ToArray(),
                        Time = p.Time
                    })
                    .ToArrayAsync();
        }

        public Task<Block> GetBlock(string blockHash)
        {
            return
                _solarisExplorerContext
                    .Blocks
                    .Include("Transactions.TransactionOutputs.Output.OutputScriptPublicKey.OutputScriptPublicKeyAddresses")
                    .Include("Transactions.TransactionInputs.Input.TransactionOutput.Output.OutputScriptPublicKey.OutputScriptPublicKeyAddresses")
                    .Select(p => new Block
                    {
                        Id = p.Id,
                        Time = p.Time,
                        Size = p.Size,
                        Height = p.Height,
                        Transactions = p.Transactions.Select(a =>
                            new Transaction
                            {
                                Id = a.Id,
                                TransactionOutputs = a.TransactionOutputs.Select(t => new TransactionOutput
                                {
                                    TransactionId = t.TransactionId,
                                    Output = new Output
                                    {
                                        Value = t.Output.Value,
                                        OutputScriptPublicKey = new OutputScriptPublicKey
                                        {
                                            OutputScriptPublicKeyAddresses = t.Output.OutputScriptPublicKey
                                                .OutputScriptPublicKeyAddresses
                                        }
                                    }
                                }).ToArray(),
                                TransactionInputs = a.TransactionInputs.Select(aa => new TransactionInput
                                {
                                    TransactionId = aa.TransactionId,
                                    Input = new Input
                                    {
                                        Id = aa.Input.Id,
                                        TransactionOutput = new TransactionOutput
                                        {
                                            Output = aa.Input.TransactionOutput.Output == null
                                                ? null
                                                : new Output
                                                {
                                                    Id = aa.Input.TransactionOutput.OutputId,
                                                    OutputScriptPublicKey = new OutputScriptPublicKey
                                                    {
                                                        OutputScriptPublicKeyAddresses = aa.Input.TransactionOutput
                                                            .Output.OutputScriptPublicKey
                                                            .OutputScriptPublicKeyAddresses,
                                                        OutputId = aa.Input.TransactionOutput.OutputId
                                                    }
                                                }
                                        }
                                    }
                                }).ToArray(),
                                BlockOrder = a.BlockOrder
                            }).ToArray()
                    })
                    .FirstOrDefaultAsync(p => p.Id.Equals(blockHash));
        }

        public Task<Block> GetLastBlock()
        {
            return _solarisExplorerContext.Blocks.OrderByDescending(p => p.Height).Select(p => new Block
            {
                Id = p.Id,
                Height = p.Height
            }).FirstOrDefaultAsync();
        }
    }
}
