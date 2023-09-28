namespace Domain.Pagination
{
    public class Parameters
    {
        const int MAX_SIZE_PAGE = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize { get; set; } = 10;

        public int PageSize { 
            get { return _pageSize;}
            set { _pageSize = (value > MAX_SIZE_PAGE) ? MAX_SIZE_PAGE : value; } }
    }
}
