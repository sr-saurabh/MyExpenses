using AutoMapper;
using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Entities.Common;
using MyExpenses.Domain.core.Models.Category;
using MyExpenses.Domain.core.Repositories;

namespace MyExpenses.Application.Providers
{
    public class CategoryProvider : ICategoryContract
    {
        public ICategoryRepo _categoryRepo { get; set; }
        private readonly IMapper _mapper;


        public CategoryProvider(ICategoryRepo categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }
        //public async Task<bool> CreateAll(int userId, int month, int year)
        //{
        //    //var  isCategoryCreated= _categoryRepo.Search(c=>c.Year == year && c.Month==month && c.AppUserId==userId).Any();
        //    //if(isCategoryCreated)
        //    //    return true;
        //    //List<string> categories = ["Housing", "Food", "Transportation", "Entertainment", "Shopping", "Others"];
        //    //Category newCategory = new Category()
        //    //{
        //    //    AppUserId = userId,
        //    //    CategoryName = "",
        //    //    Target = 0,
        //    //    Progress = 0,
        //    //    Year= year,
        //    //    Month = month,
        //    //};

        //    //foreach (var category in categories)
        //    //{
        //    //    newCategory.CategoryName = category;
        //    //    var res = await _categoryRepo.CreateAsync(new Category()
        //    //    {
        //    //        AppUserId = userId,
        //    //        CategoryName = category,
        //    //        Target = 0,
        //    //        Progress = 0,
        //    //        Year = year,
        //    //        Month = month,
        //    //    });
        //    //}

        //    return true;
        //}

        public async Task<List<ApiCategory>> GetAllCategory()
        {
            var categories = await _categoryRepo.GetAllAsync();
            return _mapper.Map<List<ApiCategory>>(categories);
        }
    }
}
