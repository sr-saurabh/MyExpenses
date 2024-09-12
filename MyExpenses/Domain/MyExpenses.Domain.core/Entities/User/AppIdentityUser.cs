using Microsoft.AspNetCore.Identity;
using MyExpenses.Domain.core.Entities.Enums;
using System;

namespace MyExpenses.Domain.core.Entities.User
{
    /// <summary>
    /// Represents an application user with identity information and additional properties.
    /// </summary>
    public class AppIdentityUser : IdentityUser<Guid>
    {
        /// <summary>
        /// Gets or sets the activity status of the user.
        /// </summary>
        public ActivityStatus ActivityStatus { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the user was created.
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// Gets or sets that the user have used google for authentication
        /// </summary>
        public bool IsGoogleLoggedIn { get; set; }
    }

    /// <summary>
    /// Represents a role in the application with identity information.
    /// </summary>
    public class AppIdentityRole : IdentityRole<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppIdentityRole"/> class.
        /// </summary>
        public AppIdentityRole()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppIdentityRole"/> class with the specified role name.
        /// </summary>
        /// <param name="role">The name of the role.</param>
        public AppIdentityRole(string role) : base(role)
        {
        }
    }
}
