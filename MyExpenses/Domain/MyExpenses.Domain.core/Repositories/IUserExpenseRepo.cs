using MyExpenses.Domain.core.Entities.Expenses;
using MyExpenses.Domain.core.Models.UserExpense;
using MyExpenses.Domain.core.Repositories.Base;

namespace MyExpenses.Domain.core.Repositories
{
    public interface IUserExpenseRepo: IAuditableRepo<UserExpense>
    {
        //Task<ApiUserExpenseWithSummary> GetAllUserExpenses(int fromUserId, int toUserId);
    }
}
