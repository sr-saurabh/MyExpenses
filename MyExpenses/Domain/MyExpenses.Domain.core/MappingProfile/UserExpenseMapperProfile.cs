using AutoMapper;
using MyExpenses.Domain.core.Entities.Expenses;
using MyExpenses.Domain.core.Models.UserExpense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Domain.core.MappingProfile
{
    public class UserExpenseMapperProfile:Profile
    {
        public UserExpenseMapperProfile()
        {
            CreateMap<ApiUserExpense, UserExpense>().ReverseMap();
            CreateMap<CreateUserExpense, UserExpense>().ReverseMap();
            CreateMap<UpdateUserExpense, UserExpense>().ReverseMap();
        }
    }
}
