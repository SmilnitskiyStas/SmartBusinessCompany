using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WebAppForSmartBusiness.Models;
using WebAppForSmartBusiness.Models.Helpers;
using WebAppForSmartBusiness.Repositories.IRepositories;

namespace WebAppForSmartBusiness.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly string _connectionString;

        public ProjectRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Project CreateProject(Project project)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "";

                if (project.EndDate != null)
                {
                    sqlQuery = $"INSERT INTO Projects (ProjectTypeId, StartDate, EndDate, ProjectManagerId, Comment, Status) " +
                    $"VALUES ({project.ProjectTypeId}, '{project.StartDate.Year}-{project.StartDate.Month}-{project.StartDate.Day}', " +
                    $"'{project.EndDate.Value.Year}-{project.EndDate.Value.Month}-{project.EndDate.Value.Day}', {project.ProjectManagerId}, '{project.Comment}', '{project.Status}'); " +
                    $"SELECT CAST (SCOPE_IDENTITY() AS int);";
                }
                else
                {
                    sqlQuery = $"INSERT INTO Projects (ProjectTypeId, StartDate, ProjectManagerId, Comment, Status) " +
                    $"VALUES ({project.ProjectTypeId}, '{project.StartDate.Year}-{project.StartDate.Month}-{project.StartDate.Day}', " +
                    $"{project.ProjectManagerId}, '{project.Comment}', '{project.Status}'); " +
                    $"SELECT CAST (SCOPE_IDENTITY() AS int);";
                }
                

                var index = db.Query<int>(sqlQuery).FirstOrDefault();

                return db.Query<Project>($"SELECT * FROM Projects WHERE ProjectId = {index}").FirstOrDefault();
            }
        }

        public bool DeleteProjectById(int projectId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<object>($"DELETE FROM Projects WHERE ProjectId = {projectId}; SELECT CAST (SCOPE_IDENTITY() AS int);") is null;
            }
        }

        public bool ExistsProjectById(int projectId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Project>($"SELECT * FROM Projects WHERE ProjectId = {projectId}").Any();
            }
        }

        public ProjectForView GetProjectById(int projectId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "SELECT " +
                    "p.ProjectId AS ProjectId, " +
                    "pt.Name AS ProjectType, " +
                    "u.FirstName + ' ' + u.LastName AS ProjectManager, " +
                    "p.StartDate AS StartDate, " +
                    "p.EndDate AS EndDate, " +
                    "p.Comment AS Comment " +
                    "FROM Projects AS p " +
                    "LEFT JOIN ProjectTypes AS pt ON p.ProjectTypeId = pt.ProjectTypeId " +
                    "LEFT JOIN Users AS u ON p.ProjectManagerId = u.UserId " +
                    $"WHERE ProjectId = {projectId}";

                return db.Query<ProjectForView>(sqlQuery).FirstOrDefault();
            }
        }

        public Project GetProjectByName(string projectName)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Project>($"SELECT * FROM Projects WHERE Name = {projectName}").FirstOrDefault();
            }
        }

        public IEnumerable<Project> GetProjects()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Project>($"SELECT * FROM Projects").ToList();
            }
        }

        public IEnumerable<ProjectForView> GetProjectsFullInfo()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "SELECT " +
                    "p.ProjectId AS ProjectId, " +
                    "pt.Name AS ProjectType, " +
                    "u.FirstName + ' ' + u.LastName AS ProjectManager, " +
                    "p.StartDate AS StartDate, " +
                    "p.EndDate AS EndDate, " +
                    "p.Comment AS Comment " +
                    "FROM Projects AS p " +
                    "LEFT JOIN ProjectTypes AS pt ON p.ProjectTypeId = pt.ProjectTypeId " +
                    "LEFT JOIN Users AS u ON p.ProjectManagerId = u.UserId";

                return db.Query<ProjectForView>(sqlQuery).ToList();
            }
        }

        public Project UpdateProject(Project project)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"UPDATE Projects SET ProjectTypeId = {project.ProjectTypeId}, StartDate = '{project.StartDate}', " +
                    $"EndDate = '{project.EndDate}', ProjectManagerId = {project.ProjectManagerId}, Comment = '{project.Comment}', Status = '{project.Status}' " +
                    $"WHERE ProjectId = {project.ProjectId}";

                db.Execute(sqlQuery);

                return db.Query<Project>($"SELECT * FROM Projects WHERE ProjectId = {project.ProjectId}").FirstOrDefault();
            }
        }
    }
}
