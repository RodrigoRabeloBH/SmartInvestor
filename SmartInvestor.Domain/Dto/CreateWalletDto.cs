using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SmartInvestor.Domain.Dto
{
    [ExcludeFromCodeCoverage]
    public class CreateWalletDto
    {
        [Required]
        public decimal DividendGoalPerYear { get; set; }
    }
}
