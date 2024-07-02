namespace WebAppForSmartBusiness.Models
{
    public class LeaveRequest
    {
        public int LeaveId { get; set; }
        public int Employeeid { get; set; }
        public int AbsenceReasonid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Comment { get; set; }
        public string Status { get; set; } = "New";
    }
}
