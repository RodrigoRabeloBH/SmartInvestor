using MediatR;
using SmartInvestor.Application.Command.Responses.Wallets;
using System.Diagnostics.CodeAnalysis;

namespace SmartInvestor.Application.Command.Requests.Wallets
{
    [ExcludeFromCodeCoverage]
    public class UpdateWalletRequest : IRequest<UpdateWalletResponse>
    {
        public Guid WalletId { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public decimal DividendGoalPerYear { get; set; }
    }
}
