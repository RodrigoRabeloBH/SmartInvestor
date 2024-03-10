using Microsoft.EntityFrameworkCore;
using SmartInvestor.Domain.Entities;
using SmartInvestor.Domain.Interfaces;
using SmartInvestor.Infrastructure.Data;
using System.Diagnostics.CodeAnalysis;

namespace SmartInvestor.Infrastructure.Repositories
{
    [ExcludeFromCodeCoverage]
    public class WalletRepository : BaseRepository<Wallet>, IWalletRepository
    {
        public WalletRepository(SmartInvestorDbContext context) : base(context) { }

        public async Task<Wallet> GetWallet(Guid walletId, CancellationToken cancellationToken)
        {
            return await _context.Wallet
                .Include(x => x.Stocks)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == walletId, cancellationToken);
        }

        public async Task<List<Wallet>> GetWalletsByUserId(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Wallet
                .Include(x => x.Stocks)
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .ToListAsync(cancellationToken);
        }
    }
}
