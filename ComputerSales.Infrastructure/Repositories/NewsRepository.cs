using ComputerSalesAPI.Core.DTOs;
using ComputerSalesAPI.Core.Entities;
using ComputerSalesAPI.Core.Interfaces;
using ComputerSalesAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerSalesAPI.Infrastructure.Repositories
{
    public class NewsRepository : GenericRepository<News>, INewsRepository
    {
        public NewsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<PagedResult<News>> ListAllPagedAsync(int pageIndex, int pageSize)
        {
            return await GetNewsPagedAsync(null, pageIndex, pageSize);
        }

        public async Task<PagedResult<News>> GetNewsPagedAsync(string? searchTerm, int pageIndex, int pageSize)
        {
            var query = _context.News.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(n => n.Title.Contains(searchTerm) || n.Content.Contains(searchTerm));
            }

            var totalCount = await query.CountAsync();
            var data = await query
                .OrderByDescending(n => n.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<News>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = data
            };
        }
    }
}
