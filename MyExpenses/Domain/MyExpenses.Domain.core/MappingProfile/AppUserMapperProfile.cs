using AutoMapper;
using MyExpenses.Domain.core.Entities.User;
using MyExpenses.Domain.core.Models.AppUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Domain.core.MappingProfile
{
    public class AppUserMapperProfile:Profile
    {
        public AppUserMapperProfile()
        {
            CreateMap<CreateAppUser, AppUser>().ReverseMap();
            CreateMap<ApiAppUser, AppUser>().ReverseMap();
        }
    }
}
