using ComputerSalesAPI.Core.Entities;
using ComputerSalesAPI.Core.Interfaces;
using ComputerSalesAPI.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerSalesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly IGenericRepository<News> _repo;

        public NewsController(IGenericRepository<News> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetNews([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var items = await _repo.ListAllPagedAsync(pageIndex, pageSize);
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<News>> GetSingleNews(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<News>> CreateNews(News item)
        {
            _repo.Add(item);
            await _repo.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSingleNews), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateNews(int id, News item)
        {
            if (id != item.Id) return BadRequest();
            _repo.Update(item);
            await _repo.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteNews(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) return NotFound();
            _repo.Delete(item);
            await _repo.SaveChangesAsync();
            return NoContent();
        }
    }
}
