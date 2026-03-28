using ComputerSalesAPI.Core.DTOs;
using ComputerSalesAPI.Core.Entities;
using System.Threading.Tasks;

namespace ComputerSalesAPI.Core.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<PagedResult<Category>> GetCategoriesAsync(string? searchTerm, int pageIndex, int pageSize);
    }
}
