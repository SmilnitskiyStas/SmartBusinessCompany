using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WebAppForSmartBusiness.Models;
using WebAppForSmartBusiness.Repositories.IRepositories;

namespace WebAppForSmartBusiness.Repositories
{
    public class AbsenceReasonRepository : IAbsenceReasonRepository
    {
        private readonly string _connectionString;

        public AbsenceReasonRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public AbsenceReason CreateAbsenceReason(AbsenceReason absenceReason)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"INSERT INTO AbsenceReasons (Name) " +
                    $"VALUES ('{absenceReason.Name}')";

                return db.Query<AbsenceReason>(sqlQuery).FirstOrDefault();
            }
        }

        public bool DeleteAbsenceReasonById(int absenceId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<object>($"DELETE FROM AbsenceReasons WHERE AbsenceId = {absenceId}; SELECT CAST (SCOPE_IDENTITY() AS int);") is null;
            }
        }

        public bool ExistsAbsenceReasonById(int absenceId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<AbsenceReason>($"SELECT * FROM AbsenceReasons WHERE AbsenceId = {absenceId}").Any();
            }
        }

        public AbsenceReason GetAbsenceReasonById(int absenceId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<AbsenceReason>($"SELECT * FROM AbsenceReasons WHERE AbsenceId = {absenceId}").FirstOrDefault();
            }
        }

        public AbsenceReason GetAbsenceReasonByName(string absenceName)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<AbsenceReason>($"SELECT * FROM AbsenceReasons WHERE Name = {absenceName}").FirstOrDefault();
            }
        }

        public IEnumerable<AbsenceReason> GetAbsenceReasons()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<AbsenceReason>($"SELECT * FROM AbsenceReasons").ToList();
            }
        }

        public AbsenceReason UpdateAbsenceReason(AbsenceReason absenceReason)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"UPDATE AbsenceReasons SET (Name) " +
                    $"VALUES ('{absenceReason.Name}') " +
                    $"WHERE AsenceId = {absenceReason.AbsenseId}";

                return db.Query<AbsenceReason>(sqlQuery).FirstOrDefault();
            }
        }
    }
}
