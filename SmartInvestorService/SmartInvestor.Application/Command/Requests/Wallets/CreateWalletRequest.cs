using MediatR;
using SmartInvestor.Application.Command.Responses.Wallets;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SmartInvestor.Application.Command.Requests.Wallets
{
    [ExcludeFromCodeCoverage]
    public class CreateWalletRequest : IRequest<CreateWalletResponse>
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public decimal DividendGoalPerYear { get; set; }
    }
}
