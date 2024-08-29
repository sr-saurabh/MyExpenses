using MyExpenses.Domain.core.Models.UserExpense;

namespace MyExpenses.Application.Abstraction
{
    public interface IUserExpenseContract
    {
        Task<ApiUserExpense> AddUserExpense(CreateUserExpense userExpense);
        Task<ApiUserExpense> GetUserExpense(int id);
        Task<ApiUserExpenseWithSummary> GetUserExpenses(int fromUserId, int toUserId);
        Task<bool> UpdateUserExpense(UpdateUserExpense userExpense);
        Task<bool> DeleteUserExpense(int id);
    }
}
