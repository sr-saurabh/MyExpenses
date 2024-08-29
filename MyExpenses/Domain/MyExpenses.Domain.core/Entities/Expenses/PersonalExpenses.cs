using MyExpenses.Domain.core.Entities.Base;
using MyExpenses.Domain.core.Entities.Enums;
using MyExpenses.Domain.core.Entities.User;

namespace MyExpenses.Domain.core.Entities.Expenses
{
    /// <summary>
    /// Represents an expense incurred by an individual user.
    /// </summary>
    public class PersonalExpenses : AuditableEntity
    {
        /// <summary>
        /// Gets or sets the type of the transaction (credit or debit).
        /// </summary>
        public TransactionType Type { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user associated with this expense.
        /// </summary>
        public int AppUserId { get; set; }

        /// <summary>
        /// Gets or sets the description of the expense.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the category of the expense.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the date of the expense.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the amount of the expense.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the user who incurred this expense.
        /// </summary>
        public AppUser User { get; set; }
    }
}
