using MediatR;
using SmartInvestor.Application.Command.Requests.Wallets;
using SmartInvestor.Application.Command.Responses.Wallets;
using SmartInvestor.Domain.Interfaces;

namespace SmartInvestor.Application.Command.Handlers.Wallets
{
    public class DecrementStockHandler : IRequestHandler<DecrementStockRequest, DecrementStockResponse>
    {
        private readonly IWalletRepository _walletRepository;

        public DecrementStockHandler(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<DecrementStockResponse> Handle(DecrementStockRequest request, CancellationToken cancellationToken)
        {
            var wallet = await _walletRepository.GetWallet(request.WalletId, cancellationToken)
                ?? throw new ApplicationException("Wallet not found");

            var stock = wallet.Stocks.Find(s => s.Ticket == request.Ticket)
                ?? throw new ApplicationException("Ticket not found");

            wallet.DecremetQuantity(request.Quantity, request.SellingPrice, stock);

            wallet.SetValues();

            bool updated = await _walletRepository.UpdateAsync(wallet, cancellationToken);

            return new DecrementStockResponse { Success = updated, Wallet = wallet };
        }
    }
}
