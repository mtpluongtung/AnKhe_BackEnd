namespace ComputerSalesAPI.Core.Entities
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int CategoryId { get; set; }
        public NewsCategory? Category { get; set; }
        public int ViewsCount { get; set; }
        public bool IsHot { get; set; }
    }
}
