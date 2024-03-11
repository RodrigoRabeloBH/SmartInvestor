using MediatR;
using SmartInvestor.Application.Command.Responses.Wallets;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SmartInvestor.Application.Command.Requests.Wallets
{
    [ExcludeFromCodeCoverage]
    public class CreateStockPlanningRequest : IRequest<CreateStockPlanningResponse>
    {
        [Required]
        public Guid WalletId { get; set; }

        [Required]
        public string Ticket { get; set; }

        [Required]
        public decimal Weight { get; set; }

        [Required]
        public int CurrentQuantity { get; set; }

        [Required]
        public decimal PurchasePrice { get; set; }

        [Required]
        public decimal ProjectedYield { get; set; }

        [Required]
        public decimal MinimumYieldRequired { get; set; }
    }
}
