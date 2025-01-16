using MyExpenses.Domain.core.Entities.Base;

namespace MyExpenses.Domain.core.Entities.Common
{
    public class Category : AuditableEntity
    {
        public string CategoryName { get; set; }
    }
}
