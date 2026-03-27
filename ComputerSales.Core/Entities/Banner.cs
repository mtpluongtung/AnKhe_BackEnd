namespace ComputerSalesAPI.Core.Entities
{
    public class Banner
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string LinkUrl { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Info { get; set; } = string.Empty;
        public string Status { get; set; } = "Active";
    }
}
