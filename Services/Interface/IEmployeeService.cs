using PayRollManagement.Model;
using PayRollManagement.ViewModel;
using System.Linq;

namespace PayRollManagement.Services.Interface
{
    public interface IEmployeeService
    {
        EmployeeViewModel GetEmployeeViewModelById(int id);
        List<EmployeeViewModel> GetAllEmployeeViewModels();
        void AddEmployee(Employee employee);
        void SalaryEmployee(Employee employee);
        void SalaryPayDetails(SalaryPayDetail salaryPayDetail);
        void UpdateEmployee(EmployeeViewModel employeeViewModel);
        void DeleteEmployee(int id);
        decimal CalculateDeductedSalary(int employeeId, int leaveDays);
        void UpdateSalaryDetails(int employeeId, decimal deductedAmount);
        decimal CalculateNetSalary(int employeeId);
        void TakeLeave(int employeeId, int LeaveDays);
    }
}
