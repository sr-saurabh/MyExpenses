using MyExpenses.Domain.core.Entities.Base;
using MyExpenses.Domain.core.Entities.Common;
using MyExpenses.Domain.core.Entities.Enums;

namespace MyExpenses.Domain.core.Entities.Expenses
{
    public class Transaction : AuditableEntity
    {
        public int? ActivityId { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime Date { get; set; }
        public Account Account { get; set; }
        public Activity? Activity { get; set; }

    }
}
