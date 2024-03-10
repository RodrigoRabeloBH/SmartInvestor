using Newtonsoft.Json;

namespace SmartInvestor.Domain.Models
{
    public class Stock
    {
        [JsonProperty("stock")]
        public string Ticket { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("close")]
        public decimal Close { get; set; }

        [JsonProperty("change")]
        public decimal Change { get; set; }

        [JsonProperty("volume")]
        public int Volume { get; set; }

        [JsonProperty("market_cap")]
        public object MarketCap { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("sector")]
        public string Sector { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
