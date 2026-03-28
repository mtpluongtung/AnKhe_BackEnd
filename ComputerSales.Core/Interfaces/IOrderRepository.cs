using ComputerSalesAPI.Core.DTOs;
using ComputerSalesAPI.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComputerSalesAPI.Core.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<PagedResult<Order>> GetOrdersByUserIdPagedAsync(string userId, int pageIndex, int pageSize);
        Task<PagedResult<Order>> GetAllOrdersWithDetailsPagedAsync(string? searchTerm, int pageIndex, int pageSize);
    }
}
