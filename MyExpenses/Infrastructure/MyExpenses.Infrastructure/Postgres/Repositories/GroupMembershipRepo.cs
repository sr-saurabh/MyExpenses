using Microsoft.EntityFrameworkCore;
using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Entities.Group;
using MyExpenses.Domain.core.Entities.Relationships;
using MyExpenses.Domain.core.Entities.User;
using MyExpenses.Domain.core.Models.Contact;
using MyExpenses.Domain.core.Repositories;
using MyExpenses.Infrastructure.Postgres.Repositories.BaseRepo;

namespace MyExpenses.Infrastructure.Postgres.Repositories
{
    /// <summary>
    /// Repository for managing operations related to the <see cref="UserGroupMembership"/> entity.
    /// </summary>
    public class GroupMembershipRepo : AuditableRepo<UserGroupMembership>, IGroupMembershipRepo
    {
        private readonly MyExpensesDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupMembershipRepo"/> class.
        /// </summary>
        /// <param name="dbContext">The database context used for interacting with the database.</param>
        /// <param name="authHelper">The authentication helper contract used for user authentication tasks.</param>
        public GroupMembershipRepo(MyExpensesDbContext dbContext, IAuthHelperContract authHelper) : base(dbContext, authHelper)
        {
            _context = dbContext;
        }

        /// <summary>
        /// Retrieves all users belonging to a specific group.
        /// </summary>
        /// <param name="groupId">The ID of the group.</param>
        /// <returns>A list of <see cref="AppUser"/> objects that are members of the specified group.</returns>
        public async Task<List<AppUser>> GetAllUsersOfGroupAsync(int groupId)
        {
            var users = await Search(m => m.GroupId == groupId)
                .Include(m => m.AppUser)
                .Select(m => m.AppUser)
                .ToListAsync();
            return users;
        }

        /// <summary>
        /// Retrieves all users belonging to a specific group.
        /// </summary>
        /// <param name="groupId">The ID of the group.</param>
        /// <returns>A list of <see cref="MyContact"/> objects that are members of the specified group.</returns>


        public async Task<List<MyContact>> GetAllUsersOGroupWithBalanceAsync(int groupId, int userId)
        {
            // Get all members of the group
            var allUsersInGroup = from user in _context.AppUsers
                                  join groupMembership in _context.UserGroupMemberships on user.Id equals groupMembership.AppUserId
                                  where groupMembership.GroupId == groupId && user.Id != userId
                                  select new MyContact
                                  {
                                      ContactId = user.Id,
                                      FullName = user.FullName,
                                      Email = user.Email,
                                      PhoneNumber = user.PhoneNumber,
                                      TotalBalance = 0 // Initialize with 0 balance
                                  };

            // Query 1: Users who have paid the expenses
            var query = from user in _context.AppUsers
                        join groupMembership in _context.UserGroupMemberships on user.Id equals groupMembership.AppUserId
                        where groupMembership.GroupId == groupId
                        join groupExpense in _context.GroupExpenses on groupMembership.GroupId equals groupExpense.GroupId
                        where groupExpense.PayerId == userId
                        join groupexpenseShare in _context.GroupExpenseShares on groupExpense.Id equals groupexpenseShare.GroupExpenseId
                        where groupexpenseShare.ReceiverId == user.Id
                        group new { user, groupexpenseShare } by new { user.Id } into g
                        select new
                        {
                            ContactId = g.Key.Id,
                            TotalBalance = g.Sum(x => x.groupexpenseShare.ShareAmount)
                        };

            // Query 2: Users who received payments
            var query2 = from user in _context.AppUsers
                         join groupMembership in _context.UserGroupMemberships on user.Id equals groupMembership.AppUserId
                         where groupMembership.GroupId == groupId
                         join groupExpense in _context.GroupExpenses on groupMembership.GroupId equals groupExpense.GroupId
                         where groupExpense.PayerId == user.Id
                         join groupexpenseShare in _context.GroupExpenseShares on groupExpense.Id equals groupexpenseShare.GroupExpenseId
                         where groupexpenseShare.ReceiverId == userId
                         group new { user, groupexpenseShare } by new { user.Id } into g
                         select new
                         {
                             ContactId = g.Key.Id,
                             TotalBalance = g.Sum(x => x.groupexpenseShare.ShareAmount)
                         };

            // Execute the queries
            var allUsersList = await allUsersInGroup.ToListAsync();
            var result = await query.ToListAsync();
            var result2 = await query2.ToListAsync();

            // Create dictionaries for easy lookup
            var resultDict = result.ToDictionary(r => r.ContactId, r => r.TotalBalance);
            var result2Dict = result2.ToDictionary(r => r.ContactId, r => r.TotalBalance);

            // Combine the results and calculate the total balance
            foreach (var user in allUsersList)
            {
                if (resultDict.TryGetValue(user.ContactId, out var balance1))
                {
                    user.TotalBalance += balance1;
                }

                if (result2Dict.TryGetValue(user.ContactId, out var balance2))
                {
                    user.TotalBalance -= balance2;
                }
            }

            return allUsersList;
        }

        /// <summary>
        /// Retrieves all groups to which a specific user belongs.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A list of <see cref="UserGroup"/> objects that the specified user is a member of.</returns>
        public async Task<List<UserGroup>> GetAllGroupsOfUserAsync(int userId)
        {
            var groups = await Search(m => m.AppUserId == userId)
                .Include(m => m.Group)
                .Select(m => m.Group)
                .ToListAsync();
            return groups;
        }
    }
}
