using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WebAppForSmartBusiness.Models;
using WebAppForSmartBusiness.Repositories.IRepositories;

namespace WebAppForSmartBusiness.Repositories
{
    public class ProjectTypeRepository : IProjectTypeRepository
    {
        private readonly string _connectionString;

        public ProjectTypeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ProjectType CreateProjectType(ProjectType projectType)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"INSERT INTO ProjectTypes (Name) " +
                    $"VALUES ('{projectType.Name}'); SELECT CAST (SCOPE_IDENTITY() AS int);";

                var index = db.Query<int>(sqlQuery).FirstOrDefault();

                return db.Query<ProjectType>($"SELECT * FROM ProjectTypes WHERE ProjectTypeId = {index}").FirstOrDefault();
            }
        }

        public bool DeleteProjectTypeById(int projectTypeId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<object>($"DELETE FROM ProjectTypes WHERE ProjectTypeId = {projectTypeId}; SELECT CAST (SCOPE_IDENTITY() AS int);") is null;
            }
        }

        public bool ExistsProjectTypeById(int projectTypeId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<ProjectType>($"SELECT * FROM ProjectTypes WHERE ProjectTypeId = {projectTypeId}").Any();
            }
        }

        public bool ExistsProjectTypeByName(string projectTypeName)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<ProjectType>($"SELECT * FROM ProjectTypes WHERE Name = '{projectTypeName}'").Any();
            }
        }

        public ProjectType GetProjectTypeById(int projectTypeId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<ProjectType>($"SELECT * FROM ProjectTypes WHERE ProjectTypeId = {projectTypeId}").FirstOrDefault();
            }
        }

        public ProjectType GetProjectTypeByName(string projectTypeName)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<ProjectType>($"SELECT * FROM ProjectTypes WHERE Name = '{projectTypeName}'").FirstOrDefault();
            }
        }

        public IEnumerable<ProjectType> GetProjectTypes()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<ProjectType>($"SELECT * FROM ProjectTypes").ToList();
            }
        }

        public ProjectType UpdateProjectType(ProjectType projectType)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"UPDATE ProjectTypes SET Name = '{projectType.Name}' " +
                    $"WHERE ProjectTypeId = {projectType.ProjectTypeId}";

                db.Execute(sqlQuery);
                return db.Query<ProjectType>($"SELECT * FROM ProjectTypes WHERE ProjectTypeId = {projectType.ProjectTypeId}").FirstOrDefault();
            }
        }
    }
}
