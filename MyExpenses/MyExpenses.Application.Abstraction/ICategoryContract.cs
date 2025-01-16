using MyExpenses.Domain.core.Models.Category;

namespace MyExpenses.Application.Abstraction
{
    public interface ICategoryContract
    {
        Task<List<ApiCategory>> GetAllCategory();
    }
}
