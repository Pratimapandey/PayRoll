namespace PayRollManagement.Model
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal NetSalary { get; set; }
//public string LeaveRequest { get; set; }

        // Navigation property to the SalaryDetails entity
        //public List<SalaryDetail> SalaryDetails { get; set; }

        //// Navigation property to the SalaryPayDetails entity
        /*  public List<SalaryPayDetail> SalaryPayDetails { get; set; }*/
        //public int LeaveBalance { get; internal set; }

    }
}
