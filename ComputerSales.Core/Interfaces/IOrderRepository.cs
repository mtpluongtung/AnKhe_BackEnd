using ComputerSalesAPI.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComputerSalesAPI.Core.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IReadOnlyList<Order>> GetOrdersByUserIdAsync(string userId);
        Task<IReadOnlyList<Order>> GetAllOrdersWithDetailsAsync();
    }
}
