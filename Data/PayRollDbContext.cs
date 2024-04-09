using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using PayRollManagement.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PayRollManagement.Data
{
    public class PayRollDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<SalaryDetail> SalaryDetails { get; set; }
        public DbSet<SalaryPayDetail> SalaryPayDetails { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }

        public DbSet<SalaryPayDeductionDetail> SalaryPayDeductionDetails { get; set; }

        public PayRollDbContext(DbContextOptions<PayRollDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });

      
            modelBuilder.Entity<SalaryPayDeductionDetail>()
                .HasKey(s => new { s.SalaryPayDeductionDetailId, s.EmployeeId, s.Month, s.Year });


   
        }
    }
}
