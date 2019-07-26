namespace Solaris.BlockExplorer.DataAccess.Models
{
    public partial class BlockTransaction
    {
        public string BlockId { get; set; }
        public string TransactionId { get; set; }
        public virtual Block Block { get; set; }
        public virtual Transaction Transaction { get; set; }
    }
}
