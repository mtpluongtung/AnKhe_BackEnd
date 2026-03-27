using ComputerSalesAPI.Core.Entities;
using ComputerSalesAPI.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerSales.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IGenericRepository<Contact> _repo;

        public ContactsController(IGenericRepository<Contact> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IReadOnlyList<Contact>>> GetContacts()
        {
            return Ok(await _repo.ListAllAsync());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        // Allow anonymous users to submit contact form
        public async Task<ActionResult<Contact>> CreateContact(Contact item)
        {
            _repo.Add(item);
            await _repo.SaveChangesAsync();
            return CreatedAtAction(nameof(GetContact), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateContact(int id, Contact item)
        {
            if (id != item.Id) return BadRequest();
            _repo.Update(item);
            await _repo.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteContact(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) return NotFound();
            _repo.Delete(item);
            await _repo.SaveChangesAsync();
            return NoContent();
        }
    }
}
