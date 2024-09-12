using AutoMapper;
using MyExpenses.Domain.core.Entities.Expenses;
using MyExpenses.Domain.core.Models.Expense;
using MyExpenses.Domain.core.Models.PersonalExpense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Domain.core.MappingProfile
{
    public class PersonalExpenseMapperProfile :Profile
    {
        public PersonalExpenseMapperProfile()
        {
            CreateMap<PersonalExpenses, CreatePersonalExpense>().ReverseMap();
            CreateMap<PersonalExpenses, UpdatePersonalExpense>().ReverseMap();
            CreateMap<PersonalExpenses, ApiPersonalExpense>().ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));


        }
    }
}
