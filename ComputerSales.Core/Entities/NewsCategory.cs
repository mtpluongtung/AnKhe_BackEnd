namespace ComputerSalesAPI.Core.Entities
{
    public class NewsCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int OrderBy { get; set; }
        public int? ParentId { get; set; }
    }
}
