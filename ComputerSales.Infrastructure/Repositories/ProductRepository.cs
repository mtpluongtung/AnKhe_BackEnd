using ComputerSalesAPI.Core.Entities;
using ComputerSalesAPI.Core.Interfaces;
using ComputerSalesAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComputerSalesAPI.Core.DTOs;

namespace ComputerSalesAPI.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .Include(p => p.Category)
                .OrderByDescending(p => p.Id)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Product>> GetHotProductsAsync()
        {
            return await _context.Products
                .Where(p => p.IsHot)
                .Include(p => p.Category)
                .OrderByDescending(p => p.Id)
                .ToListAsync();
        }

        public override async Task<PagedResult<Product>> ListAllPagedAsync(int pageIndex, int pageSize)
        {
            var totalCount = await _context.Products.CountAsync();
            var data = await _context.Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Product>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = data
            };
        }
    }
}
