using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayRollManagement.Model
{
    public class SalaryPayDeductionDetail
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SalaryPayDeductionDetailId { get; set; }
        public int EmployeeId { get; set; } 
        public int Month { get; set; } 
        public int Year { get; set; } 
        public decimal DeductedAmount { get; set; }

        // Navigation property to the SalaryPayDetail entity
        /*public SalaryPayDetail SalaryPayDetail { get; set; }*/
    }
}
