using AutoMapper;
using MyExpenses.Domain.core.Entities.Expenses;
using MyExpenses.Domain.core.Models.Expense;
using MyExpenses.Domain.core.Models.PersonalExpense;

namespace MyExpenses.Domain.core.MappingProfile
{
    public class ActivityMapperProfile :Profile
    {
        public ActivityMapperProfile()
        {
            CreateMap<Activity, CreateActivity>().ReverseMap();
            CreateMap<Activity, UpdateActivity>().ReverseMap();

            CreateMap<Activity, ApiActivity>().ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.Transaction.TransactionType.ToString())).ForMember(dest=>dest.Amount, opt=>opt.MapFrom(src=>src.Transaction.Amount)).ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.Transaction.AccountId)).ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Transaction.Date));



            //for transaction
            CreateMap<Transaction, ApiTransaction>();

        }
    }
}
