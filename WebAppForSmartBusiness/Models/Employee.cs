namespace WebAppForSmartBusiness.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public int UserId { get; set; }
        public int SubdivisionId { get; set; }
        public int PositionId { get; set; }
        public string Status { get; set; }
        public int PeoplePartnerId { get; set; }
        public int OutOfOfficeBalance { get; set; }
    }
}
