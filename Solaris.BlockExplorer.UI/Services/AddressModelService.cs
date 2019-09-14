﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Solaris.BlockExplorer.Domain.Models;
using Solaris.BlockExplorer.Domain.Services;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public class AddressModelService : IAddressModelService
    {
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;

        public AddressModelService(IAddressService addressService, IMapper mapper)
        {
            _addressService = addressService;
            _mapper = mapper;
        }

        public async Task<IAddressModel> GetAddress(string publicKey)
        {
            var address = await _addressService.GetAddress(publicKey);

            return _mapper.Map<AddressModel>(address);
        }

        public async Task<PagedResultModel<IEnumerable<AddressTransactionModel>>> GetTransactions(string publicKey, PagingModel paging)
        {
            var transactions = await _addressService.GetTransactions(publicKey, _mapper.Map<Paging>(paging));
            return _mapper.Map<PagedResultModel<IEnumerable<AddressTransactionModel>>>(transactions);
        }
    }
}
