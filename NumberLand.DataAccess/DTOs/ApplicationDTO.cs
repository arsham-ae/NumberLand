namespace NumberLand.DataAccess.DTOs
{
    public class ApplicationDTO
    {
        public int appId { get; set; }
        public string appSlug { get; set; }
        public string appName { get; set; }
        public string appContent { get; set; }
        public string appIcon { get; set; }
    }
    public class CreateApplicationDTO
    {
        public int appId { get; set; }
        public string appSlug { get; set; }
        public string appName { get; set; }
        public string? appContent { get; set; }
    }
}
