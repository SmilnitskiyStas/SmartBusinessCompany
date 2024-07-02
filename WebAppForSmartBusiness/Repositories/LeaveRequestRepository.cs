using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WebAppForSmartBusiness.Models;
using WebAppForSmartBusiness.Repositories.IRepositories;

namespace WebAppForSmartBusiness.Repositories
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly string _connectionString;

        public LeaveRequestRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public LeaveRequest CreateLeaveRequst(LeaveRequest leaveRequest)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"INSERT INTO LeaveRequests (Employeeid, AbsenceReasonid, StartDate, EndDate, Comment, Status) " +
                    $"VALUES ({leaveRequest.Employeeid}, {leaveRequest.AbsenceReasonid}, '{leaveRequest.StartDate}', " +
                    $"'{leaveRequest.EndDate}', '{leaveRequest.Comment}', '{leaveRequest.Status}')";

                return db.Query<LeaveRequest>(sqlQuery).FirstOrDefault();
            }
        }

        public bool DeleteLeaveRequstById(int leaveId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<object>($"DELETE FROM LeaveRequests WHERE LeaveId = {leaveId}; SELECT CAST (SCOPE_IDENTITY() AS int);") is null;
            }
        }

        public bool ExistsLeaveRequestById(int leaveId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<LeaveRequest>($"SELECT * FROM LeaveRequests WHERE LeaveId = {leaveId};").Any();
            }
        }

        public LeaveRequest GetLeaveRequestById(int leaveId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<LeaveRequest>($"SELECT * FROM LeaveRequests WHERE LeaveId = {leaveId};").FirstOrDefault();
            }
        }

        public LeaveRequest GetLeaveRequestByName(string leaveName)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<LeaveRequest>($"SELECT * FROM LeaveRequests WHERE LeaveId = {leaveName};").FirstOrDefault();
            }
        }

        public IEnumerable<LeaveRequest> GetLeaveRequests()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<LeaveRequest>($"SELECT * FROM LeaveRequests ").ToList();
            }
        }

        public LeaveRequest UpdateLeaveRequst(LeaveRequest leaveRequest)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"UPDATE LeaveRequests SET Employeeid = {leaveRequest.Employeeid}, AbsenceReasonid = {leaveRequest.AbsenceReasonid}, " +
                    $"StartDate = '{leaveRequest.StartDate}', EndDate = '{leaveRequest.EndDate}', Comment = '{leaveRequest.Comment}', Status = '{leaveRequest.Status}' " +
                    $"WHERE LeaveId = {leaveRequest.LeaveId}";

                return db.Query<LeaveRequest>(sqlQuery).FirstOrDefault();
            }
        }
    }
}
