using Microsoft.EntityFrameworkCore;
using SmartInvestor.Domain.Entities;

namespace SmartInvestor.Infrastructure.Data
{
    public class SmartInvestorDbContext : DbContext
    {
        public DbSet<Wallet> Wallet { get; set; }
        public DbSet<StockPlanning> StockPlanning { get; set; }
        public SmartInvestorDbContext(DbContextOptions<SmartInvestorDbContext> options) : base(options) { }
    }
}
