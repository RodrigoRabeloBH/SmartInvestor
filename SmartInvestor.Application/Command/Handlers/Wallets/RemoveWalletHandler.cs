using MediatR;
using SmartInvestor.Application.Command.Requests.Wallets;
using SmartInvestor.Application.Command.Responses.Wallets;
using SmartInvestor.Domain.Interfaces;

namespace SmartInvestor.Application.Command.Handlers.Wallets
{
    public class RemoveWalletHandler : IRequestHandler<RemoveWalletRequest, RemoveWalletResponse>
    {
        private readonly IWalletRepository _walletRepository;

        public RemoveWalletHandler(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<RemoveWalletResponse> Handle(RemoveWalletRequest request, CancellationToken cancellationToken)
        {
            bool deleted = await _walletRepository.DeleteAsync(request.WalletId, cancellationToken);

            return new RemoveWalletResponse { Success = deleted };
        }
    }
}
