using ComputerSalesAPI.Core.DTOs;
using ComputerSalesAPI.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComputerSalesAPI.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IReadOnlyList<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<IReadOnlyList<Product>> GetHotProductsAsync();
        Task<PagedResult<Product>> GetProductsAsync(int? categoryId, decimal? minPrice, decimal? maxPrice, string? sort, string? searchTerm, int pageIndex, int pageSize);
    }
}
