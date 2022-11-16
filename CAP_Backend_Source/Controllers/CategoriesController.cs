using CAP_Backend_Source.Modules.Category.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CAP_Backend_Source.Modules.Category.Request.CategoryRequest;

namespace CAP_Backend_Source.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            return Ok(await _categoryService.GetAllCategory());
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
        {
            return Ok(await _categoryService.CreateCategory(request));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, EditCategoryRequest request)
        {
            return Ok(await _categoryService.UpdateCategory(id, request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            return Ok(await _categoryService.DeleteCategory(id));
        }
    }
}
