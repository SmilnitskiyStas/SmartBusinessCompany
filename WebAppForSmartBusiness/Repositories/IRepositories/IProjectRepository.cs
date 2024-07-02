using WebAppForSmartBusiness.Models;
using WebAppForSmartBusiness.Models.Helpers;

namespace WebAppForSmartBusiness.Repositories.IRepositories
{
    public interface IProjectRepository
    {
        IEnumerable<Project> GetProjects();
        IEnumerable<ProjectForView> GetProjectsFullInfo();
        ProjectForView GetProjectById(int projectId);
        Project GetProjectByName(string projectName);
        Project CreateProject(Project project);
        Project UpdateProject(Project project);
        bool DeleteProjectById(int projectId);
        bool ExistsProjectById(int projectId);
    }
}
