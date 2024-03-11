using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace SmartInvestor.Domain.Models
{
    [ExcludeFromCodeCoverage]
    public class Index
    {
        [JsonProperty("stock")]
        public string Stock { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
