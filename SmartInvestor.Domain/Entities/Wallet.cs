using System.Diagnostics.CodeAnalysis;

namespace SmartInvestor.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Wallet : Entity
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public decimal DividendGoalPerYear { get; set; }
        public decimal AmmountInvested { get; set; }
        public decimal CurrentAmmountInvested { get; set; }
        public decimal Appreciation { get; set; }
        public List<StockPlanning> Stocks { get; set; } = new List<StockPlanning>();

        public void SetValues()
        {
            if (Stocks.Any())
            {
                foreach (var stock in Stocks)
                {
                    stock.RequiredQuantity = Math.Round(DividendGoalPerYear * (stock.Weight / 100) / stock.ProjectedYield);

                    stock.CeilingPrice = stock.ProjectedYield / stock.MinimumYieldRequired;

                    stock.TotalAmmountInvested = stock.PurchasePrice != 0
                        ? stock.PurchasePrice * stock.CurrentQuantity
                        : stock.AveragePrice * stock.CurrentQuantity;

                    stock.AveragePrice = stock.CurrentQuantity == 0 ? 0 : stock.TotalAmmountInvested / stock.CurrentQuantity;

                    stock.CurrentTotalAmmountInvested = stock.CurrentQuantity * stock.CurrentPrice;

                    stock.Buy = stock.CurrentPrice < stock.CeilingPrice && stock.CurrentQuantity < stock.RequiredQuantity;

                    stock.Goal = stock.RequiredQuantity == 0 ? 0 : stock.CurrentQuantity / stock.RequiredQuantity * 100;
                }

                AmmountInvested = Stocks.Sum(stock => stock.TotalAmmountInvested);

                CurrentAmmountInvested = Stocks.Sum(s => s.CurrentTotalAmmountInvested);

                Appreciation = CurrentAmmountInvested == 0 ? 0 : (1 - (AmmountInvested / CurrentAmmountInvested)) * 100;
            }
        }

        public void IncremetQuantity(int quantity, decimal purchasePrice, StockPlanning stock)
        {
            stock.TotalAmmountInvested = (stock.AveragePrice * stock.CurrentQuantity) + (quantity * purchasePrice);

            stock.CurrentQuantity += quantity;

            stock.AveragePrice = stock.TotalAmmountInvested / stock.CurrentQuantity;

            stock.CurrentTotalAmmountInvested = stock.CurrentQuantity * stock.CurrentPrice;
        }

        public void DecremetQuantity(int quantity, decimal purchasePrice, StockPlanning stock)
        {
            stock.TotalAmmountInvested = (stock.AveragePrice * stock.CurrentQuantity) - (quantity * purchasePrice);

            stock.CurrentQuantity -= quantity;

            stock.CurrentQuantity = stock.CurrentQuantity <= 0 ? 0 : stock.CurrentQuantity;

            stock.AveragePrice = stock.CurrentQuantity <= 0 ? 0 : stock.TotalAmmountInvested / stock.CurrentQuantity;

            stock.CurrentTotalAmmountInvested = stock.CurrentQuantity * stock.CurrentPrice;
        }
    }
}
