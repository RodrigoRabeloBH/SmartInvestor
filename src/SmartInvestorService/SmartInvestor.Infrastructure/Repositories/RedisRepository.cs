using Microsoft.Extensions.Logging;
using SmartInvestor.Domain.Interfaces;
using StackExchange.Redis;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace SmartInvestor.Infrastructure.Repositories
{
    [ExcludeFromCodeCoverage]
    public class RedisRepository<T> : IRedisRepository<T> where T : class
    {
        private readonly IDatabase _database;
        private readonly ILogger<RedisRepository<T>> _logger;

        public RedisRepository(IConnectionMultiplexer connectionMultiplexer, ILogger<RedisRepository<T>> logger)
        {
            _database = connectionMultiplexer.GetDatabase();
            _logger = logger;
        }

        public async Task<bool> DeleteAsync(string cacheKey)
        {
            try
            {
                return await _database.KeyDeleteAsync(cacheKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[REDIS-REPOSITORY]-[DELETE-ASYNC] --> Error message: {error}", ex.Message);

                throw;
            }
        }

        public async Task<T> GetAsync(string cacheKey)
        {
            try
            {
                var data = await _database.StringGetAsync(cacheKey);

                return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<T>(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[REDIS-REPOSITORY]-[GET-ASYNC] --> Error message: {error}", ex.Message);

                throw;
            }
        }

        public async Task<bool> UpdateAsync(string cacheKey, T entity, TimeSpan timeToLive)
        {
            try
            {
                return await _database.StringSetAsync(cacheKey, JsonSerializer.Serialize(entity), timeToLive);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[REDIS-REPOSITORY]-[UPDATE-ASYNC] --> Error message: {error}", ex.Message);

                throw;
            }
        }
    }
}
