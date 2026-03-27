using ComputerSalesAPI.Core.Entities;
using ComputerSalesAPI.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerSales.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WebpageConfigsController : ControllerBase
    {
        private readonly IGenericRepository<WebpageConfig> _repo;

        public WebpageConfigsController(IGenericRepository<WebpageConfig> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<WebpageConfig>>> GetConfigs()
        {
            return Ok(await _repo.ListAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WebpageConfig>> GetConfig(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<WebpageConfig>> CreateConfig(WebpageConfig item)
        {
            _repo.Add(item);
            await _repo.SaveChangesAsync();
            return CreatedAtAction(nameof(GetConfig), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateConfig(int id, WebpageConfig item)
        {
            if (id != item.Id) return BadRequest();
            _repo.Update(item);
            await _repo.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfig(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) return NotFound();
            _repo.Delete(item);
            await _repo.SaveChangesAsync();
            return NoContent();
        }
    }
}
