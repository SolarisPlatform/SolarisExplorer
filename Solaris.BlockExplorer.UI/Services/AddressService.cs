using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solaris.BlockExplorer.DataAccess.Models;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public class AddressService : IAddressService
    {
        private readonly SolarisExplorerContext _solarisExplorerContext;

        public AddressService(SolarisExplorerContext solarisExplorerContext)
        {
            _solarisExplorerContext = solarisExplorerContext;
        }

        public async Task<AddressModel> GetAddress(string address)
        {
            var received = await _solarisExplorerContext.Outputs.Where(p => p.OutputScriptPublicKey.OutputScriptPublicKeyAddresses.Any(pp => pp.Address.Equals(address))).SumAsync(p => p.Value);
            var receivedCount = await _solarisExplorerContext.Outputs.Where(p =>
                    p.OutputScriptPublicKey.OutputScriptPublicKeyAddresses.Any(pp => pp.Address.Equals(address)))
                .LongCountAsync();

            //.Where(pp => pp.OutputIndex.Equals(p.OutputIndex))
            var sent = await _solarisExplorerContext.Inputs.Where(p => p.Transaction.TransactionOutputs
                    .Any(pp =>
                        pp.Output.OutputScriptPublicKey.OutputScriptPublicKeyAddresses.Any(ppp =>
                            ppp.Address.Equals(address))))
                .SumAsync(p => p.Transaction.TransactionOutputs.Sum(pp => pp.Output.Value));

            var sentCount = await _solarisExplorerContext.Inputs.Where(p => p.Transaction.TransactionOutputs
                .Where(pp => pp.OutputIndex.Equals(p.OutputIndex)).Any(pp =>
                    pp.Output.OutputScriptPublicKey.OutputScriptPublicKeyAddresses.Any(ppp =>
                        ppp.Address.Equals(address)))).LongCountAsync();
            
                //In inputs kijken of er een Transaction is die wijst naar een vout waar dit address in voor komt


                //await _solarisExplorerContext.Inputs.Where(p => p.Transaction.TransactionOutputs.Any(pp =>
                //        pp.Output.OutputScriptPublicKey.OutputScriptPublicKeyAddresses.Any(ppp =>
                //            ppp.Address.Equals(address))))
                //    .SumAsync(p => p.Transaction.TransactionOutputs.Sum(pp => pp.Output.Value));

            return new AddressModel
            {
                Received = received,
                ReceivedCount = receivedCount,
                Sent = sent,
                SentCount = sentCount,
                Balance = received - sent,
                Address = address
            };
        }
    }
}
