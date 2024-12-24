using MyExpenses.Domain.core.Entities.Base;
using MyExpenses.Domain.core.Entities.Enums;
using MyExpenses.Domain.core.Entities.Expenses;
using MyExpenses.Domain.core.Entities.User;

namespace MyExpenses.Domain.core.Entities.Common
{
    public class Account : AuditableEntity
    {
        public int AppUserId { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string BranchName { get; set; }
        public string IFSC { get; set; }
        public AccountType AccountType { get; set; }
        public decimal Balance { get; set; }

        public AppUser AppUser { get; set; }

        public ICollection<PersonalExpenses>? PersonalExpenses { get; set; }
    }
}
