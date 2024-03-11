using SmartInvestor.Domain.Entities;

namespace SmartInvestor.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : Entity
    {
        Task<T> GetAsync(Guid userId, CancellationToken cancellationToken);
        Task<List<T>> GetAllAsync(Guid userId, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken);
        Task<bool> CreateAsync(T entity, CancellationToken cancellationToken);
    }
}
