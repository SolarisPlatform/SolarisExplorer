namespace Solaris.BlockExplorer.UI.Models
{
    public class PagedResultModel<T>
    {
        public T Result { get; set; }
        public long PageCount { get; set; }
        public long CurrentPage { get; set; }
        public int PageSize { get; set; }
        public long PagingStart => CurrentPage - 5 <= 0 ? 1 : CurrentPage - 5;

        public long PagingEnd => CurrentPage + 6 > PageCount ? PageCount : CurrentPage + 6;
    }
}
