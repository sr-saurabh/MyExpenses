using MyExpenses.Domain.core.Entities.Common;
using MyExpenses.Domain.core.Repositories.Base;

namespace MyExpenses.Domain.core.Repositories
{
    public interface ICategoryRepo: IAuditableRepo<Category>
    {
    }
}

