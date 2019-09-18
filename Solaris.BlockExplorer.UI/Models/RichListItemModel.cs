namespace Solaris.BlockExplorer.UI.Models
{
    public class RichListItemModel
    {
        public string[] Addresses { get; set; } = new string[0];
        public decimal Amount { get; set; }
        public long LastTransaction { get; set; }
        public decimal Percentage { get; set; }
    }
}
