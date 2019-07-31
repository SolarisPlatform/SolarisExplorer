namespace Solaris.BlockExplorer.DataAccess.Entities.Read
{
    public class BlockTransactionDetail
    {
        public string AddressList
        {
            set => Addresses = value?.Split(',');
        }

        public string[] Addresses { get; set; } = new string[0];
        public decimal? Amount { get; set; }
    }
}
