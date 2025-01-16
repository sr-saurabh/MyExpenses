using AutoMapper;
using MyExpenses.Domain.core.Entities.Common;
using MyExpenses.Domain.core.Models.Category;
using MyExpenses.Domain.core.Models.Goals;

namespace MyExpenses.Domain.core.MappingProfile
{
    public class GoalMapperProfile:Profile
    {
        public GoalMapperProfile()
        {
            CreateMap<Goal,ApiGoal>()
                .ForMember(g=>g.CategoryName, g=>g.MapFrom(g=>g.Category.CategoryName))
                .ForMember(g=>g.CategoryId, g=>g.MapFrom(g=>g.Category.Id));
            CreateMap<Goal,CreateGoal>();
            CreateMap<Goal,UpdateGoal>();
        }
    }
}
