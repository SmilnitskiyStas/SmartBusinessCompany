using WebAppForSmartBusiness.Models;

namespace WebAppForSmartBusiness.Repositories.IRepositories
{
    public interface IApprovalRequestRepository
    {
        IEnumerable<ApprovalRequest> GetApprovalRequests();
        ApprovalRequest GetApprovalRequestById(int approvalId);
        ApprovalRequest GetApprovalRequestByName(string ApprovalName);
        ApprovalRequest CreateApprovalRequest(ApprovalRequest approvalRequest);
        ApprovalRequest UpdateApprovalRequest(ApprovalRequest approvalRequest);
        bool DeleteApprovalRequestById(int approvalId);
        bool ExistsApprovalRequestById(int approvalId);
    }
}
