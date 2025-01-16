using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Entities.Common;
using MyExpenses.Domain.core.Repositories;
using MyExpenses.Infrastructure.Postgres.Repositories.BaseRepo;

namespace MyExpenses.Infrastructure.Postgres.Repositories
{
    public class CategoryRepo : AuditableRepo<Category>, ICategoryRepo
    {
        private readonly IAuthHelperContract _authHelper;
        private readonly MyExpensesDbContext _context;


        public CategoryRepo(MyExpensesDbContext dbContext, IAuthHelperContract authHelper) : base(dbContext, authHelper)
        {
            _context = dbContext;
            _authHelper = authHelper;
        }      
    }
}
