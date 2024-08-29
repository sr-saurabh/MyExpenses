namespace MyExpenses.Domain.core.Entities.Enums
{
    /// <summary>
    /// Represents the status of a contact invitation.
    /// </summary>
    public enum ContactInvitationStatus
    {
        /// <summary>
        /// The invitation is pending and has not yet been responded to.
        /// </summary>
        Pending,

        /// <summary>
        /// The invitation has been accepted.
        /// </summary>
        Accepted,

        /// <summary>
        /// The invitation has been rejected.
        /// </summary>
        Rejected
    }
}
