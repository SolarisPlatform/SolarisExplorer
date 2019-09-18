namespace Solaris.BlockExplorer.Domain.Models
{
    public class RichListItem
    {
        public string[] Addresses { get; set; } = new string[0];
        public decimal Amount { get; set; }
        public long LastTransaction { get; set; }
        public decimal Percentage { get; set; }
    }
}
