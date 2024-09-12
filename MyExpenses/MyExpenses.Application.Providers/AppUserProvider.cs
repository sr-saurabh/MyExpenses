using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Entities.Enums;
using MyExpenses.Domain.core.Entities.User;
using MyExpenses.Domain.core.Models.AppUser;
using MyExpenses.Domain.core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyExpenses.Application.Providers
{
    /// <summary>
    /// Provides functionality for managing application users.
    /// </summary>
    public class AppUserProvider : IAppUserContract
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAppUserRepo _appUserRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppUserProvider"/> class.
        /// </summary>
        /// <param name="mapper">The AutoMapper instance used for mapping entities to models.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="appUserRepo">The repository used for accessing application user data.</param>
        public AppUserProvider(IMapper mapper, IHttpContextAccessor httpContextAccessor, IAppUserRepo appUserRepo)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _appUserRepo = appUserRepo;
        }

        /// <summary>
        /// Registers a new application user.
        /// </summary>
        /// <param name="user">The details of the user to register.</param>
        /// <param name="email">The email address of the user.</param>
        /// <returns>The registered <see cref="CreateAppUser"/> object.</returns>
        /// <exception cref="Exception">Thrown when the user profile already exists.</exception>
        public async Task<CreateAppUser> RegisterAppUser(CreateAppUser user, string email, Guid? guid = null)
        {
            try
            {
                var existingUser = _appUserRepo.Search(u => u.UserId == user.UserId).SingleOrDefault();
                if (existingUser != null)
                    throw new Exception("Profile Already created");

                var appUser = _mapper.Map<AppUser>(user);
                appUser.Email = email;
                appUser.UserIdentity = new() { ActivityStatus = ActivityStatus.Active };
                appUser.FullName = $"{appUser.FirstName} {appUser.LastName}";

                await _appUserRepo.CreateAsync(appUser);
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Retrieves the current application user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>An <see cref="ApiAppUser"/> object representing the current user.</returns>
        public async Task<ApiAppUser> GetCurrentAppUser(Guid userId)
        {
            var user = _appUserRepo.Search(u => u.UserId == userId).SingleOrDefault();
            if (user == null)
                return null;
            return _mapper.Map<ApiAppUser>(user);
        }

        /// <summary>
        /// Retrieves a list of all application users.
        /// </summary>
        /// <returns>A list of <see cref="ApiAppUser"/> objects representing all users.</returns>
        public async Task<List<ApiAppUser>> GetUsers()
        {
            var users = await _appUserRepo.GetAllAsync();
            return _mapper.Map<List<ApiAppUser>>(users);
        }

        /// <summary>
        /// Retrieves a specific application user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>An <see cref="ApiAppUser"/> object representing the user.</returns>
        public async Task<ApiAppUser> GetUser(int id)
        {
            var user = await _appUserRepo.GetByIdAsync(id);
            return _mapper.Map<ApiAppUser>(user);
        }

        /// <summary>
        /// Updates an existing application user.
        /// </summary>
        /// <param name="user">The updated details of the user.</param>
        /// <param name="id">The ID of the user to update.</param>
        /// <returns><c>true</c> if the user was successfully updated; otherwise, <c>false</c>.</returns>
        public async Task<bool> UpdateUser(CreateAppUser user, int id)
        {
            var isUpdated = await _appUserRepo.UpdateAsync(u => u.Id == id, u => u
                .SetProperty(s => s.FirstName, user.FirstName)
                .SetProperty(s => s.LastName, user.LastName)
                .SetProperty(s => s.FullName, $"{user.FirstName} {user.LastName}")
                .SetProperty(s => s.PhoneNumber, user.PhoneNumber)
                .SetProperty(s => s.MonthlyBudget, user.MonthlyBudget)
                .SetProperty(s => s.Avatar, user.Avatar));
            return isUpdated;
        }

        /// <summary>
        /// Deletes an application user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>The ID of the deleted user.</returns>
        public async Task<bool> DeleteUser(int id)
        {
            bool res = await _appUserRepo.DeleteAsync(id);
            return res;
        }

        /// <summary>
        /// Disables an application user by setting their activity status to inactive.
        /// </summary>
        /// <param name="id">The ID of the user to disable.</param>
        /// <returns><c>true</c> if the user was successfully disabled; otherwise, <c>false</c>.</returns>
        public async Task<bool> DisableUser(int id)
        {
            var isUpdated = await _appUserRepo.UpdateAsync(u => u.Id == id, u => u
                .SetProperty(s => s.UserIdentity.ActivityStatus, ActivityStatus.Inactive));
            return isUpdated;
        }

        /// <summary>
        /// Searches for an application user based on email and/or contact number.
        /// </summary>
        /// <param name="user">The search criteria including email and contact number.</param>
        /// <returns>An <see cref="ApiAppUser"/> object representing the user matching the search criteria.</returns>
        public async Task<ApiAppUser> SearchUser(SearchUser user)
        {
            var users = _appUserRepo.Search(u => (string.IsNullOrEmpty(user.Email) || u.Email.ToLower() == user.Email.ToLower()) &&
                                                (string.IsNullOrEmpty(user.ContactNo) || u.PhoneNumber == user.ContactNo)).SingleOrDefault();
            return _mapper.Map<ApiAppUser>(users);
        }
    }
}
