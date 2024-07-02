using WebAppForSmartBusiness.Models.Helpers;

namespace WebAppForSmartBusiness.Models
{
    public class ProjectInputFilter
    {
        public IEnumerable<ProjectForView> Projects { get; set; }
        public Project ProjectForFilter { get; set; }
    }
}
