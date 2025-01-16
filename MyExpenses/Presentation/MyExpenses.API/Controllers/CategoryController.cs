using Microsoft.AspNetCore.Mvc;
using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Models.Category;

namespace MyExpenses.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CategoryController : ControllerBase
    {

        private readonly ICategoryContract _categoryContract;
        public CategoryController(ICategoryContract categoryContract)
        {
            _categoryContract = categoryContract;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _categoryContract.GetAllCategory();
            return Ok(res);
        }




    }
}
