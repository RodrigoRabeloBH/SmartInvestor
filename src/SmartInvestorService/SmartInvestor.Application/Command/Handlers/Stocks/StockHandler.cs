using MediatR;
using Microsoft.Extensions.Logging;
using SmartInvestor.Application.Command.Requests.Stocks;
using SmartInvestor.Application.Command.Responses.Stocks;
using SmartInvestor.Domain.Interfaces;

namespace SmartInvestor.Application.Command.Handlers.Stocks
{
    public class StockHandler : IRequestHandler<StockRequest, StockResponse>
    {
        private readonly IBrapiServices _brapiServices;
        private readonly ILogger<StockHandler> _logger;

        public StockHandler(IBrapiServices brapiServices, ILogger<StockHandler> logger)
        {
            _brapiServices = brapiServices;
            _logger = logger;
        }

        public async Task<StockResponse> Handle(StockRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[SEARCH STOCK HANDLER] --> Getting stock with ticket: {ticket}", request.Ticket);

            cancellationToken.Register(() => _logger.LogInformation("[STOCK HANDLER] --> Cancellation token requested"));

            var stock = await _brapiServices.GetStockByTicket(request.Ticket, cancellationToken);

            return new StockResponse { Stock = stock };
        }
    }
}
