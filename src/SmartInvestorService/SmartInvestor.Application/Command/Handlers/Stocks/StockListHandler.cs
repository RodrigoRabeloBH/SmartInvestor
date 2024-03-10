using MediatR;
using Microsoft.Extensions.Logging;
using SmartInvestor.Application.Command.Requests.Stocks;
using SmartInvestor.Application.Command.Responses.Stocks;
using SmartInvestor.Domain.Interfaces;

namespace SmartInvestor.Application.Command.Handlers.Stocks
{
    public class StockListHandler : IRequestHandler<StockListRequest, StockListResponse>
    {
        private readonly IBrapiServices _brapiServices;
        private readonly ILogger<StockHandler> _logger;

        public StockListHandler(IBrapiServices brapiServices, ILogger<StockHandler> logger)
        {
            _brapiServices = brapiServices;
            _logger = logger;
        }

        public async Task<StockListResponse> Handle(StockListRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[SEARCH STOCK HANDLER] --> Getting stocks ...");

            cancellationToken.Register(() => _logger.LogInformation("[STOCK LIST HANDLER] --> Cancellation token requested"));

            var stocks = await _brapiServices.GetStocks(request.QueryParams, cancellationToken);

            return new StockListResponse { Stocks = stocks };
        }
    }
}
