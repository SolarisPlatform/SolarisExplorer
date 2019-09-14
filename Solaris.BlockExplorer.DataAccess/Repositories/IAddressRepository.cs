﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Solaris.BlockExplorer.DataAccess.Entities.Read;

namespace Solaris.BlockExplorer.DataAccess.Repositories
{
    public interface IAddressRepository
    {
        Task<Address> GetAddress(string publicKey);
        Task<PagedResult<IEnumerable<AddressTransaction>>> GetAddressTransactions(string publicKey, Paging paging);
    }
}