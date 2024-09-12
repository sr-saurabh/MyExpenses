using Microsoft.AspNetCore.Mvc;
using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Models.Auth;
using System.Threading.Tasks;

namespace MyExpenses.API.Controllers
{
    /// <summary>
    /// Handles authentication-related operations, including user registration and login.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthContract _authContract;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authContract">The authentication contract for user operations.</param>
        public AuthController(IAuthContract authContract)
        {
            _authContract = authContract;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registerUser">The registration details of the user.</param>
        /// <returns>A result containing the <see cref="AuthResponse"/> with registration details.</returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(Register registerUser)
        {
            var result = await _authContract.Register(registerUser);
            return Ok(result);
        }

        /// <summary>
        /// Logs in an existing user.
        /// </summary>
        /// <param name="loginUser">The login details of the user.</param>
        /// <returns>A result containing the <see cref="AuthResponse"/> with authentication tokens.</returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(Login loginUser)
        {
            var result = await _authContract.Login(loginUser);
            return Ok(result);
        }

        [HttpPost]
        [Route("login-with-google")]
        public async Task<IActionResult> LoginWithGoogle(GoogleLogin credentials)
        {
            var res= await _authContract.LoginWithGoogle(credentials);
            return Ok(res);
        }
    }
}
