using MongoDB.Bson.Serialization.Attributes;

namespace Solaris.BlockExplorer.Indexer.DataAccess.Models
{
    public class Transaction : ITransaction
    {
        [BsonId]
        public string TxId { get; set; }
        public Vin[] Inputs { get; set; }
        public Vout[] Outputs { get; set; }
        public string Hex { get; set; }
        public string Hash { get; set; }
        public long Version { get; set; }
        public long Size { get; set; }
        public long VSize { get; set; }
        public long LockTime { get; set; }
        public string BlockHash { get; set; }
        public long Time { get; set; }
        public long BlockTime { get; set; }
    }
}
