using MyExpenses.Domain.core.Models.AppUser;
using MyExpenses.Domain.core.Models.Contact;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyExpenses.Application.Abstraction
{
    /// <summary>
    /// Defines the contract for managing contacts and contact requests.
    /// </summary>
    public interface IContactContract
    {
        /// <summary>
        /// Adds a contact request from one user to another.
        /// </summary>
        /// <param name="fromUserId">The ID of the user sending the contact request.</param>
        /// <param name="toUserId">The ID of the user receiving the contact request.</param>
        /// <returns><c>true</c> if the contact request was successfully added; otherwise, <c>false</c>.</returns>
        Task<bool> AddContact(int fromUserId, int toUserId);

        /// <summary>
        /// Removes a contact by its ID.
        /// </summary>
        /// <param name="contactId">The ID of the contact to remove.</param>
        /// <returns><c>true</c> if the contact was successfully removed; otherwise, <c>false</c>.</returns>
        Task<bool> RemoveContact(int contactId);

        /// <summary>
        /// Accepts a contact request by its ID.
        /// </summary>
        /// <param name="contactId">The ID of the contact request to accept.</param>
        /// <returns><c>true</c> if the contact request was successfully accepted; otherwise, <c>false</c>.</returns>
        Task<bool> AcceptContact(int contactId);

        /// <summary>
        /// Rejects a contact request by its ID.
        /// </summary>
        /// <param name="contactId">The ID of the contact request to reject.</param>
        /// <returns><c>true</c> if the contact request was successfully rejected; otherwise, <c>false</c>.</returns>
        Task<bool> RejectContact(int contactId);

        /// <summary>
        /// Retrieves the list of contacts for a specific user.
        /// </summary>
        /// <param name="fromUserId">The ID of the user whose contacts are to be retrieved.</param>
        /// <returns>A list of <see cref="MyContact"/> objects representing the user's contacts.</returns>
        Task<List<MyContact>> GetContacts(int fromUserId);

        /// <summary>
        /// Retrieves the list of contact requests received by a specific user.
        /// </summary>
        /// <param name="toUserId">The ID of the user whose contact requests are to be retrieved.</param>
        /// <returns>A list of <see cref="ContactRequest"/> objects representing the user's received contact requests.</returns>
        Task<List<ContactRequest>> GetContactRequest(int toUserId);
    }
}
