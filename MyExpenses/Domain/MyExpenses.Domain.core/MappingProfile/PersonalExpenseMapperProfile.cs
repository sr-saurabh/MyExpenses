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
            CreateMap<Transaction, CreateActivity>().ReverseMap();
            CreateMap<Transaction, UpdateActivity>().ReverseMap();
            CreateMap<Transaction, ApiActivity>().ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.TransactionType.ToString()));


        }
    }
}
