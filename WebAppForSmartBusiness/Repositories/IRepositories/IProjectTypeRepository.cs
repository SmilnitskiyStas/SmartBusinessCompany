using WebAppForSmartBusiness.Models;

namespace WebAppForSmartBusiness.Repositories.IRepositories
{
    public interface IProjectTypeRepository
    {
        IEnumerable<ProjectType> GetProjectTypes();
        ProjectType GetProjectTypeById(int projectTypeId);
        ProjectType GetProjectTypeByName(string projectTypeName);
        ProjectType CreateProjectType(ProjectType projectType);
        ProjectType UpdateProjectType(ProjectType projectType);
        bool DeleteProjectTypeById(int projectTypeId);
        bool ExistsProjectTypeById(int projectTypeId);
        bool ExistsProjectTypeByName(string projectTypeName);
    }
}
