using System.Diagnostics.CodeAnalysis;

namespace SmartInvestor.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
