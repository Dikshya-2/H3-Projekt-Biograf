using Biograf.Repo.DTOs;
using Biograf.Repo.Interface;
using Biograf.Repo.Models.Entities;
using Biograf.Repo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biograf.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _categoryRepo;

        public CategoryController(ICategory category)
        {
            _categoryRepo = category;
        }
        [HttpGet]
        //public async Task<List<Category>> Get()
        public async Task<ActionResult<List<Category>>> Get()

        {
            //return await _categoryRepo.Get();
            try
            {
                return await _categoryRepo.Get();
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error occurred while getting categories.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            //return Ok(await _categoryRepo.Get(id));
            try
            {
                var category = await _categoryRepo.Get(id);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while getting the category.");
            }
        }

        [HttpGet("categoryId")]
        public async Task<IActionResult> GetByMovieByCategory(int categoryId)
        {
            // return Ok(await _categoryRepo.FindCategoryById(categoryId));
            try
            {
                var category = await _categoryRepo.FindCategoryById(categoryId);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while getting the category by movie.");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto category)

        {
            //return Ok(await _categoryRepo.Create(category));
            try
            {
                var createdCategory = await _categoryRepo.Create(category);
                return Ok(createdCategory);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while creating the category.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //return Ok(await _categoryRepo.Delete(id));
            //try
            //{
            //    return Ok(await _categoryRepo.Delete(id));
            //}
            //catch (Exception ex)
            //{
            //    // Log the exception
            //    return StatusCode(500, "An error occurred while deleting the category.");
            //}
            try
            {
                var deletedCategory = await _categoryRepo.Delete(id);
                if (deletedCategory == null)
                {
                    return NotFound();
                }
                return Ok(deletedCategory);

            }
            catch (Exception ex)
            { return StatusCode(500, ex.Message); }
        }
         

            [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryDto category)
        {
            //return Ok
            //var updatedCategory = (await _categoryRepo.Update(id, category));
            //return Ok(updatedCategory);
            try
            {
                var updatedCategory = await _categoryRepo.Update(id, category);
                if (updatedCategory == null)
                {
                    return NotFound();
                }
                return Ok(updatedCategory);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while updating the category.");
            }
        }
    }

}

