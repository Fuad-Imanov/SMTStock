using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMTstock.Entities.DTO.CategoryDto;
using SMTstock.Services.Interfaces;

namespace SMTstock.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/CategoryAPI")]
    [ApiController]
    public class CategoryAPIController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryAPIController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }



        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _categoryService.GetAllCategoriesAsync();
            return Ok(result);
        }

        [HttpGet("GetCategoryById/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }


        [HttpPost("AddCategory")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddcategoryAsync(AddCategoryDTO addcategoryDTO)
        {


            if (ModelState.IsValid)
            {

                var result = await _categoryService.AddCategoryAsync(addcategoryDTO);
                return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Data.Id }, result);
            }

            return BadRequest(ModelState);


        }

        [HttpPut("Updatecategory/{id:int}")]
        public async Task<IActionResult> UpdatecategoryAsync(int id, [FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO == null || id != categoryDTO.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _categoryService.UpdateCategory(id, categoryDTO);
                    return Ok(result);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_categoryService.GetCategoryByIdAsync(id) == null)
                    {
                        return NotFound();
                    }

                    throw;
                }

                return NoContent();
            }

            return BadRequest(ModelState);
        }



        [HttpDelete("DeleteCategory/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _categoryService.RemoveCategory(id);

            return Ok(result);
        }

    }
}


