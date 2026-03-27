using System.Collections.Generic;
using System.Threading.Tasks;
using ComputerSalesAPI.Core.DTOs;

namespace ComputerSalesAPI.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<PagedResult<T>> ListAllPagedAsync(int pageIndex, int pageSize);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> SaveChangesAsync();
    }
}
