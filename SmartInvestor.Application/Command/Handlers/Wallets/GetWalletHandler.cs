using MediatR;
using SmartInvestor.Application.Command.Requests.Wallets;
using SmartInvestor.Application.Command.Responses.Wallets;
using SmartInvestor.Domain.Interfaces;

namespace SmartInvestor.Application.Command.Handlers.Wallets
{
    public class GetWalletHandler : IRequestHandler<GetWalletRequest, GetWalletResponse>
    {
        private readonly IWalletRepository _repo;

        public GetWalletHandler(IWalletRepository repo)
        {
            _repo = repo;
        }

        public async Task<GetWalletResponse> Handle(GetWalletRequest request, CancellationToken cancellationToken)
        {
            var wallet = await _repo.GetWallet(request.WalletId, cancellationToken);

            return new GetWalletResponse { Wallet = wallet };
        }
    }
}
