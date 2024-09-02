using AutoMapper;
using MyExpenses.Domain.core.Entities.Settlement;
using MyExpenses.Domain.core.Models.Settlement;

namespace MyExpenses.Domain.core.MappingProfile
{
    public class SettlementMapperProfile :Profile
    {
        public SettlementMapperProfile()
        {
            CreateMap<CreateSettlement, SettlementHistory>().ReverseMap();
            CreateMap<UpdateSettlement, SettlementHistory>().ReverseMap();
        }
    }
}
