﻿using SmartInvestor.Domain.Models;
using SmartInvestor.Domain.Utils;

namespace SmartInvestor.Domain.Interfaces
{
    public interface IBrapiServices
    {
        Task<StockDetail> GetStockByTicket(string ticket, CancellationToken cancellationToken);

        Task<List<Stock>> GetStocks(QueryParams pagination, CancellationToken cancellationToken);
    }
}
