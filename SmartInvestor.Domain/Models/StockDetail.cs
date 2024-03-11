using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace SmartInvestor.Domain.Models
{
    [ExcludeFromCodeCoverage]
    public class StockDetail
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("twoHundredDayAverage")]
        public decimal TwoHundredDayAverage { get; set; }

        [JsonProperty("twoHundredDayAverageChange")]
        public decimal TwoHundredDayAverageChange { get; set; }

        [JsonProperty("twoHundredDayAverageChangePercent")]
        public decimal TwoHundredDayAverageChangePercent { get; set; }

        [JsonProperty("marketCap")]
        public long MarketCap { get; set; }

        [JsonProperty("shortName")]
        public string ShortName { get; set; }

        [JsonProperty("longName")]
        public string LongName { get; set; }

        [JsonProperty("regularMarketChange")]
        public decimal RegularMarketChange { get; set; }

        [JsonProperty("regularMarketChangePercent")]
        public decimal RegularMarketChangePercent { get; set; }

        [JsonProperty("regularMarketTime")]
        public DateTime RegularMarketTime { get; set; }

        [JsonProperty("regularMarketPrice")]
        public decimal RegularMarketPrice { get; set; }

        [JsonProperty("regularMarketDayHigh")]
        public decimal RegularMarketDayHigh { get; set; }

        [JsonProperty("regularMarketDayRange")]
        public string RegularMarketDayRange { get; set; }

        [JsonProperty("regularMarketDayLow")]
        public decimal RegularMarketDayLow { get; set; }

        [JsonProperty("regularMarketVolume")]
        public int RegularMarketVolume { get; set; }

        [JsonProperty("regularMarketPreviousClose")]
        public decimal RegularMarketPreviousClose { get; set; }

        [JsonProperty("regularMarketOpen")]
        public decimal RegularMarketOpen { get; set; }

        [JsonProperty("averageDailyVolume3Month")]
        public int AverageDailyVolume3Month { get; set; }

        [JsonProperty("averageDailyVolume10Day")]
        public int AverageDailyVolume10Day { get; set; }

        [JsonProperty("fiftyTwoWeekLowChange")]
        public decimal FiftyTwoWeekLowChange { get; set; }

        [JsonProperty("fiftyTwoWeekLowChangePercent")]
        public decimal FiftyTwoWeekLowChangePercent { get; set; }

        [JsonProperty("fiftyTwoWeekRange")]
        public string FiftyTwoWeekRange { get; set; }

        [JsonProperty("fiftyTwoWeekHighChange")]
        public decimal FiftyTwoWeekHighChange { get; set; }

        [JsonProperty("fiftyTwoWeekHighChangePercent")]
        public decimal FiftyTwoWeekHighChangePercent { get; set; }

        [JsonProperty("fiftyTwoWeekLow")]
        public decimal FiftyTwoWeekLow { get; set; }

        [JsonProperty("fiftyTwoWeekHigh")]
        public decimal FiftyTwoWeekHigh { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("priceEarnings")]
        public decimal PriceEarnings { get; set; }

        [JsonProperty("earningsPerShare")]
        public decimal EarningsPerShare { get; set; }

        [JsonProperty("logourl")]
        public string Logourl { get; set; }
    }
}
