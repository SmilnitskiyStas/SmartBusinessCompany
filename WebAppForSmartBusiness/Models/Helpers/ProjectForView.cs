namespace WebAppForSmartBusiness.Models.Helpers
{
    public class ProjectForView
    {
        public int ProjectId { get; set; }
        public string ProjectType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ProjectManager { get; set; }
        public string? Comment { get; set; }
        public string Status { get; set; }
    }
}
