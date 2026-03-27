using ComputerSalesAPI.Core.Entities;
using ComputerSalesAPI.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerSalesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsCategoriesController : ControllerBase
    {
        private readonly IGenericRepository<NewsCategory> _repo;

        public NewsCategoriesController(IGenericRepository<NewsCategory> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsCategory>>> GetCategories()
        {
            return Ok(await _repo.ListAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NewsCategory>> GetCategory(int id)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<NewsCategory>> CreateCategory(NewsCategory category)
        {
            _repo.Add(category);
            await _repo.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateCategory(int id, NewsCategory category)
        {
            if (id != category.Id) return BadRequest();
            _repo.Update(category);
            await _repo.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null) return NotFound();
            _repo.Delete(category);
            await _repo.SaveChangesAsync();
            return NoContent();
        }
    }
}
