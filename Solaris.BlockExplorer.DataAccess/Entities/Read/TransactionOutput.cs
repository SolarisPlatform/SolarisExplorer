﻿namespace Solaris.BlockExplorer.DataAccess.Entities.Read
{
    public class TransactionOutput
    {
        public long Index { get; set; }
        public string Addresses { get; set; }
        public string RedeemedTransactionId { get; set; }
        public decimal Amount { get; set; }
        public bool IsNullData { get; set; }
    }
}
