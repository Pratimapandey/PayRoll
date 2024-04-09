namespace PayRollManagement.ViewModel
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public string PaymentStatus { get; set; }
        public DateTime DateofPayment { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal NetSalary { get; set; }
        public decimal DeductedAmount { get; set;}
        public int LeaveDays { get; set; }


    }
}


