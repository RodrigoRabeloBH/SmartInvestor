using SmartInvestor.Domain.Models;
using System.Diagnostics.CodeAnalysis;

namespace SmartInvestor.Application.Command.Responses.Stocks
{
    [ExcludeFromCodeCoverage]
    public class StockResponse
    {
        public StockDetail Stock { get; set; }
    }
}
