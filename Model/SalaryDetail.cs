namespace PayRollManagement.Model
{
    public class SalaryDetail
    {
        public int SalaryDetailId { get; set; }
        public int EmployeeId { get; set; }
        public decimal BaseSalary { get; set; }

        // Navigation property to the Employee entity
        public Employee Employee { get; set; }
    }
}
