using MediatR;
using SmartInvestor.Application.Command.Responses.Stocks;
using System.Diagnostics.CodeAnalysis;

namespace SmartInvestor.Application.Command.Requests.Stocks
{
    [ExcludeFromCodeCoverage]
    public class StockRequest : IRequest<StockResponse>
    {
        public string Ticket { get; set; }
    }
}
