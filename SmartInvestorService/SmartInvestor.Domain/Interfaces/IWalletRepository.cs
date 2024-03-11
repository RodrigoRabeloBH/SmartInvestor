using SmartInvestor.Domain.Entities;

namespace SmartInvestor.Domain.Interfaces
{
    public interface IWalletRepository : IBaseRepository<Wallet>
    {
        Task<Wallet> GetWallet(Guid walletId, CancellationToken cancellationToken);

        Task<List<Wallet>> GetWalletsByUserId(Guid userId, CancellationToken cancellationToken);
    }
}
