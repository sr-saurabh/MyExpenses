using MyExpenses.Domain.core.Entities.Base;
using MyExpenses.Domain.core.Entities.User;

namespace MyExpenses.Domain.core.Entities.Expenses
{
    /// <summary>
    /// Represents an expense incurred by an individual user.
    /// </summary>
    public class Activity : AuditableEntity
    {
        /// <summary>
        /// Gets or sets the ID of the user associated with this expense.
        /// </summary>
        public int AppUserId { get; set; }
        
        /// <summary>
        /// Gets or sets the ID of the user associated with this expense.
        /// </summary>
        public int TransactionId{ get; set; }

        /// <summary>
        /// Gets or sets the description of the expense.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the categoryId of the expense.
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category of the expense.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the user who incurred this expense.
        /// </summary>
        public AppUser User { get; set; }
        
        /// <summary>
        /// Gets or sets the account for the expense.
        /// </summary>
        public Transaction Transaction { get; set; }
    }
}
