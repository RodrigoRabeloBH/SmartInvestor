using SmartInvestor.Domain.Models;
using System.Diagnostics.CodeAnalysis;

namespace SmartInvestor.Application.Command.Responses.Stocks
{
    [ExcludeFromCodeCoverage]
    public class StockListResponse
    {
        public List<Stock> Stocks { get; set; }
    }
}
