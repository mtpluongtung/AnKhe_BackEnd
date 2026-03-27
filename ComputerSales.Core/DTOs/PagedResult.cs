using System;
using System.Collections.Generic;

namespace ComputerSalesAPI.Core.DTOs
{
    public class PagedResult<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IReadOnlyList<T> Data { get; set; } = new List<T>();
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}
