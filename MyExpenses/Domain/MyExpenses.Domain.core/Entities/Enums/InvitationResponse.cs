namespace MyExpenses.Domain.core.Entities.Enums
{
    /// <summary>
    /// Represents the possible responses to an invitation.
    /// </summary>
    public enum InvitationResponse
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
        Rejected,

        /// <summary>
        /// The invitation has expired and is no longer valid.
        /// </summary>
        Expired
    }
}
