using AutoMapper;
using MyExpenses.Domain.core.Entities.Common;
using MyExpenses.Domain.core.Models.Category;

namespace MyExpenses.Domain.core.MappingProfile
{
    public class CategoryMapperProfile:Profile
    {
        public CategoryMapperProfile()
        {
            CreateMap<Category, ApiCategory>();
        }
    }
}
