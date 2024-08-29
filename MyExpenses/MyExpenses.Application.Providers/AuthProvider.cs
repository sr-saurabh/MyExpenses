using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Entities.Enums;
using MyExpenses.Domain.core.Entities.User;
using MyExpenses.Domain.core.Models.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Application.Providers
{
    /// <summary>
    /// Provides authentication and authorization functionality.
    /// </summary>
    public class AuthProvider : IAuthContract
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly RoleManager<AppIdentityRole> _userRoleManager;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthProvider"/> class.
        /// </summary>
        /// <param name="userManager">The user manager for handling user operations.</param>
        /// <param name="configuration">The configuration for accessing application settings.</param>
        /// <param name="userRoleManager">The role manager for handling user roles.</param>
        public AuthProvider(UserManager<AppIdentityUser> userManager, IConfiguration configuration, RoleManager<AppIdentityRole> userRoleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _userRoleManager = userRoleManager;
        }

        /// <summary>
        /// Authenticates a user based on the provided login credentials.
        /// </summary>
        /// <param name="login">The login details including email and password.</param>
        /// <returns>An <see cref="AuthResponse"/> representing the result of the authentication attempt.</returns>
        public async Task<AuthResponse> Login(Login login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null)
                return AuthResponseHelper.UserDoesNotExist();

            if (!await _userManager.CheckPasswordAsync(user, login.Password))
                return AuthResponseHelper.InValidPassword();

            await _userManager.AddToRoleAsync(user, "User");
            var claims = await GenerateAuthClaim(user);
            var token = GenerateToken(claims);
            return AuthResponseHelper.SuccessWithToken(token);
        }

        /// <summary>
        /// Registers a new user with the provided registration details.
        /// </summary>
        /// <param name="register">The registration details including email and password.</param>
        /// <returns>An <see cref="AuthResponse"/> representing the result of the registration attempt.</returns>
        public async Task<AuthResponse> Register(Register register)
        {
            var user = await _userManager.FindByEmailAsync(register.Email);
            if (user != null)
                return AuthResponseHelper.AlreadyRegistered();

            AppIdentityUser identityUser = new()
            {
                ActivityStatus = ActivityStatus.Active,
                Email = register.Email,
                NormalizedEmail = register.Email.ToLower(),
                UserName = register.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.UtcNow,
                EmailConfirmed = true
            };

            var createdUser = await _userManager.CreateAsync(identityUser, register.Password);
            var userRole = await _userRoleManager.CreateAsync(new AppIdentityRole("User"));

            await _userManager.AddToRoleAsync(identityUser, "User");
            var claims = await GenerateAuthClaim(identityUser);
            var token = GenerateToken(claims);
            return AuthResponseHelper.SuccessWithToken(token);
        }

        /// <summary>
        /// Generates a list of claims for the specified user.
        /// </summary>
        /// <param name="user">The user for whom to generate claims.</param>
        /// <returns>A list of <see cref="Claim"/> objects representing the user's claims.</returns>
        private async Task<List<Claim>> GenerateAuthClaim(AppIdentityUser user)
        {
            bool isActive = user.ActivityStatus == ActivityStatus.Active;

            var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, user.NormalizedUserName),
               new Claim(ClaimTypes.Role, "User"),
               new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
               new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
               new Claim("IsActive", isActive.ToString()),
            };
            return authClaims;
        }

        /// <summary>
        /// Generates a JWT token based on the provided claims.
        /// </summary>
        /// <param name="claims">A list of claims to include in the token.</param>
        /// <returns>A string representing the generated JWT token.</returns>
        private string GenerateToken(List<Claim> claims)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"],
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
