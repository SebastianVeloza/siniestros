namespace Application.DTO.Pagination
{
    public class PagedResult<T>
    {
        public IReadOnlyCollection<T> Items { get; }
        public int TotalItems { get; }
        public int Page { get; }
        public int PageSize { get; }
        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);

        public PagedResult(
            IReadOnlyCollection<T> items,
            int totalItems,
            int page,
            int pageSize)
        {
            Items = items;
            TotalItems = totalItems;
            Page = page;
            PageSize = pageSize;
        }
    }
}
