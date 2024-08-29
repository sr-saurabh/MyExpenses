using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Entities.Expenses;
using MyExpenses.Domain.core.Models.UserExpense;
using MyExpenses.Domain.core.Repositories;
using MyExpenses.Infrastructure.Postgres.Repositories.BaseRepo;

namespace MyExpenses.Infrastructure.Postgres.Repositories
{
    public class UserExpenseRepo : AuditableRepo<UserExpense>, IUserExpenseRepo
    {

        public UserExpenseRepo(MyExpensesDbContext dbContext, IAuthHelperContract authHelper) : base(dbContext, authHelper)
        {
        }

        public Task<ApiUserExpenseWithSummary> GetAllUserExpenses(int fromUserId, int toUserId)
        {
            //var query = from userExpense in _dbContext.UserExpenses
            //            join expense in _dbContext.Expenses on userExpense.ExpenseId equals expense.Id
            //            where userExpense.AppUserId == userId
            //            select new ApiUserExpense
            //            {
            //                Id = userExpense.Id,
            //                Amount = userExpense.Amount,
            //                ExpenseId = userExpense.ExpenseId,
            //                ExpenseName = expense.Name,
            //                CreatedAt = userExpense.CreatedAt,
            //                UpdatedAt = userExpense.UpdatedAt
            //            };
            //return query;

            var fromUserAmount = _dbContext.UserExpenses.Where(x => x.FromUserId == fromUserId && x.ToUserId == toUserId).Sum(x => x.Amount);
            var toUserAmount = _dbContext.UserExpenses.Where(x => x.FromUserId == toUserId && x.ToUserId == fromUserId).Sum(x => x.Amount);

            var userExpenses = _dbContext.UserExpenses.Where(x => (x.FromUserId == fromUserId && x.ToUserId == toUserId) || (x.FromUserId == toUserId && x.ToUserId == fromUserId)).ToList();
            //ApiUserExpenseWithSummary apiUserExpenseWithSummary= new ApiUserExpenseWithSummary
            //{
            //    ApiUserExpenses = 
            //};


            return null;
        }
    }
}
