using MediatR;
using SmartInvestor.Application.Command.Responses.Stocks;
using SmartInvestor.Domain.Utils;
using System.Diagnostics.CodeAnalysis;

namespace SmartInvestor.Application.Command.Requests.Stocks
{
    [ExcludeFromCodeCoverage]
    public class StockListRequest : IRequest<StockListResponse>
    {
        public QueryParams QueryParams { get; set; }
    }
}
