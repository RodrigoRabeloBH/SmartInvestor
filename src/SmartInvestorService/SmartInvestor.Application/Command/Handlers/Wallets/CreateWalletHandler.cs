using MediatR;
using SmartInvestor.Application.Command.Requests.Wallets;
using SmartInvestor.Application.Command.Responses.Wallets;
using SmartInvestor.Domain.Entities;
using SmartInvestor.Domain.Interfaces;

namespace SmartInvestor.Application.Command.Handlers.Wallets
{
    public class CreateWalletHandler : IRequestHandler<CreateWalletRequest, CreateWalletResponse>
    {
        private readonly IWalletRepository _repo;

        public CreateWalletHandler(IWalletRepository repo)
        {
            _repo = repo;
        }

        public async Task<CreateWalletResponse> Handle(CreateWalletRequest request, CancellationToken cancellationToken)
        {
            var wallet = new Wallet
            {
                CreatedDate = DateTime.UtcNow,
                DividendGoalPerYear = request.DividendGoalPerYear,
                UserId = request.UserId,
                Username = request.Username
            };

            bool created = await _repo.CreateAsync(wallet, cancellationToken);

            return new CreateWalletResponse
            {
                Wallet = wallet,
                Success = created
            };
        }
    }
}
