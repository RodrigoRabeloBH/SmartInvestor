using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace SmartInvestor.Domain.Models
{
    [ExcludeFromCodeCoverage]
    public class StockSearch
    {
        [JsonProperty("results")]
        public List<StockIDetail> Stocks { get; set; }

        [JsonProperty("requestedAt")]
        public DateTime RequestedAt { get; set; }

        [JsonProperty("took")]
        public string Took { get; set; }
    }
}
