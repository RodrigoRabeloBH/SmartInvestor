namespace SmartInvestor.Domain.Interfaces
{
    public interface IRedisRepository<T> where T : class
    {
        Task<T> GetAsync(string cacheKey);
        Task<bool> UpdateAsync(string cacheKey, T entity, TimeSpan timeToLive);
        Task<bool> DeleteAsync(string cacheKey);
    }
}
