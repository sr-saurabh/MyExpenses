using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyExpenses.Application.Abstraction;

namespace MyExpenses.API.Controllers
{
    /// <summary>
    /// Manages HTTP requests related to personal expenses, including retrieval, creation, updating, and deletion.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionContract _transactionContract;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionController"/> class.
        /// </summary>
        /// <param name="personalExpenseContract">The contract for managing personal expenses.</param>
        public TransactionController(ITransactionContract transactionContract)
        {
            _transactionContract = transactionContract;
        }
        [HttpGet("account/{accountId}")]
        public async Task<IActionResult> GetAllTransactions(int accountId )
        {
            var res = await _transactionContract.GetTransactions(accountId);
            return Ok(res);
        }
    }
}
