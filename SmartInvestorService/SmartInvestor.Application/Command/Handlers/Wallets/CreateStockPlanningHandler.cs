using AutoMapper;
using MediatR;
using SmartInvestor.Application.Command.Requests.Wallets;
using SmartInvestor.Application.Command.Responses.Wallets;
using SmartInvestor.Domain.Entities;
using SmartInvestor.Domain.Interfaces;

namespace SmartInvestor.Application.Command.Handlers.Wallets
{
    public class CreateStockPlanningHandler : IRequestHandler<CreateStockPlanningRequest, CreateStockPlanningResponse>
    {
        private readonly IWalletRepository _rep;
        private readonly IBrapiServices _brapiServices;
        private readonly IMapper _mapper;

        public CreateStockPlanningHandler(IWalletRepository rep, IBrapiServices brapiServices, IMapper mapper)
        {
            _rep = rep;
            _brapiServices = brapiServices;
            _mapper = mapper;
        }

        public async Task<CreateStockPlanningResponse> Handle(CreateStockPlanningRequest request, CancellationToken cancellationToken)
        {
            var wallet = await _rep.GetWallet(request.WalletId, cancellationToken)
                ?? throw new ApplicationException("Could not found wallet!");

            var stock = await _brapiServices.GetStockByTicket(request.Ticket, cancellationToken)
                ?? throw new ApplicationException("Could not found ticket!");

            var stockPlanning = _mapper.Map<StockPlanning>(request);

            stockPlanning.CurrentPrice = stock.RegularMarketPrice;

            stockPlanning.LogoUrl = stock.Logourl;

            var stockAlreadyAdded = wallet.Stocks.Find(s => s.Ticket == request.Ticket);

            if (stockAlreadyAdded != null)
            {
                stockAlreadyAdded.CurrentPrice = stock.RegularMarketPrice;

                wallet.IncremetQuantity(request.CurrentQuantity, request.PurchasePrice, stockAlreadyAdded);
            }
            else
                wallet.Stocks.Add(stockPlanning);

            wallet.SetValues();

            bool updated = await _rep.UpdateAsync(wallet, cancellationToken);

            return new CreateStockPlanningResponse { Success = updated, Wallet = wallet };
        }
    }
}
