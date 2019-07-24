using MongoDB.Bson.Serialization.Attributes;

namespace Solaris.BlockExplorer.UI.Models
{
    public class TransactionModel : ITransactionModel
    {
        [BsonId]
        public string TxId { get; set; }
        public VinModel[] Inputs { get; set; }
        public VoutModel[] Outputs { get; set; }
        public string Hex { get; set; }
        public string Hash { get; set; }
        public long Version { get; set; }
        public long Size { get; set; }
        public long VSize { get; set; }
        public long LockTime { get; set; }
        public string BlockHash { get; set; }
        public IBlockModel Block { get; set; }
        public long Time { get; set; }
        public long BlockTime { get; set; }
        public long Confirmations { get; set; }
    }
}
