using AutoMapper;
using BankPortalAggregator.Models;

namespace BankPortalAggregator.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Deposit, DepositDto>().ForMember(dto => dto.Bank, conf => conf.MapFrom(ol => ol.Bank.Name));
            CreateMap<DepositVariation, DepositVariationDto>().ReverseMap();
        }
    }
}
