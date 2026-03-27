using ComputerSalesAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerSalesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class StatisticsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StatisticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("revenue-stats")]
        public async Task<IActionResult> GetRevenueStats([FromQuery] string period = "monthly")
        {
            var query = _context.Orders.Where(o => o.Status == "Completed");

            if (period == "daily")
            {
                var stats = await query
                    .GroupBy(o => o.OrderDate.Date)
                    .Select(g => new { Label = g.Key.ToString("dd/MM/yyyy"), Value = g.Sum(o => o.TotalAmount) })
                    .OrderBy(g => g.Label)
                    .Take(30)
                    .ToListAsync();
                return Ok(stats);
            }
            else if (period == "yearly")
            {
                var stats = await query
                    .GroupBy(o => o.OrderDate.Year)
                    .Select(g => new { Label = g.Key.ToString(), Value = g.Sum(o => o.TotalAmount) })
                    .OrderBy(g => g.Label)
                    .ToListAsync();
                return Ok(stats);
            }
            else // monthly
            {
                var stats = await query
                    .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
                    .Select(g => new { Label = $"{g.Key.Month}/{g.Key.Year}", Value = g.Sum(o => o.TotalAmount), SortKey = g.Key.Year * 100 + g.Key.Month })
                    .OrderBy(g => g.SortKey)
                    .Select(g => new { g.Label, g.Value })
                    .ToListAsync();
                return Ok(stats);
            }
        }

        [HttpGet("confirmed-products")]
        public async Task<IActionResult> GetConfirmedProducts()
        {
            var confirmedProducts = await _context.OrderDetails
                .Include(od => od.Product)
                .Include(od => od.Order)
                .Where(od => od.Order.Status == "Completed")
                .GroupBy(od => new { od.ProductId, od.Product.Name })
                .Select(g => new
                {
                    ProductId = g.Key.ProductId,
                    ProductName = g.Key.Name,
                    TotalQuantity = g.Sum(od => od.Quantity),
                    TotalRevenue = g.Sum(od => od.Quantity * od.UnitPrice)
                })
                .OrderByDescending(g => g.TotalQuantity)
                .ToListAsync();

            return Ok(confirmedProducts);
        }
    }
}
