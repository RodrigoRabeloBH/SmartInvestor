namespace SmartInvestor.Domain.Utils
{
    public class QueryParams
    {
        private const int MaxResultsPerPage = 36;

        private int _pageSize = 6;
        public int Page { get; set; } = 1;
        public string Search { get; set; }
        public string SortOrder { get; set; }
        public string SortBy { get; set; }
        public string Type { get; set; } = "stock";
        public string Sector { get; set; }
        public int Limit { get => _pageSize; set => _pageSize = value > MaxResultsPerPage ? MaxResultsPerPage : value; }
    }
}
