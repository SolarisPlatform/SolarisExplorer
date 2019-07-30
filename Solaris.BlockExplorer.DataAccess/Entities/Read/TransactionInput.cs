namespace Solaris.BlockExplorer.DataAccess.Entities.Read
{
    public class TransactionInput
    {
        public string PreviousOutputTransactionId { get; set; }
        public long PreviousOutputIndex { get; set; }

 
        public string[] Addresses { get; set; }

        public string AddressList
        {
            set => Addresses = value?.Split(',');
        }


        public decimal Amount { get; set; }
    }
}
