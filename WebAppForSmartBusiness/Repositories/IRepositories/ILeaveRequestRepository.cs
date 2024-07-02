using WebAppForSmartBusiness.Models;

namespace WebAppForSmartBusiness.Repositories.IRepositories
{
    public interface ILeaveRequestRepository
    {
        IEnumerable<LeaveRequest> GetLeaveRequests();
        LeaveRequest GetLeaveRequestById(int leaveId);
        LeaveRequest GetLeaveRequestByName(string leaveName);
        LeaveRequest CreateLeaveRequst(LeaveRequest leaveRequest);
        LeaveRequest UpdateLeaveRequst(LeaveRequest leaveRequest);
        bool DeleteLeaveRequstById(int leaveId);
        bool ExistsLeaveRequestById(int leaveId);
    }
}
