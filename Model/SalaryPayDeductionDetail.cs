namespace PayRollManagement.Model
{
    public class SalaryPayDeductionDetail
    {
        public int SalaryPayDeductionDetailId { get; set; }
        public int EmployeeId { get; set; } // Add this property
        public int Month { get; set; } // Add this property
        public int Year { get; set; } // Add this property
        public string DeductionName { get; set; }
        public decimal DeductedAmount { get; set; }

        // Navigation property to the SalaryPayDetail entity
        public SalaryPayDetail SalaryPayDetail { get; set; }
    }
}
