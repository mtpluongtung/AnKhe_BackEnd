namespace ComputerSalesAPI.Core.Entities
{
    public class WebpageConfig
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Keyword { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
        public string FaviconUrl { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string PassEmail { get; set; } = string.Empty;
        public string ServerEmail { get; set; } = string.Empty;
        public string PortEmail { get; set; } = string.Empty;
        public string GoogleAnalytics { get; set; } = string.Empty;
        public string GoogleWebmaster { get; set; } = string.Empty;
        public string IntroContent { get; set; } = string.Empty;
        public string FooterContent { get; set; } = string.Empty;
        public string FacebookId { get; set; } = string.Empty;
        public string FacebookSecret { get; set; } = string.Empty;
        public string FacebookPage { get; set; } = string.Empty;
        public string TwitterId { get; set; } = string.Empty;
        public string GoogleId { get; set; } = string.Empty;
        public string KeyCode { get; set; } = string.Empty;
    }
}
