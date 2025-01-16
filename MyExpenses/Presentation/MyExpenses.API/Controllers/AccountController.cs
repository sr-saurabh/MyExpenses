using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Entities.Common;
using MyExpenses.Domain.core.Models.Accounts;

namespace MyExpenses.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountContract _accountContract;
        public AccountController(IAccountContract accountContract)
        {
            _accountContract = accountContract;
        }
        [HttpGet]
        public async Task<IActionResult> GetAccount(int accountId)
        {
            var result = await _accountContract.GetAccount(accountId);
            return Ok(result);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAccounts(int userId)
        {
            var result = await _accountContract.GetAccounts(userId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(CreateAccount account)
        {
            var result = await _accountContract.CreateAccount(account);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAccount(UpdateAccount account)
        {
            var result = await _accountContract.UpdateAccount(account);
            return Ok(result);
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteAccount(int accountId)
        {
            var result = await _accountContract.DeleteAccount(accountId);
            return Ok(result);
        }
    }
}
