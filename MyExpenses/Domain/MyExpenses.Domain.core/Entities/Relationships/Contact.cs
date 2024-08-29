using MyExpenses.Domain.core.Entities.Base;
using MyExpenses.Domain.core.Entities.Enums;
using MyExpenses.Domain.core.Entities.User;

namespace MyExpenses.Domain.core.Entities.Relationships
{
    /// <summary>
    /// Represents a contact relationship between two users, including the status and balance.
    /// </summary>
    public class Contact : AuditableEntity
    {
        /// <summary>
        /// Gets or sets the ID of the user who is the recipient of the contact request.
        /// </summary>
        public int ToUserId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who initiated the contact request.
        /// </summary>
        public int FromUserId { get; set; }

        /// <summary>
        /// Gets or sets the status of the contact invitation.
        /// </summary>
        public ContactInvitationStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the balance associated with the contact, if any.
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Gets or sets the user who is the recipient of the contact request.
        /// </summary>
        public AppUser ToUser { get; set; }

        /// <summary>
        /// Gets or sets the user who initiated the contact request.
        /// </summary>
        public AppUser FromUser { get; set; }
    }
}
