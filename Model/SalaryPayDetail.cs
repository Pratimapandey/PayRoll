namespace PayRollManagement.Model
{
    public class SalaryPayDetail
    {
        public int SalaryPayDetailId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime DateofPayment { get; set; }
        public string PaymentStatus { get; set; }

        //public Employee Employee { get; set; }


        //public List<SalaryPayDeductionDetail> SalaryPayDeductionDetails { get; set; }
    }
}
