using MyExpenses.Domain.core.Entities.Base;
using MyExpenses.Domain.core.Entities.User;

namespace MyExpenses.Domain.core.Entities.Expenses
{
    /// <summary>
    /// Represents a share of a group expense allocated to a specific user.
    /// </summary>
    public class GroupExpenseShare : AuditableEntity
    {
        /// <summary>
        /// Gets or sets the ID of the group expense associated with this share.
        /// </summary>
        public int GroupExpenseId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who will receive this share.
        /// </summary>
        public int ReceiverId { get; set; }

        /// <summary>
        /// Gets or sets the amount of the share allocated to the receiver.
        /// </summary>
        public decimal ShareAmount { get; set; }

        /// <summary>
        /// Gets or sets the group expense associated with this share.
        /// </summary>
        public GroupExpenses GroupExpense { get; set; }

        /// <summary>
        /// Gets or sets the user who will receive this share.
        /// </summary>
        public AppUser Reciever { get; set; }
    }
}
