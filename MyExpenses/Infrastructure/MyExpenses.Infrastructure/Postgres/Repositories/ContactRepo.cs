using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Entities.Enums;
using MyExpenses.Domain.core.Entities.Relationships;
using MyExpenses.Domain.core.Models.Contact;
using MyExpenses.Domain.core.Repositories;
using MyExpenses.Infrastructure.Postgres.Repositories.BaseRepo;

namespace MyExpenses.Infrastructure.Postgres.Repositories
{
    /// <summary>
    /// Repository for managing operations related to the <see cref="Contact"/> entity.
    /// </summary>
    public class ContactRepo : AuditableRepo<Contact>, IContactRepo
    {
        private readonly IAuthHelperContract _authHelper;
        private readonly MyExpensesDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactRepo"/> class.
        /// </summary>
        /// <param name="dbContext">The database context used for interacting with the database.</param>
        /// <param name="authHelper">The authentication helper contract used for user authentication tasks.</param>
        public ContactRepo(MyExpensesDbContext dbContext, IAuthHelperContract authHelper) : base(dbContext, authHelper)
        {
            _dbContext = dbContext;
            _authHelper = authHelper;
        }

        /// <summary>
        /// Accepts a contact request by updating its status to <see cref="ContactInvitationStatus.Accepted"/>.
        /// </summary>
        /// <param name="contactId">The identifier of the contact request to be accepted.</param>
        /// <returns><c>true</c> if the request was successfully updated; otherwise, <c>false</c>.</returns>
        public async Task<bool> AcceptRequest(int contactId)
        {
            var isUpdated = await UpdateAsync(c => c.Id == contactId,
                                    c => c.SetProperty(c => c.Status, ContactInvitationStatus.Accepted)
                                   );
            return isUpdated;
        }

        /// <summary>
        /// Rejects a contact request by updating its status to <see cref="ContactInvitationStatus.Rejected"/>.
        /// </summary>
        /// <param name="contactId">The identifier of the contact request to be rejected.</param>
        /// <returns><c>true</c> if the request was successfully updated; otherwise, <c>false</c>.</returns>
        public async Task<bool> RejectRequest(int contactId)
        {
            var isUpdated = await UpdateAsync(c => c.Id == contactId,
                                    c => c.SetProperty(c => c.Status, ContactInvitationStatus.Rejected)
                                   );
            return isUpdated;
        }

        /// <summary>
        /// Retrieves a list of pending contact requests for a specified user.
        /// </summary>
        /// <param name="toUserId">The identifier of the user who received the contact requests.</param>
        /// <returns>A list of <see cref="ContactRequest"/> representing the pending contact requests.</returns>
        public async Task<List<ContactRequest>> GetRequest(int toUserId)
        {
            var query = from c in _dbContext.Contacts
                        join u in _dbContext.AppUsers on c.FromUserId equals u.Id
                        where c.ToUserId == toUserId && c.Status == ContactInvitationStatus.Pending
                        select new ContactRequest
                        {
                            ContactRequestId = c.Id,
                            RequesterId = c.FromUserId,
                            FullName = u.FullName,
                            Email = u.Email,
                            PhoneNumber = u.PhoneNumber,
                            Avatar = u.Avatar,
                            Status = c.Status
                        };

            return query.ToList();
        }

        /// <summary>
        /// Retrieves a list of contacts for a specified user.
        /// </summary>
        /// <param name="userId">The identifier of the user whose contacts are to be retrieved.</param>
        /// <returns>A list of <see cref="MyContact"/> representing the user's contacts.</returns>
        public async Task<List<MyContact>> GetContacts(int userId)
        {
            var query = from c in _dbContext.Contacts.Where(c => (c.ToUserId == userId) && c.Status == ContactInvitationStatus.Accepted)
                        join u in _dbContext.AppUsers on c.FromUserId equals u.Id
                        select new MyContact
                        {
                            ContactId = c.Id,
                            FullName = u.FullName,
                            Email = u.Email,
                            PhoneNumber = u.PhoneNumber,
                            Avatar = u.Avatar,
                            TotalBalance = -(c.Balance)
                        };

            var contacts = query.ToList();
            query = from c in _dbContext.Contacts.Where(c => (c.FromUserId == userId) && c.Status == ContactInvitationStatus.Accepted)
                    join u in _dbContext.AppUsers on c.ToUserId equals u.Id
                    select new MyContact
                    {
                        ContactId = c.Id,
                        FullName = u.FullName,
                        Email = u.Email,
                        PhoneNumber = u.PhoneNumber,
                        Avatar = u.Avatar,
                        TotalBalance = c.Balance
                    };
            contacts.AddRange(query.ToList());
            return contacts;
        }
    }
}
