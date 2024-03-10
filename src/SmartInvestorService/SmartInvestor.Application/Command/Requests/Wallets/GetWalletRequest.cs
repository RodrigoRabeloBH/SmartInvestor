using MediatR;
using SmartInvestor.Application.Command.Responses.Wallets;
using System.Diagnostics.CodeAnalysis;

namespace SmartInvestor.Application.Command.Requests.Wallets
{
    [ExcludeFromCodeCoverage]
    public class GetWalletRequest : IRequest<GetWalletResponse>
    {
        public Guid UserId { get; set; }
    }
}
