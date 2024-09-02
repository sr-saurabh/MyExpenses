using MyExpenses.Domain.core.Entities.Settlement;
using MyExpenses.Domain.core.Repositories.Base;

namespace MyExpenses.Domain.core.Repositories
{
    public interface ISettlementRepo : IAuditableRepo<SettlementHistory>
    {
    }
}
