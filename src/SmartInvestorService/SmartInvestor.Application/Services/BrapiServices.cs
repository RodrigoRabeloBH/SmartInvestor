using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartInvestor.Domain.Interfaces;
using SmartInvestor.Domain.Models;
using SmartInvestor.Domain.Utils;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SmartInvestor.Application.Services
{

    [ExcludeFromCodeCoverage]
    public class BrapiServices : IBrapiServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<BrapiServices> _logger;
        private readonly IRedisRepository<StockDetail> _stockIDetailCache;
        private readonly IRedisRepository<List<Stock>> _stockListCache;

        private const string ENDPOINT = "/api/quote/";

        private readonly TimeSpan timeToLive = TimeSpan.FromMinutes(30);

        public BrapiServices(IHttpClientFactory httpClientFactory, ILogger<BrapiServices> logger,
            IRedisRepository<StockDetail> stockIDetailCache, IRedisRepository<List<Stock>> stockListCache)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _stockIDetailCache = stockIDetailCache;
            _stockListCache = stockListCache;
        }

        public async Task<StockDetail> GetStockByTicket(string ticket, CancellationToken cancellationToken)
        {
            StockDetail stockDetail = null;

            try
            {
                string CACHE_KEY = $"ticket::{ticket}";

                cancellationToken.Register(() => _logger.LogInformation("[BRAPI SERVICE]-[GET STOCK] --> Cancellation token requested"));

                stockDetail = await _stockIDetailCache.GetAsync(CACHE_KEY);

                if (stockDetail == null)
                {
                    using var http = _httpClientFactory.CreateClient(nameof(BrapiServices));

                    var response = await http.GetAsync(ENDPOINT + ticket, cancellationToken);

                    var content = await response.Content.ReadAsStringAsync(cancellationToken);

                    if (response.IsSuccessStatusCode)
                    {
                        var stockSearch = JsonConvert.DeserializeObject<StockSearch>(content);

                        stockDetail = stockSearch?.Stocks?.FirstOrDefault();

                        if (stockDetail != null)
                            await _stockIDetailCache.UpdateAsync(CACHE_KEY, stockDetail, timeToLive);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[BRAPI SERVICE]-[GET STOCK]--> Error message: {error}", ex.Message);

                throw;
            }

            return stockDetail;
        }

        public async Task<List<Stock>> GetStocks(QueryParams pagination, CancellationToken cancellationToken)
        {
            List<Stock> stocks = null;

            try
            {
                var url = BuildUrl(pagination);

                string CACHE_KEY = $"stock-list::{url}";

                cancellationToken.Register(() => _logger.LogInformation("Cancellation token requested"));

                stocks = await _stockListCache.GetAsync(CACHE_KEY);

                if (stocks == null)
                {
                    using var http = _httpClientFactory.CreateClient(nameof(BrapiServices));

                    var response = await http.GetAsync(url, cancellationToken);

                    var content = await response.Content.ReadAsStringAsync(cancellationToken);

                    if (response.IsSuccessStatusCode)
                    {
                        stocks = JsonConvert.DeserializeObject<StockSearchList>(content).Stocks;

                        if (stocks != null && stocks.Any())
                            await _stockListCache.UpdateAsync(CACHE_KEY, stocks, timeToLive);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[BRAPI SERVICE]-[GET STOCKS]--> Error message: {error}", ex.Message);

                throw;
            }

            return stocks;
        }

        private static string BuildUrl(QueryParams pagination)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"{ENDPOINT}list?type={pagination.Type}&page={pagination.Page}&limit={pagination.Limit}");

            if (!string.IsNullOrEmpty(pagination.Search))
                stringBuilder.Append($"&search={pagination.Search}");

            if (!string.IsNullOrEmpty(pagination.SortOrder))
                stringBuilder.Append($"&sortOrder={pagination.SortOrder}");

            if (!string.IsNullOrEmpty(pagination.SortBy))
                stringBuilder.Append($"&sortBy={pagination.SortBy}");

            if (!string.IsNullOrEmpty(pagination.Sector))
                stringBuilder.Append($"&sector={pagination.Sector}");

            var url = stringBuilder.ToString();

            return url;
        }
    }
}
