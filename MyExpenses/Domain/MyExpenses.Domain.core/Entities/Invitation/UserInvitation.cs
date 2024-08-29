using MyExpenses.Domain.core.Entities.Base;
using MyExpenses.Domain.core.Entities.Enums;
using System;

namespace MyExpenses.Domain.core.Entities.Invitation
{
    /// <summary>
    /// Represents an invitation sent to a user, including the inviter's profile and the invitation details.
    /// </summary>
    public class UserInvitation : AuditableEntity
    {
        /// <summary>
        /// Gets or sets the ID of the profile that sent the invitation.
        /// </summary>
        public int InviterProfileId { get; set; }

        /// <summary>
        /// Gets or sets the name of the person being invited.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email address of the person being invited.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the person being invited.
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Gets or sets the date when the invitation was sent.
        /// </summary>
        public DateTime InvitationDate { get; set; }

        /// <summary>
        /// Gets or sets the response status of the invitation.
        /// </summary>
        public InvitationResponse InvitationResponse { get; set; }
    }
}
