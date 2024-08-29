using MyExpenses.Domain.core.Entities.Base;
using MyExpenses.Domain.core.Entities.Group;
using MyExpenses.Domain.core.Entities.User;

namespace MyExpenses.Domain.core.Entities.Expenses
{
    /// <summary>
    /// Represents an expense associated with a group.
    /// </summary>
    public class GroupExpenses : AuditableEntity
    {
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
        /// Gets or sets the ID of the group associated with the expense.
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who paid the expense.
        /// </summary>
        public int PayerId { get; set; }

        /// <summary>
        /// Gets or sets the group associated with the expense.
        /// </summary>
        public UserGroup Group { get; set; }

        /// <summary>
        /// Gets or sets the user who paid the expense.
        /// </summary>
        public AppUser Payer { get; set; }

        /// <summary>
        /// Gets or sets the collection of shares associated with the group expense.
        /// </summary>
        public IEnumerable<GroupExpenseShare> GroupExpenseShares { get; set; }
    }
}
