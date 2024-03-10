using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace SmartInvestor.Domain.Models
{
    [ExcludeFromCodeCoverage]
    public class StockSearchList
    {
        [JsonProperty("indexes")]
        public List<Index> Indexes { get; set; } = new List<Index>();

        [JsonProperty("stocks")]
        public List<Stock> Stocks { get; set; }

        [JsonProperty("availableSectors")]
        public List<string> AvailableSectors { get; set; }

        [JsonProperty("availableStockTypes")]
        public List<string> AvailableStockTypes { get; set; }

        [JsonProperty("currentPage")]
        public int CurrentPage { get; set; }

        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }

        [JsonProperty("itemsPerPage")]
        public int ItemsPerPage { get; set; }

        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }

        [JsonProperty("hasNextPage")]
        public bool HasNextPage { get; set; }
    }
}
