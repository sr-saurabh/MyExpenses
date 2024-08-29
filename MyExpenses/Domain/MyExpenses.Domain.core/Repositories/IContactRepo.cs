using MyExpenses.Domain.core.Entities.Relationships;
using MyExpenses.Domain.core.Models.Contact;
using MyExpenses.Domain.core.Repositories.Base;

namespace MyExpenses.Domain.core.Repositories
{
    /// <summary>
    /// Defines the contract for a repository that manages Contact entities, extending the generic auditable repository interface.
    /// Provides methods for managing contact requests and retrieving contacts.
    /// </summary>
    public interface IContactRepo : IAuditableRepo<Contact>
    {
        /// <summary>
        /// Accepts a contact request by the given contact ID.
        /// </summary>
        /// <param name="contactId">The ID of the contact request to accept.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a boolean indicating whether the request was successfully accepted.</returns>
        Task<bool> AcceptRequest(int contactId);

        /// <summary>
        /// Rejects a contact request by the given contact ID.
        /// </summary>
        /// <param name="contactId">The ID of the contact request to reject.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a boolean indicating whether the request was successfully rejected.</returns>
        Task<bool> RejectRequest(int contactId);

        /// <summary>
        /// Retrieves a list of contact requests sent to the specified user.
        /// </summary>
        /// <param name="toUserId">The ID of the user to whom the contact requests were sent.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a list of contact requests.</returns>
        Task<List<ContactRequest>> GetRequest(int toUserId);

        /// <summary>
        /// Retrieves a list of contacts associated with the specified user.
        /// </summary>
        /// <param name="userId">The ID of the user whose contacts are to be retrieved.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a list of contacts.</returns>
        Task<List<MyContact>> GetContacts(int userId);
    }
}

