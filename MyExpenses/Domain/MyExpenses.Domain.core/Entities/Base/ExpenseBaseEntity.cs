namespace MyExpenses.Domain.core.Entities.Base
{
    public class ExpenseBaseEntity:AuditableEntity
    {

        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }

    }
}
