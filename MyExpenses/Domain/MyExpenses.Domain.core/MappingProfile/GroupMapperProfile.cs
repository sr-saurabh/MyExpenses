using AutoMapper;
using MyExpenses.Domain.core.Entities.Group;
using MyExpenses.Domain.core.Models.Group;

namespace MyExpenses.Domain.core.MappingProfile
{
    public class GroupMapperProfile:Profile
    {
        public GroupMapperProfile()
        {
            CreateMap<UserGroup, CreateGroup>().ReverseMap();
            CreateMap<UserGroup, UpdateGroup>().ReverseMap();
            CreateMap<UserGroup, ApiUserGroup>();
        }
    }
}
