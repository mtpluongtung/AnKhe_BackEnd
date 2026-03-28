using ComputerSalesAPI.Core.DTOs;
using ComputerSalesAPI.Core.Entities;
using System.Threading.Tasks;

namespace ComputerSalesAPI.Core.Interfaces
{
    public interface INewsRepository : IGenericRepository<News>
    {
        Task<PagedResult<News>> GetNewsPagedAsync(string? searchTerm, int pageIndex, int pageSize);
    }
}
