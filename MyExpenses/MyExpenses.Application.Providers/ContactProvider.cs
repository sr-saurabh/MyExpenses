using AutoMapper;
using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Entities.Enums;
using MyExpenses.Domain.core.Entities.Relationships;
using MyExpenses.Domain.core.Models.Contact;
using MyExpenses.Domain.core.Repositories;

namespace MyExpenses.Application.Providers
{
    /// <summary>
    /// Provides functionality for managing contacts and contact requests.
    /// </summary>
    public class ContactProvider : IContactContract
    {
        private readonly IContactRepo _contactRepo;
        private readonly IMapper _mapper;
        private readonly IAuthHelperContract _authHelperContract;
        private readonly IAppUserRepo _appUserRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactProvider"/> class.
        /// </summary>
        /// <param name="contactRepo">The contact repository for handling contact operations.</param>
        /// <param name="mapper">The mapper for mapping between domain models and DTOs.</param>
        /// <param name="authHelperContract">The authentication helper for user verification.</param>
        /// <param name="appUserRepo">The application user repository for handling user operations.</param>
        public ContactProvider(IContactRepo contactRepo, IMapper mapper, IAuthHelperContract authHelperContract, IAppUserRepo appUserRepo)
        {
            _contactRepo = contactRepo;
            _mapper = mapper;
            _authHelperContract = authHelperContract;
            _appUserRepo = appUserRepo;
        }

        /// <summary>
        /// Accepts a contact request.
        /// </summary>
        /// <param name="contactId">The ID of the contact request to accept.</param>
        /// <returns>True if the contact request was successfully accepted; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown if the contact request is not found or the user is not verified.</exception>
        public async Task<bool> AcceptContact(int contactId)
        {
            var contact = await _contactRepo.GetByIdAsync(contactId);
            if (contact == null)
                throw new Exception("Request not found");

            var isVerified = await VerifyUser(contact.ToUserId, null);
            if (!isVerified)
                throw new Exception("User not verified");

            var isAccepted = await _contactRepo.AcceptRequest(contactId);
            return isAccepted;
        }

        /// <summary>
        /// Adds a contact request from one user to another.
        /// </summary>
        /// <param name="fromUserId">The ID of the user sending the contact request.</param>
        /// <param name="toUserId">The ID of the user receiving the contact request.</param>
        /// <returns>True if the contact request was successfully added; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown if the current user or the recipient is not found, or if the request has already been sent or the users are already friends.</exception>
        public async Task<bool> AddContact(int fromUserId, int toUserId)
        {
            var isVerified = await VerifyUser(fromUserId, null);
            if (!isVerified)
                throw new Exception("Current user not found");

            var isVerifiedToUser = _appUserRepo.Search(u => u.Id == toUserId).SingleOrDefault();
            if (isVerifiedToUser == null)
                throw new Exception("User not found");

            var existingContact = _contactRepo.Search(c => (c.FromUserId == fromUserId && c.ToUserId == toUserId)
                                                        || (c.FromUserId == toUserId && c.ToUserId == fromUserId)).SingleOrDefault();

            if (existingContact != null)
            {
                throw new Exception(existingContact.Status == ContactInvitationStatus.Pending ? "Request already sent" : "Already a friend");
            }

            Contact contact = new()
            {
                FromUserId = fromUserId,
                ToUserId = toUserId,
                Status = ContactInvitationStatus.Pending
            };

            var res = await _contactRepo.CreateAsync(contact);
            return res;
        }

        /// <summary>
        /// Gets all contact requests for a user.
        /// </summary>
        /// <param name="toUserId">The ID of the user for whom to retrieve contact requests.</param>
        /// <returns>A list of <see cref="ContactRequest"/> objects representing the contact requests.</returns>
        public async Task<List<ContactRequest>> GetContactRequest(int toUserId)
        {
            var requests = await _contactRepo.GetRequest(toUserId);
            return requests;
        }

        /// <summary>
        /// Gets all contacts of a user.
        /// </summary>
        /// <param name="userId">The ID of the user whose contacts are to be retrieved.</param>
        /// <returns>A list of <see cref="MyContact"/> objects representing the user's contacts.</returns>
        /// <exception cref="NotImplementedException">Thrown if the method is not yet implemented.</exception>
        public async Task<List<MyContact>> GetContacts(int userId)
        {
            var contacts = await _contactRepo.GetContacts(userId);
            return _mapper.Map<List<MyContact>>(contacts);
        }

        /// <summary>
        /// Rejects a contact request.
        /// </summary>
        /// <param name="contactId">The ID of the contact request to reject.</param>
        /// <returns>True if the contact request was successfully rejected; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown if the contact request is not found or the user is not verified.</exception>
        public async Task<bool> RejectContact(int contactId)
        {
            var contact = await _contactRepo.GetByIdAsync(contactId);
            if (contact == null)
                throw new Exception("Request not found");

            var isVerified = await VerifyUser(contact.ToUserId, null);
            if (!isVerified)
                throw new Exception("User not verified");

            var isRejected = await _contactRepo.RejectRequest(contactId);
            return isRejected;
        }

        /// <summary>
        /// Removes a contact from the user's contact list.
        /// </summary>
        /// <param name="contactId">The ID of the contact to remove.</param>
        /// <returns>True if the contact was successfully removed; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown if the contact is not found or the user is not verified.</exception>
        public async Task<bool> RemoveContact(int contactId)
        {
            var contact = await _contactRepo.GetByIdAsync(contactId);
            if (contact == null)
                throw new Exception("Request not found");

            var isVerified = await VerifyUser(contact.FromUserId, null);
            if (!isVerified)
                throw new Exception("User not verified");

            var deletedContact = await _contactRepo.DeleteAsync(contactId);
            return deletedContact != null;
        }

        /// <summary>
        /// Verifies that a user exists and is valid.
        /// </summary>
        /// <param name="id">The ID of the user to verify.</param>
        /// <param name="guid">An optional GUID for additional verification.</param>
        /// <returns>True if the user is verified; otherwise, false.</returns>
        private async Task<bool> VerifyUser(int id, Guid? guid)
        {
            if (guid == null)
                guid = _authHelperContract.GetCurrentUserId();
            var user = _appUserRepo.Search(u => u.UserId == guid && u.Id == id).SingleOrDefault();
            return user != null;
        }
    }
}
