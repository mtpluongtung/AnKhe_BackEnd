using ComputerSalesAPI.Core.Entities;
using ComputerSalesAPI.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerSales.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenusController : ControllerBase
    {
        private readonly IGenericRepository<Menu> _repo;

        public MenusController(IGenericRepository<Menu> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Menu>>> GetMenus()
        {
            return Ok(await _repo.ListAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Menu>> GetMenu(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Menu>> CreateMenu(Menu item)
        {
            _repo.Add(item);
            await _repo.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMenu), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateMenu(int id, Menu item)
        {
            if (id != item.Id) return BadRequest();
            _repo.Update(item);
            await _repo.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteMenu(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) return NotFound();
            _repo.Delete(item);
            await _repo.SaveChangesAsync();
            return NoContent();
        }
    }
}
