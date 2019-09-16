using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Solaris.BlockExplorer.UI.Models.Api;
using Solaris.BlockExplorer.UI.Services.Api;

namespace Solaris.BlockExplorer.UI.Controllers.Api
{
    [ApiController]
    public class GetAddressTransactionsController : ControllerBase
    {
        private readonly IAddressTransactionModelService _addressTransactionModelService;

        public GetAddressTransactionsController(IAddressTransactionModelService addressTransactionModelService)
        {
            _addressTransactionModelService = addressTransactionModelService;
        }

        [Route("api/getlasttxs/{PublicKey}/{Count}/{Min}", Name = "ApiGetAddressTransactions")]
        public Task<IEnumerable<AddressTransactionModel>> Index(string publicKey, long count, long min)
        {
            return _addressTransactionModelService.GetAddressTransactions(publicKey, count, min);
        }
    }
}