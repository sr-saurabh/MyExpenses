using MyExpenses.Domain.core.Entities.Base;
using MyExpenses.Domain.core.Entities.User;

namespace MyExpenses.Domain.core.Entities.Common
{
    public class Category : AuditableEntity
    {
        public int AppUserId { get; set; }
        public string Name { get; set; }

        public int MyProperty { get; set; }
        public int Budget { get; set; }
        public AppUser AppUser{ get; set; }

    }
}
