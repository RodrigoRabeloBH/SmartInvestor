using Microsoft.EntityFrameworkCore;
using SmartInvestor.Domain.Entities;
using SmartInvestor.Domain.Interfaces;
using SmartInvestor.Infrastructure.Data;
using System.Diagnostics.CodeAnalysis;

namespace SmartInvestor.Infrastructure.Repositories
{
    [ExcludeFromCodeCoverage]
    public class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        protected readonly SmartInvestorDbContext _context;

        public BaseRepository(SmartInvestorDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(T entity, CancellationToken cancellationToken)
        {
            await _context.Set<T>().AddAsync(entity, cancellationToken);

            return await _context.SaveChangesAsync(cancellationToken) > 0;

        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetAsync(id, cancellationToken);

            _context.Set<T>().Remove(entity);

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<List<T>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .Where(e => e.Id == userId)
                .ToListAsync(cancellationToken);
        }

        public async Task<T> GetAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == userId, cancellationToken);
        }

        public async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            _context.Set<T>().Update(entity);

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
