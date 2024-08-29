using AutoMapper;
using MyExpenses.Domain.core.Entities.Expenses;
using MyExpenses.Domain.core.Models.GroupExpense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Domain.core.MappingProfile
{
    public class GroupExpenseMapperProfile:Profile
    {
        public GroupExpenseMapperProfile()
        {
                CreateMap<GroupExpenses, CreateGroupExpense>().ReverseMap();
        }
    }
}
