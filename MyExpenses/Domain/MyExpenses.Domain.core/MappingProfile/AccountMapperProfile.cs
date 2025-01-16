using AutoMapper;
using MyExpenses.Domain.core.Entities.Common;
using MyExpenses.Domain.core.Models.Accounts;

namespace MyExpenses.Domain.core.MappingProfile
{
    public class AccountMapperProfile:Profile
    {
        public AccountMapperProfile()
        {
            CreateMap<Account, CreateAccount>().ReverseMap();
            CreateMap<Account, UpdateAccount>().ReverseMap();
            CreateMap<Account, ApiAccount>().ReverseMap();
        }
    }
}
