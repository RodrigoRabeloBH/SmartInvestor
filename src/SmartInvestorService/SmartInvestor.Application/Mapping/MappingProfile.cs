using AutoMapper;
using SmartInvestor.Application.Command.Requests.Wallets;
using SmartInvestor.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace SmartInvestor.Application.Mapping
{
    [ExcludeFromCodeCoverage]
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateStockPlanningRequest, StockPlanning>()
                .ForMember(d => d.CreatedDate, o => o.MapFrom(s => DateTime.UtcNow));

            CreateMap<IncrementStockRequest, StockPlanning>()
                .ForMember(d => d.CreatedDate, o => o.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.CurrentQuantity, o => o.MapFrom(s => s.Quantity));

            CreateMap<UpdateWalletRequest, Wallet>().ReverseMap();
        }
    }
}
