using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Entities.Expenses;
using MyExpenses.Domain.core.Models.GroupExpense;
using MyExpenses.Domain.core.Repositories;
using MyExpenses.Infrastructure.Postgres.Repositories.BaseRepo;

namespace MyExpenses.Infrastructure.Postgres.Repositories
{
    /// <summary>
    /// Repository for managing operations related to the <see cref="GroupExpenses"/> entity.
    /// </summary>
    public class GroupExpenseRepo : AuditableRepo<GroupExpenses>, IGroupExpenseRepo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupExpenseRepo"/> class.
        /// </summary>
        /// <param name="dbContext">The database context used for interacting with the database.</param>
        /// <param name="authHelper">The authentication helper contract used for user authentication tasks.</param>
        public GroupExpenseRepo(MyExpensesDbContext dbContext, IAuthHelperContract authHelper) : base(dbContext, authHelper)
        {
        }

        /// <summary>
        /// Retrieves a specific group expense by its identifier.
        /// </summary>
        /// <param name="groupExpenseId">The identifier of the group expense to retrieve.</param>
        /// <returns>An <see cref="IQueryable{ApiGroupExpense}"/> representing the group expense.</returns>
        public async Task<IQueryable<ApiGroupExpense>> GetGroupExpense(int groupExpenseId)
        {
            var query = from ge in _dbContext.GroupExpenses
                        join es in _dbContext.GroupExpenseShares on ge.Id equals es.GroupExpenseId
                        join u in _dbContext.AppUsers on ge.PayerId equals u.Id
                        join r in _dbContext.AppUsers on es.ReceiverId equals r.Id
                        select new ApiGroupExpense
                        {
                            GroupExpenseId = ge.Id,
                            GroupId = ge.GroupId,
                            Description = ge.Description,
                            TotalAmount = ge.Amount,
                            ExpenseDate = ge.Date,
                            PaidById = ge.PayerId,
                            PaidByName = u.FullName,
                            PaidByAvatar = u.Avatar, // Assuming Avatar exists in AppUser
                            GroupExpenseShares = (from es in _dbContext.GroupExpenseShares
                                                  join appuser in _dbContext.AppUsers on es.ReceiverId equals appuser.Id
                                                  where es.GroupExpenseId == ge.Id
                                                  select new ApiGroupExpenseShare
                                                  {
                                                      Id = es.Id,
                                                      ReceiverId = es.ReceiverId,
                                                      ShareAmount = es.ShareAmount,
                                                      DebitorName = appuser.FullName,
                                                      DebitorAvatar = appuser.Avatar // Assuming Avatar exists in AppUser
                                                  }).ToList()
                        };

            return query;
        }

        /// <summary>
        /// Retrieves a list of group expenses for a specific group.
        /// </summary>
        /// <param name="groupId">The identifier of the group whose expenses are to be retrieved.</param>
        /// <returns>An <see cref="IQueryable{ApiGroupExpense}"/> representing the group expenses.</returns>
        public async Task<IQueryable<ApiGroupExpense>> GetGroupExpenses(int groupId)
        {
            var query = from ge in _dbContext.GroupExpenses
                        join u in _dbContext.AppUsers on ge.PayerId equals u.Id
                        join es in _dbContext.GroupExpenseShares on ge.Id equals es.GroupExpenseId
                        join r in _dbContext.AppUsers on es.ReceiverId equals r.Id
                        where ge.GroupId == groupId
                        group new { ge, u, es, r } by new
                        {
                            ge.Id,
                            ge.GroupId,
                            ge.Description,
                            ge.Amount,
                            ge.Date,
                            u.FullName,
                            u.Avatar
                        } into g
                        select new ApiGroupExpense
                        {
                            GroupExpenseId = g.Key.Id,
                            GroupId = g.Key.GroupId,
                            Description = g.Key.Description,
                            TotalAmount = g.Key.Amount,
                            ExpenseDate = g.Key.Date,
                            PaidById = g.Key.Id,
                            PaidByName = g.Key.FullName,
                            PaidByAvatar = g.Key.Avatar,
                            GroupExpenseShares = g.Select(x => new ApiGroupExpenseShare
                            {
                                Id = x.es.Id,
                                ReceiverId = x.es.ReceiverId,
                                ShareAmount = x.es.ShareAmount,
                                DebitorName = x.r.FullName,
                                DebitorAvatar = x.r.Avatar
                            }).ToList()
                        };

            return query;
        }
    }
}
