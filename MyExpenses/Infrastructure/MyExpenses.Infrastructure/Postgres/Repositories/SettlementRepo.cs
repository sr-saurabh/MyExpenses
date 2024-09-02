using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Entities.Settlement;
using MyExpenses.Domain.core.Repositories;
using MyExpenses.Infrastructure.Postgres.Repositories.BaseRepo;

namespace MyExpenses.Infrastructure.Postgres.Repositories
{
    public class SettlementRepo : AuditableRepo<SettlementHistory>, ISettlementRepo
    {
        public SettlementRepo(MyExpensesDbContext dbContext, IAuthHelperContract authHelper) : base(dbContext, authHelper)
        {
        }
    }
}
