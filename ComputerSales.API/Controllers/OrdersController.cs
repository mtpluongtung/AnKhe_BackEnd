using ComputerSalesAPI.Core.Entities;
using ComputerSalesAPI.Core.Interfaces;
using ComputerSalesAPI.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComputerSalesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IProductRepository _productRepo;

        public OrdersController(IOrderRepository orderRepo, IProductRepository productRepo)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] string? searchTerm, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            if (User.IsInRole("Admin"))
            {
                var allOrders = await _orderRepo.GetAllOrdersWithDetailsPagedAsync(searchTerm, pageIndex, pageSize);
                return Ok(allOrders);
            }

            var orders = await _orderRepo.GetOrdersByUserIdPagedAsync(userId, pageIndex, pageSize);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _orderRepo.GetByIdAsync(id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            // Kiểm tra và trừ số lượng trong kho
            foreach (var detail in order.OrderDetails)
            {
                var product = await _productRepo.GetByIdAsync(detail.ProductId);
                if (product == null) 
                    return BadRequest(new { message = $"Sản phẩm với ID {detail.ProductId} không tồn tại." });
                
                if (product.StockQuantity < detail.Quantity)
                {
                    return BadRequest(new { message = $"Sản phẩm '{product.Name}' không đủ hàng trong kho (Còn lại: {product.StockQuantity})." });
                }

                product.StockQuantity -= detail.Quantity;
                // Không cần gọi Update(product) vì product đã được track bởi GetByIdAsync
            }

            order.UserId = userId;
            order.OrderDate = DateTime.Now;
            order.Status = "Pending";

            _orderRepo.Add(order);
            await _orderRepo.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        [HttpPatch("{id}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] OrderStatusUpdateDto updateDto)
        {
            if (updateDto == null || string.IsNullOrEmpty(updateDto.Status))
                return BadRequest(new { message = "Trạng thái không hợp lệ." });

            var order = await _orderRepo.GetByIdAsync(id);
            if (order == null) return NotFound();

            order.Status = updateDto.Status;
            if (updateDto.Status == "Cancelled")
            {
                order.CancelReason = updateDto.CancelReason;
            }

            // Entity is already being tracked by FindAsync (GetByIdAsync)
            await _orderRepo.SaveChangesAsync();

            return NoContent();
        }
    }

    public class OrderStatusUpdateDto
    {
        [System.Text.Json.Serialization.JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [System.Text.Json.Serialization.JsonPropertyName("cancelReason")]
        public string? CancelReason { get; set; }
    }
}
