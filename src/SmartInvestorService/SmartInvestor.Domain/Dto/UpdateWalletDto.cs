using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SmartInvestor.Domain.Dto
{
    [ExcludeFromCodeCoverage]
    public class UpdateWalletDto
    {
        [Required]
        public Guid WalletId { get; set; }

        [Required]
        public decimal DividendGoalPerYear { get; set; }
    }
}
