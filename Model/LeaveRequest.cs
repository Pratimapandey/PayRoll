namespace PayRollManagement.Model
{
    public class LeaveRequest
    {
        public int LeaveRequestId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int LeaveDays { get; set; }
    }
}
