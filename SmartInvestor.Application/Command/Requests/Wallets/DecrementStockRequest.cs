using MediatR;
using SmartInvestor.Application.Command.Responses.Wallets;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SmartInvestor.Application.Command.Requests.Wallets
{
    [ExcludeFromCodeCoverage]
    public class DecrementStockRequest : IRequest<DecrementStockResponse>
    {
        [Required]
        public Guid WalletId { get; set; }

        [Required]
        public string Ticket { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal SellingPrice { get; set; }
    }
}
