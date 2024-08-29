using MyExpenses.Domain.core.Entities.Base;
using MyExpenses.Domain.core.Entities.Group;
using MyExpenses.Domain.core.Entities.User;

namespace MyExpenses.Domain.core.Entities.Relationships
{
    /// <summary>
    /// Represents a membership of a user in a group.
    /// </summary>
    public class UserGroupMembership : AuditableEntity
    {
        /// <summary>
        /// Gets or sets the ID of the group to which the user belongs.
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Gets or sets the group associated with this membership.
        /// </summary>
        public UserGroup Group { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who is a member of the group.
        /// </summary>
        public int AppUserId { get; set; }

        /// <summary>
        /// Gets or sets the user who is a member of the group.
        /// </summary>
        public AppUser AppUser { get; set; }
    }
}
