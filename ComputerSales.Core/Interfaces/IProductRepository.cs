using ComputerSalesAPI.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComputerSalesAPI.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IReadOnlyList<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<IReadOnlyList<Product>> GetHotProductsAsync();
    }
}
