using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WebAppForSmartBusiness.Models;
using WebAppForSmartBusiness.Repositories.IRepositories;

namespace WebAppForSmartBusiness.Repositories
{
    public class ApprovalRequestRepository : IApprovalRequestRepository
    {
        private readonly string _connectionString;

        public ApprovalRequestRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ApprovalRequest CreateApprovalRequest(ApprovalRequest approvalRequest)
        {
            using (IDbConnection db =  new SqlConnection(_connectionString))
            {
                var sqlQuery = $"INSERT INTO ApprovalRequests (ApploverId, LeaveRequestId, Status, Comment) " +
                    $"VALUES ({approvalRequest.ApproverId}, {approvalRequest.LeaveRequestId}, '{approvalRequest.Status}', '{approvalRequest.Comment}')";

                return db.Query<ApprovalRequest>(sqlQuery).FirstOrDefault();
            }
        }

        public bool DeleteApprovalRequestById(int approvalId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<object>($"DELETE FROM ApprovalRequests WHERE ApprovalId = {approvalId}") is null;
            }
        }

        public bool ExistsApprovalRequestById(int approvalId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<ApprovalRequest>($"SELECT * FROM ApprovalRequests WHERE ApprovalId = {approvalId}").Any();
            }
        }

        public ApprovalRequest GetApprovalRequestById(int approvalId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<ApprovalRequest>($"SELECT * FROM ApprovalRequests WHERE ApprovalId = {approvalId}").FirstOrDefault();
            }
        }

        public ApprovalRequest GetApprovalRequestByName(string ApprovalName)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<ApprovalRequest>($"SELECT * FROM ApprovalRequests WHERE ApprovalId = {ApprovalName}").FirstOrDefault();
            }
        }

        public IEnumerable<ApprovalRequest> GetApprovalRequests()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<ApprovalRequest>($"SELECT * FROM ApprovalRequests").ToList();
            }
        }

        public ApprovalRequest UpdateApprovalRequest(ApprovalRequest approvalRequest)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"UPDATE ApprovalRequests SET Applover = {approvalRequest.ApproverId}, LeaveRequest = {approvalRequest.LeaveRequestId}, " +
                    $"Status = {approvalRequest.Status}, Comment = {approvalRequest.Status} " +
                    $"WHERE ApprovarId = {approvalRequest.ApprovalId}";

                db.Execute(sqlQuery);

                return db.Query<ApprovalRequest>($"SELECT * FROM ApprovalRequests WHERE ApprovalId = {approvalRequest.ApprovalId}").FirstOrDefault();
            }
        }
    }
}
