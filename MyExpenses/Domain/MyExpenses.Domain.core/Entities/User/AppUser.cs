using MyExpenses.Domain.core.Entities.Base;
using MyExpenses.Domain.core.Entities.Expenses;
using MyExpenses.Domain.core.Entities.Relationships;
using MyExpenses.Domain.core.Entities.Settlement;

namespace MyExpenses.Domain.core.Entities.User
{
    /// <summary>
    /// Represents a user in the application with personal details, contacts, expenses, and settlements.
    /// </summary>
    public class AppUser : AuditableEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the identity information for the user.
        /// </summary>
        public AppIdentityUser UserIdentity { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the full name of the user, if available.
        /// </summary>
        public string? FullName { get; set; }

        /// <summary>
        /// Gets or sets the URL of the user's avatar image.
        /// </summary>
        public string? Avatar { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the user.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the monthly budget set by the user.
        /// </summary>
        public double MonthlyBudget { get; set; }

        /// <summary>
        /// Gets or sets the collection of contacts where this user is the sender of the contact request.
        /// </summary>
        public ICollection<Contact>? FromContacts { get; set; }

        /// <summary>
        /// Gets or sets the collection of contacts where this user is the recipient of the contact request.
        /// </summary>
        public ICollection<Contact>? ToContacts { get; set; }

        /// <summary>
        /// Gets or sets the collection of group memberships associated with this user.
        /// </summary>
        public ICollection<UserGroupMembership>? GroupMemberships { get; set; }

        /// <summary>
        /// Gets or sets the collection of personal expenses incurred by the user.
        /// </summary>
        public ICollection<PersonalExpenses>? PersonalExpenses { get; set; }

        /// <summary>
        /// Gets or sets the collection of expenses where this user is the spender.
        /// </summary>
        public ICollection<UserExpense>? FromUserExpenses { get; set; }

        /// <summary>
        /// Gets or sets the collection of expenses where this user is the recipient.
        /// </summary>
        public ICollection<UserExpense>? ToUserExpenses { get; set; }

        /// <summary>
        /// Gets or sets the collection of group expense shares associated with the user.
        /// </summary>
        public ICollection<GroupExpenseShare>? GroupExpenseShares { get; set; }

        /// <summary>
        /// Gets or sets the collection of settlements where this user is the spender.
        /// </summary>
        public ICollection<SettlementHistory>? FromSettlements { get; set; }

        /// <summary>
        /// Gets or sets the collection of settlements where this user is the recipient.
        /// </summary>
        public ICollection<SettlementHistory>? ToSettlements { get; set; }
    }
}
