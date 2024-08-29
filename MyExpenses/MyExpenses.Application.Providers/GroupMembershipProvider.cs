using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Entities.Relationships;
using MyExpenses.Domain.core.Models.AppUser;
using MyExpenses.Domain.core.Models.Contact;
using MyExpenses.Domain.core.Models.Group;
using MyExpenses.Domain.core.Repositories;

namespace MyExpenses.Application.Providers
{
    /// <summary>
    /// Provides functionality for managing group memberships, including adding, removing users, and retrieving user and group information.
    /// </summary>
    public class GroupMembershipProvider : IGroupMembershipContract
    {
        private readonly IGroupMembershipRepo _groupMembershipRepo;
        private readonly IMapper _mapper;
        protected readonly IAuthHelperContract _authHelper;
        protected readonly IContactRepo _contactRepo;
        protected readonly IAppUserRepo _appUserRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupMembershipProvider"/> class.
        /// </summary>
        /// <param name="groupMembershipRepo">The repository for handling group membership operations.</param>
        /// <param name="mapper">The AutoMapper instance for mapping objects.</param>
        /// <param name="authHelperContract">The contract for authentication helper operations.</param>
        /// <param name="contactRepo">The repository for handling contact operations.</param>
        /// <param name="appUserRepo">The repository for handling application user operations.</param>
        public GroupMembershipProvider(IGroupMembershipRepo groupMembershipRepo, IMapper mapper, IAuthHelperContract authHelperContract, IContactRepo contactRepo, IAppUserRepo appUserRepo)
        {
            _groupMembershipRepo = groupMembershipRepo;
            _mapper = mapper;
            _authHelper = authHelperContract;
            _contactRepo = contactRepo;
            _appUserRepo = appUserRepo;
        }

        /// <summary>
        /// Adds a user to a group if a valid contact relationship exists between the users.
        /// </summary>
        /// <param name="userId">The ID of the user to add to the group.</param>
        /// <param name="groupId">The ID of the group to add the user to.</param>
        /// <returns>True if the user was successfully added to the group; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown if the contact relationship does not exist between the users or if the user is already in the group.</exception>
        public async Task<bool> AddUserToGroup(int userId, int groupId)
        {
            var currentUserId = _authHelper.GetCurrentUserId();
            var id = _appUserRepo.Search(u => u.UserId == currentUserId).Select(u => u.Id).SingleOrDefault();

            // Check if the contact relationship exists between the users
            var userRelation = _contactRepo.Search(c => (c.FromUserId == userId && c.ToUserId == id) || (c.ToUserId == userId && c.FromUserId == id)).SingleOrDefault();

            if (userRelation == null)
                throw new Exception("Invalid Request");

            var groupMembership = await _groupMembershipRepo.Search(m => m.AppUserId == userId && m.GroupId == groupId).SingleOrDefaultAsync();
            if (groupMembership != null)
                throw new Exception("User already in the group");

            UserGroupMembership userGroupMembership = new() { GroupId = groupId, AppUserId = userId };
            var result = await _groupMembershipRepo.CreateAsync(userGroupMembership);
            return result;
        }

        /// <summary>
        /// Retrieves all groups that a specific user is a member of.
        /// </summary>
        /// <param name="userId">The ID of the user whose groups are to be retrieved.</param>
        /// <returns>A list of <see cref="ApiUserGroup"/> objects representing the groups of the user.</returns>
        public async Task<List<ApiUserGroup>> GetGroupsOfUser(int userId)
        {
            var groups = await _groupMembershipRepo.GetAllGroupsOfUserAsync(userId);
            return _mapper.Map<List<ApiUserGroup>>(groups);
        }

        /// <summary>
        /// Retrieves all members of a specific group.
        /// </summary>
        /// <param name="groupId">The ID of the group whose members are to be retrieved.</param>
        /// <returns>A list of <see cref="ApiAppUser"/> objects representing the members of the group.</returns>
        public async Task<List<MyContact>> GetMembersOfGroup(int groupId)
        {
            var currentUserId = _authHelper.GetCurrentUserId();
            var appuser = _appUserRepo.Search(u => u.UserId == currentUserId).SingleOrDefault();
            if (appuser == null)
                throw new Exception("Invalid Request");
            var isExist = _groupMembershipRepo.Search(m => m.AppUserId == appuser.Id && m.GroupId == groupId).SingleOrDefault();
            if (isExist == null)
                throw new Exception("Invalid Request");
            var userList = await _groupMembershipRepo.GetAllUsersOGroupWithBalanceAsync(groupId, appuser.Id);
            return userList;

            //var users = await _groupMembershipRepo.GetAllUsersOfGroupAsync(groupId);
            // return _mapper.Map<List<ApiAppUser>>(users);
        }

        /// <summary>
        /// Removes a user from a group if the current user is the creator of the group membership.
        /// </summary>
        /// <param name="userId">The ID of the user to remove from the group.</param>
        /// <param name="groupId">The ID of the group from which the user will be removed.</param>
        /// <returns>True if the user was successfully removed from the group; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown if the current user is not authorized to remove the user from the group.</exception>
        public async Task<bool> RemoveUserFromGroup(int userId, int groupId)
        {
            var currUserId = _authHelper.GetCurrentUserId();

            var membership = await _groupMembershipRepo.Search(m => m.CreatedBy == currUserId && m.GroupId == groupId && m.AppUserId == userId).SingleOrDefaultAsync();
            if (membership == null)
                throw new Exception("Invalid Request");

            var result = await _groupMembershipRepo.DeleteAsync(membership.Id);
            return result != null;
        }
    }
}
