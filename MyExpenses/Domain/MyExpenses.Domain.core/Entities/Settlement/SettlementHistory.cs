using MyExpenses.Domain.core.Entities.Base;
using MyExpenses.Domain.core.Entities.Enums;
using MyExpenses.Domain.core.Entities.Group;
using MyExpenses.Domain.core.Entities.User;
using System;

namespace MyExpenses.Domain.core.Entities.Settlement
{
    /// <summary>
    /// Represents the history of a financial settlement between users or within a group.
    /// </summary>
    public class SettlementHistory : AuditableEntity
    {
        /// <summary>
        /// Gets or sets the ID of the user who initiated the settlement.
        /// </summary>
        public int FromUserId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who received the settlement.
        /// </summary>
        public int ToUserId { get; set; }

        /// <summary>
        /// Gets or sets the amount of the settlement.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the date when the settlement was made.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the ID of the group associated with this settlement, if applicable.
        /// </summary>
        public int? GroupId { get; set; }

        /// <summary>
        /// Gets or sets the description of the settlement.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the status of the settlement.
        /// </summary>
        public SettlementStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the user who initiated the settlement.
        /// </summary>
        public AppUser FromUser { get; set; }

        /// <summary>
        /// Gets or sets the user who received the settlement.
        /// </summary>
        public AppUser ToUser { get; set; }

        /// <summary>
        /// Gets or sets the group associated with this settlement, if applicable.
        /// </summary>
        public UserGroup? Group { get; set; }
    }
}
