using AutoMapper;
using MediatR;
using SmartInvestor.Application.Command.Requests.Wallets;
using SmartInvestor.Application.Command.Responses.Wallets;
using SmartInvestor.Domain.Interfaces;

namespace SmartInvestor.Application.Command.Handlers.Wallets
{
    public class UpdateWalletHandler : IRequestHandler<UpdateWalletRequest, UpdateWalletResponse>
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IMapper _mapper;

        public UpdateWalletHandler(IWalletRepository walletRepository, IMapper mapper)
        {
            _walletRepository = walletRepository;
            _mapper = mapper;
        }

        public async Task<UpdateWalletResponse> Handle(UpdateWalletRequest request, CancellationToken cancellationToken)
        {
            var wallet = await _walletRepository.GetWallet(request.WalletId, cancellationToken);

            var walletToUpdate = _mapper.Map(request, wallet);

            walletToUpdate.SetValues();

            bool updated = await _walletRepository.UpdateAsync(walletToUpdate, cancellationToken);

            return new UpdateWalletResponse { Wallet = wallet, Success = updated };
        }
    }
}
