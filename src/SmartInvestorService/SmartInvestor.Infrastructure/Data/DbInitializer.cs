using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace SmartInvestor.Infrastructure.Data
{
    [ExcludeFromCodeCoverage]
    public class DbInitializer
    {
        public static void InitializeDb(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<SmartInvestorDbContext>();

            context.Database.Migrate();
        }
    }
}
