using SmartInvestor.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace SmartInvestor.Application.Command.Responses.Wallets
{
    [ExcludeFromCodeCoverage]
    public class GetWalletResponse
    {
        public Wallet Wallet { get; set; }
    }
}
