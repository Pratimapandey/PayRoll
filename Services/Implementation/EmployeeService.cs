using PayRollManagement.Data;
using PayRollManagement.Model;
using PayRollManagement.Services.Interface;
using PayRollManagement.ViewModel;
using System;
using System.Linq;

namespace PayRollManagement.Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly PayRollDbContext _context;

        public EmployeeService(PayRollDbContext context)
        {
            _context = context;
        }
        public EmployeeViewModel GetEmployeeViewModelById(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == id);
            if (employee != null)
            {
                if (HasTakenLeave(id))
                {
                    var salaryPayDetail = _context.SalaryPayDetails
                        .FirstOrDefault(sd => sd.EmployeeId == id && sd.Month == DateTime.Now.Month && sd.Year == DateTime.Now.Year);

                    var deductedAmount = _context.SalaryPayDeductionDetails
                        .Where(d => d.EmployeeId == id && d.Month == DateTime.Now.Month && d.Year == DateTime.Now.Year)
                        .Sum(d => d.DeductedAmount);

                    return new EmployeeViewModel
                    {
                        EmployeeId = employee.EmployeeId,
                        Name = employee.Name,
                        Address = employee.Address,
                        LeaveStartDate = DateTime.Now, // You may need to change this
                        LeaveEndDate = DateTime.Now, // You may need to change this
                        BaseSalary = salaryPayDetail.BaseSalary , // Use null conditional operator to avoid null reference exception
                        NetSalary = salaryPayDetail.NetSalary, // Use null conditional operator to avoid null reference exception
                        DeductedAmount = deductedAmount
                    };
                }
                else
                {
                    // If employee hasn't taken leave, retrieve base salary and net salary from the employee object
                    return new EmployeeViewModel
                    {
                        EmployeeId = employee.EmployeeId,
                        Name = employee.Name,
                        Address = employee.Address,
                        LeaveStartDate = DateTime.Now, // You may need to change this
                        LeaveEndDate = DateTime.Now, // You may need to change this
                 
                        BaseSalary = employee.BaseSalary, // Use base salary from the employee table
                        NetSalary = employee.NetSalary, // Use net salary from the employee table
                        DeductedAmount = 0 // No deductions as employee didn't take leave
                    };
                }
            }
            return null; // or throw an exception if employee not found
        }

        private bool HasTakenLeave(int employeeId)
        {
            // Check if there are any deductions for the current month and year
            return _context.SalaryPayDeductionDetails.Any(d => d.EmployeeId == employeeId && d.Month == DateTime.Now.Month && d.Year == DateTime.Now.Year);
        }

        public List<EmployeeViewModel> GetAllEmployeeViewModels()
        {
            var employees = _context.Employees.ToList();
            var employeeViewModels = new List<EmployeeViewModel>();

            foreach (var employee in employees)
            {
                var salaryPayDetail = _context.SalaryPayDetails
                    .FirstOrDefault(sd => sd.EmployeeId == employee.EmployeeId && sd.Month == DateTime.Now.Month && sd.Year == DateTime.Now.Year);

                var deductedAmount = _context.SalaryPayDeductionDetails
                    .Where(d => d.EmployeeId == employee.EmployeeId && d.Month == DateTime.Now.Month && d.Year == DateTime.Now.Year)
                    .Sum(d => d.DeductedAmount);

                var employeeViewModel = new EmployeeViewModel
                {
                    EmployeeId = employee.EmployeeId,
                    Name = employee.Name,
                    Address = employee.Address,
                    LeaveStartDate = DateTime.Now, // You may need to change this
                    LeaveEndDate = DateTime.Now, // You may need to change this

                    BaseSalary = salaryPayDetail?.BaseSalary ?? 0, 
                    NetSalary = salaryPayDetail?.NetSalary ?? 0,
                    DeductedAmount = deductedAmount
                };

                employeeViewModels.Add(employeeViewModel);
            }

            return employeeViewModels;
        }


        public void AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void UpdateEmployee(EmployeeViewModel employeeViewModel)
        {
            var existingEmployee = _context.Employees.FirstOrDefault(e => e.EmployeeId == employeeViewModel.EmployeeId);
            if (existingEmployee == null)
            {
                // Handle scenario where the employee does not exist
                // You may throw an exception or log an error
                return;
            }

            // Update properties of existing employee entity
            existingEmployee.Name = employeeViewModel.Name;
            existingEmployee.Address = employeeViewModel.Address;
            existingEmployee.BaseSalary = employeeViewModel.BaseSalary;

            _context.SaveChanges();
        }

        public void DeleteEmployee(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
        }

        public decimal CalculateDeductedSalary(int employeeId, int leaveDays)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (employee != null)
            {
                // Calculate deducted salary based on the provided formula
                decimal deductedAmount = (employee.BaseSalary / 20) * leaveDays;
                return deductedAmount;
            }
            throw new InvalidOperationException("Employee not found");
        }

        public void UpdateSalaryDetails(int employeeId, decimal deductedAmount)
        {
            var salaryDetail = new SalaryDetail
            {
                EmployeeId = employeeId,
                BaseSalary = deductedAmount
            };
            _context.SalaryDetails.Add(salaryDetail);
            _context.SaveChanges();
        }

        public decimal CalculateNetSalary(int employeeId)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (employee != null)
            {
                decimal netSalary = employee.BaseSalary;

                // Subtract deductions from net salary
                var deductions = _context.SalaryDetails.Where(sd => sd.EmployeeId == employeeId).Sum(sd => sd.BaseSalary);
                netSalary -= deductions;

                return netSalary;
            }
            throw new InvalidOperationException("Employee not found");
        }
        public decimal CalculateDeductedSalary(decimal baseSalary, int workingDays, int numberOfLeaveDays)
        {
            // Calculate deducted salary based on the provided formula
            decimal deductedAmount = (baseSalary / workingDays) * numberOfLeaveDays;
            return deductedAmount;
        }
        public void TakeLeave(int employeeId, int numberOfDays)
        {
            try
            {
                var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
                if (employee != null)
                {
                    // Calculate deducted amount for leave
                    decimal deductedAmount = CalculateDeductedSalary(employee.BaseSalary, 20, numberOfDays);

                    // Calculate net salary
                    decimal netSalary = CalculateNetSalary(employeeId);
                    netSalary -= deductedAmount;

                    // Check if a deduction detail record already exists for the current month and year
                    var existingDeductionDetail = _context.SalaryPayDeductionDetails.FirstOrDefault(d =>
                        d.EmployeeId == employeeId && d.Month == DateTime.Now.Month && d.Year == DateTime.Now.Year);

                    if (existingDeductionDetail != null)
                    {
                        // Update existing deduction detail with the new deducted amount
                        existingDeductionDetail.DeductedAmount += deductedAmount;
                    }
                    else
                    {
                        // Create new salary details for the current month and year
                        var salaryPayDetail = new SalaryPayDetail
                        {
                            EmployeeId = employeeId,
                            Month = DateTime.Now.Month, // Assuming current month
                            Year = DateTime.Now.Year, // Assuming current year
                            BaseSalary = employee.BaseSalary, // Main salary is in base salary
                            NetSalary = netSalary
                        };
                        _context.SalaryPayDetails.Add(salaryPayDetail);

                        // Save deduction details
                        var deductionDetail = new SalaryPayDeductionDetail
                        {
                            EmployeeId = employeeId,
                            Month = DateTime.Now.Month, // Assuming current month
                            Year = DateTime.Now.Year, // Assuming current year
                            DeductionName = "Leave Deduction", // You can provide a meaningful deduction name
                            DeductedAmount = deductedAmount,
                            // Assign the corresponding salary detail
                        };
                        _context.SalaryPayDeductionDetails.Add(deductionDetail);
                    }

                    _context.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException("Employee not found");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                throw new InvalidOperationException("An error occurred while taking leave: " + ex.Message);
            }
        }

    }



}


