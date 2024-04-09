namespace PayRollManagement.Model
{
    public class SalaryDetail
    {
        public int SalaryDetailId { get; set; }
        public int EmployeeId { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal NetSalary { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        


        // Navigation property to the Employee entity
        public Employee employee { get; set; }
    }
}
