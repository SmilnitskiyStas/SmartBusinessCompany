using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WebAppForSmartBusiness.Models;
using WebAppForSmartBusiness.Repositories.IRepositories;

namespace WebAppForSmartBusiness.Repositories
{
    public class SubdivisionRepository : ISubdivisionRepository
    {
        private readonly string _connectionString;

        public SubdivisionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Subdivision CreateSubdivision(Subdivision subdivision)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"INSERT INTO Subdivisions (Name) " +
                    $"VALUES ('{subdivision.Name}')";

                return db.Query<Subdivision>(sqlQuery).FirstOrDefault();
            }
        }

        public bool DeleteSubdivisionById(int subdivisionId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<object>($"DELETE FROM Subdivisions WHERE SubdivisionId = {subdivisionId}; SELECT CAST (SCOPE_IDENTITY() AS int);") is null;
            }
        }

        public bool ExistsSubdivisionById(int subdivisionId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Subdivision>($"SELECT * FROM Subdivisions WHERE SubdivisionId = {subdivisionId}").Any();
            }
        }

        public bool ExistsSubdivisionByName(string subdivisionName)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Subdivision>($"SELECT * FROM Subdivisions WHERE Name = '{subdivisionName}'").Any();
            }
        }

        public Subdivision GetSubdivisionById(int subdivisionId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Subdivision>($"SELECT * FROM Subdivisions WHERE SubdivisionId = {subdivisionId}").FirstOrDefault();
            }
        }

        public Subdivision GetSubdivisionByName(string subdivisionName)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Subdivision>($"SELECT * FROM Subdivisions WHERE Name = '{subdivisionName}'").FirstOrDefault();
            }
        }

        public IEnumerable<Subdivision> GetSubdivisions()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Subdivision>($"SELECT * FROM Subdivisions").ToList();
            }
        }

        public Subdivision UpdateSubdivision(Subdivision subdivision)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"UPDATE Subdivisions SET Name = '{subdivision.Name}' " +
                    $"WHERE SubdivisionId = {subdivision.SubdivisionId}";

                db.Execute(sqlQuery);

                return db.Query<Subdivision>($"SELECT * FROM Subdivisions WHERE SubdivisionId = {subdivision.SubdivisionId}").FirstOrDefault();
            }
        }
    }
}
