namespace PayRollManagement.Model
{
    public class SalaryPayDetail
    {
        public int SalaryPayDetailId { get; set; }
        public int EmployeeId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal BaseSalary { get; set; }

        public decimal NetSalary { get; set; }
        public Employee Employee { get; set; }


        public List<SalaryPayDeductionDetail> SalaryPayDeductionDetails { get; set; }
    }
}
