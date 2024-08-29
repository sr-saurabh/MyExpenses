using AutoMapper;
using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Entities.Group;
using MyExpenses.Domain.core.Entities.Relationships;
using MyExpenses.Domain.core.Models.Group;
using MyExpenses.Domain.core.Repositories;
using System.Data.Entity;

namespace MyExpenses.Application.Providers
{
    /// <summary>
    /// Provides functionality for managing groups, including creating, updating, deleting, and retrieving groups.
    /// </summary>
    public class GroupProvider : IGroupContract
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepo _groupRepo;
        private readonly IGroupMembershipRepo _groupMembershipRepo;
        private readonly IAuthHelperContract _authHelper;
        private readonly IAppUserRepo _appUserRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupProvider"/> class.
        /// </summary>
        /// <param name="mapper">The AutoMapper instance for mapping objects.</param>
        /// <param name="groupRep">The repository for handling group operations.</param>
        /// <param name="groupMembershipRepo">The repository for handling group membership operations.</param>
        /// <param name="authHelperContract">The contract for authentication helper operations.</param>
        /// <param name="appUserRepo">The repository for handling application user operations.</param>
        public GroupProvider(IMapper mapper, IGroupRepo groupRep, IGroupMembershipRepo groupMembershipRepo, IAuthHelperContract authHelperContract, IAppUserRepo appUserRepo)
        {
            _mapper = mapper;
            _groupRepo = groupRep;
            _groupMembershipRepo = groupMembershipRepo;
            _authHelper = authHelperContract;
            _appUserRepo = appUserRepo;
        }

        /// <summary>
        /// Creates a new group and adds the current user as a member of the group.
        /// </summary>
        /// <param name="group">The details of the group to create.</param>
        /// <returns>An <see cref="ApiUserGroup"/> representing the created group.</returns>
        public async Task<ApiUserGroup> CreateGroup(CreateGroup group)
        {
            var newGroup = _mapper.Map<UserGroup>(group);
            await _groupRepo.CreateAsync(newGroup);

            var userId = _authHelper.GetCurrentUserId();
            var user = _appUserRepo.Search(u => u.UserId == userId).SingleOrDefault();

            // Adding the current user as a member of the group
            var userGroupMembership = new UserGroupMembership { GroupId = newGroup.Id, AppUserId = user.Id };
            await _groupMembershipRepo.CreateAsync(userGroupMembership);
            return _mapper.Map<ApiUserGroup>(newGroup);
        }

        /// <summary>
        /// Deletes a group by its ID.
        /// </summary>
        /// <param name="id">The ID of the group to delete.</param>
        /// <returns>True if the group was successfully deleted; otherwise, false.</returns>
        public async Task<bool> DeleteGroup(int id)
        {
            var res = await _groupRepo.DeleteAsync(id);
            return res!=null;
        }

        /// <summary>
        /// Retrieves a group by its ID.
        /// </summary>
        /// <param name="id">The ID of the group to retrieve.</param>
        /// <returns>An <see cref="ApiUserGroup"/> representing the group.</returns>
        public async Task<ApiUserGroup> GetGroupById(int id)
        {
            var group = await _groupRepo.GetByIdAsync(id);
            return _mapper.Map<ApiUserGroup>(group);
        }

        /// <summary>
        /// Retrieves all groups.
        /// </summary>
        /// <returns>A list of <see cref="ApiUserGroup"/> objects representing all groups.</returns>
        public async Task<List<ApiUserGroup>> GetGroups()
        {
            var groups = await _groupRepo.GetAllAsync();
            return _mapper.Map<List<ApiUserGroup>>(groups);
        }

        /// <summary>
        /// Updates the details of a group.
        /// </summary>
        /// <param name="group">The updated details of the group.</param>
        /// <param name="userId">The ID of the user making the update.</param>
        /// <returns>True if the group was successfully updated; otherwise, false.</returns>
        public async Task<bool> UpdateGroup(UpdateGroup group, int userId)
        {
            var isUpdated = await _groupRepo.UpdateAsync(g => g.Id == userId,
                                                          u => u.SetProperty(u => u.GroupName, group.GroupName)
                                                          .SetProperty(u => u.GroupPicture, group.GroupPicture)
                                                          .SetProperty(u => u.GroupTitle, group.GroupTitle));

            return isUpdated;
        }
    }
}
