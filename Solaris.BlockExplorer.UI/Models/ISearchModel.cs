namespace Solaris.BlockExplorer.UI.Models
{
    public interface ISearchModel
    {
        long Height { get; set; }
        string Id { get; set; }
        string Type { get; set; }
    }
}