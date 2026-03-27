namespace ComputerSalesAPI.Core.Entities
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LinkUrl { get; set; } = string.Empty;
        public int OrderBy { get; set; }
        public string Position { get; set; } = string.Empty;
        public int? ParentId { get; set; }
    }
}
