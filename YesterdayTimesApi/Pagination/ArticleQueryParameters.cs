
namespace YesterdayTimesApi.Pagination
{
    public class ArticleQueryParameters
    {
        const int maxPageSize = 9;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 9;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
