namespace WebAppForSmartBusiness.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public int ProjectTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int ProjectManagerId { get; set; }
        public string? Comment { get; set; }
        public string Status { get; set; }
    }
}
