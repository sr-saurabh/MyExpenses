using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyExpenses.Domain.core.Entities.Expenses;
using MyExpenses.Domain.core.Entities.Group;
using MyExpenses.Domain.core.Entities.Relationships;
using MyExpenses.Domain.core.Entities.Settlement;
using MyExpenses.Domain.core.Entities.User;
using MyExpenses.Infrastructure.Postgres.ModelCreationHooks;

namespace MyExpenses.Infrastructure.Postgres
{
    /// <summary>
    /// Represents the database context for the MyExpenses application.
    /// Inherits from <see cref="IdentityDbContext{AppIdentityUser, AppIdentityRole, Guid}"/>
    /// to provide identity management features and manage entity sets.
    /// </summary>
    public class MyExpensesDbContext : IdentityDbContext<AppIdentityUser, AppIdentityRole, Guid>
    {
        #region AppUser
        /// <summary>
        /// Gets or sets the <see cref="DbSet{AppUser}"/> representing the application users.
        /// </summary>
        public DbSet<AppUser> AppUsers { get; set; }
        #endregion

        #region UserGroup
        /// <summary>
        /// Gets or sets the <see cref="DbSet{UserGroup}"/> representing the user groups.
        /// </summary>
        public DbSet<UserGroup> UserGroups { get; set; }
        #endregion

        #region Expenses
        /// <summary>
        /// Gets or sets the <see cref="DbSet{GroupExpenses}"/> representing the group expenses.
        /// </summary>
        public DbSet<GroupExpenses> GroupExpenses { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{GroupExpenseShare}"/> representing the group expense shares.
        /// </summary>
        public DbSet<GroupExpenseShare> GroupExpenseShares { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{PersonalExpenses}"/> representing the personal expenses.
        /// </summary>
        public DbSet<PersonalExpenses> PersonalExpenses { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{UserExpense}"/> representing the user expenses.
        /// </summary>
        public DbSet<UserExpense> UserExpenses { get; set; }
        #endregion

        #region Settlement
        /// <summary>
        /// Gets or sets the <see cref="DbSet{SettlementHistory}"/> representing the settlement history.
        /// </summary>
        public DbSet<SettlementHistory> Settlements { get; set; }
        #endregion

        #region Relationship
        /// <summary>
        /// Gets or sets the <see cref="DbSet{Contact}"/> representing the contacts.
        /// </summary>
        public DbSet<Contact> Contacts { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{UserGroupMembership}"/> representing the user group memberships.
        /// </summary>
        public DbSet<UserGroupMembership> UserGroupMemberships { get; set; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="MyExpensesDbContext"/> class with the specified options.
        /// </summary>
        /// <param name="options">The options to configure the context.</param>
        public MyExpensesDbContext(DbContextOptions<MyExpensesDbContext> options) : base(options) { }

        /// <summary>
        /// Configures the model creating process for the context.
        /// </summary>
        /// <param name="modelBuilder">The builder used to configure the model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.OnAppUserCreating()
                        .OnPersonalExpenseCreating()
                        .OnGroupExpenseCreating()
                        .OnUserExpenseCreating()
                        .OnGroupExpenseShareCreating()
                        .OnGroupMemberShipCreating()
                        .OnContactCreating()
                        .OnSettlementCreating()
                        .OnInvitationCreating()
                        .OnUserGroupCreating();

            modelBuilder.Entity<AppIdentityUser>()
                .HasIndex(u => u.ActivityStatus);
        }
    }
}
