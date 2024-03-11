using System.Diagnostics.CodeAnalysis;

namespace SmartInvestor.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class StockPlanning : Entity
    {
        public string Ticket { get; set; }
        public decimal Weight { get; set; }
        public decimal CurrentQuantity { get; set; }
        public decimal RequiredQuantity { get; set; }
        public decimal CeilingPrice { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal TotalAmmountInvested { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal CurrentTotalAmmountInvested { get; set; }
        public decimal ProjectedYield { get; set; }
        public decimal MinimumYieldRequired { get; set; }
        public decimal CurrentYield { get; set; }
        public bool Buy { get; set; }
        public decimal Goal { get; set; }
        public string LogoUrl { get; set; }

        public Guid WalletId { get; set; }
        public Wallet Wallet { get; set; }
    }
}
