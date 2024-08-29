using MyExpenses.Domain.core.Entities.Base;
using MyExpenses.Domain.core.Entities.Expenses;
using MyExpenses.Domain.core.Entities.Relationships;
using MyExpenses.Domain.core.Entities.Settlement;
using System.Collections.Generic;

namespace MyExpenses.Domain.core.Entities.Group
{
    /// <summary>
    /// Represents a group to which users can belong, with associated expenses and settlements.
    /// </summary>
    public class UserGroup : AuditableEntity
    {
        /// <summary>
        /// Gets or sets the name of the group.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets the picture associated with the group.
        /// </summary>
        public string GroupPicture { get; set; }

        /// <summary>
        /// Gets or sets the title of the group.
        /// </summary>
        public string GroupTitle { get; set; }

        /// <summary>
        /// Gets or sets the collection of memberships within the group.
        /// </summary>
        public ICollection<UserGroupMembership> GroupMemberships { get; set; }

        /// <summary>
        /// Gets or sets the collection of expenses associated with the group.
        /// </summary>
        public ICollection<GroupExpenses> GroupExpenses { get; set; }

        /// <summary>
        /// Gets or sets the collection of settlement histories associated with the group.
        /// </summary>
        public ICollection<SettlementHistory> SettlementHistories { get; set; }
    }
}
