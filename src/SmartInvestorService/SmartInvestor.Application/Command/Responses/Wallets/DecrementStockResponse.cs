using SmartInvestor.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace SmartInvestor.Application.Command.Responses.Wallets
{
    [ExcludeFromCodeCoverage]
    public class DecrementStockResponse
    {
        public bool Success { get; set; }
        public Wallet Wallet { get; set; }
    }
}
