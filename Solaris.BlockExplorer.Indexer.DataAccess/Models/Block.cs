using MongoDB.Bson.Serialization.Attributes;

namespace Solaris.BlockExplorer.Indexer.DataAccess.Models
{
    public class Block : IBlock
    {
        [BsonId]
        public long Id { get; set; }
        public string Hash { get; set; }
        public long Height { get; set; }
        public string[] Transactions { get; set; }
        public long Time { get; set; }
        public long TransactionCount { get; set; }
        public decimal Difficulty { get; set; }
        public long Size { get; set; }
        public decimal ValueOut { get; set; }
    }
}
