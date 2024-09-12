using MyExpenses.Domain.core.Models.Auth;
using System.Threading.Tasks;

namespace MyExpenses.Application.Abstraction
{
    /// <summary>
    /// Defines the contract for authentication-related operations.
    /// </summary>
    public interface IAuthContract
    {
        /// <summary>
        /// Authenticates a user based on the provided login credentials.
        /// </summary>
        /// <param name="login">The login credentials of the user.</param>
        /// <returns>An <see cref="AuthResponse"/> object containing authentication information, such as tokens.</returns>
        Task<AuthResponse> Login(Login login);

        /// <summary>
        /// Registers a new user based on the provided registration details.
        /// </summary>
        /// <param name="register">The registration details of the user.</param>
        /// <returns>An <see cref="AuthResponse"/> object containing authentication information, such as tokens.</returns>
        Task<AuthResponse> Register(Register register);

        Task<AuthResponse> LoginWithGoogle(GoogleLogin credentials);
    }
}
