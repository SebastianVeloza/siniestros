namespace Application.DTO.Pagination
{
    public class CountPage<T>
    {
        public int TotalCount { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
