namespace Solaris.BlockExplorer.DataAccess.Entities.Read
{
    public class PagedResult<T>
    {
        public T Result { get; set; }
        public long PageCount { get; set; }
        public long CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
