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
                var salaryPayDetail = _context.SalaryDetails.FirstOrDefault(sd => sd.EmployeeId == id);
                var deductedAmount = _context.SalaryPayDeductionDetails
                    .Where(d => d.EmployeeId == id && d.Month == DateTime.Now.Month && d.Year == DateTime.Now.Year)
                    .Sum(d => d.DeductedAmount);

                // Get leave request details for the employee
                var leaveRequest = _context.LeaveRequests.FirstOrDefault(lr => lr.EmployeeId == id);
                int leaveDays = leaveRequest != null ? leaveRequest.LeaveDays : 0;

                // Get salary pay details for the employee
                var salaryPayDetails = _context.SalaryPayDetails.FirstOrDefault(sp => sp.EmployeeId == id);
                DateTime dateOfPayment = salaryPayDetails.DateofPayment;
                string paymentStatus = salaryPayDetails.PaymentStatus;

                return new EmployeeViewModel
                {
                    EmployeeId = employee.EmployeeId,
                    Name = employee.Name,
                    Address = employee.Address,
                    BaseSalary = salaryPayDetail.BaseSalary,
                    NetSalary = salaryPayDetail.NetSalary,
                    DeductedAmount = deductedAmount,
                    LeaveDays = leaveDays,
                    DateofPayment = dateOfPayment,
                    PaymentStatus = paymentStatus
                };
            }
            return null; // or throw an exception if employee not found
        }

        public List<EmployeeViewModel> GetAllEmployeeViewModels()
        {
            var employees = _context.Employees.ToList();
            var employeeViewModels = new List<EmployeeViewModel>();

            foreach (var employee in employees)
            {
                var salaryPayDetail = _context.SalaryDetails.FirstOrDefault(sd => sd.EmployeeId == employee.EmployeeId);
                var deductedAmount = _context.SalaryPayDeductionDetails
                    .Where(d => d.EmployeeId == employee.EmployeeId && d.Month == DateTime.Now.Month && d.Year == DateTime.Now.Year)
                    .Sum(d => d.DeductedAmount);

                var employeeViewModel = new EmployeeViewModel
                {
                    EmployeeId = employee.EmployeeId,
                    Name = employee.Name,
                    Address = employee.Address,
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
            try
            {
                // Add employee to the database
                _context.Employees.Add(employee);

                // Save changes to the Employees table to generate the EmployeeId
                _context.SaveChanges();


            }
            catch (Exception ex)
            {
                // Handle exception appropriately
                Console.WriteLine($"An error occurred while adding the employee: {ex.Message}");
                throw; // Rethrow the exception to be handled at a higher level
            }
        }
        public void SalaryEmployee(Employee employee)
        {
            // Create and save initial salary details for the new employee
            var salaryDetail = new SalaryDetail
            {
                EmployeeId = employee.EmployeeId, // Use the generated EmployeeId
                BaseSalary = employee.BaseSalary,
                NetSalary = employee.BaseSalary,
                Name = employee.Name,
                Address = employee.Address,
                // Set other properties as needed
            };

            // Add the salary detail to the SalaryDetails table
            _context.SalaryDetails.Add(salaryDetail);

            // Save changes to record the initial salary details
            _context.SaveChanges();
        }
        public void SalaryPayDetails(SalaryPayDetail salaryPayDetail)
        {
            var newSalaryPayDetail = new SalaryPayDetail()
            {
                EmployeeId = salaryPayDetail.EmployeeId,
                DateofPayment = salaryPayDetail.DateofPayment,
                PaymentStatus = salaryPayDetail.PaymentStatus // Use the generated EmployeeId
            };

            _context.SalaryPayDetails.Add(newSalaryPayDetail);
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
                var deductions = _context.SalaryPayDeductionDetails.Where(sd => sd.EmployeeId == employeeId)
                                                                   .Sum(sd => sd.DeductedAmount);
                netSalary -= deductions;

                return netSalary;
            }
            throw new InvalidOperationException("Employee not found");
        }


        public void TakeLeave(int employeeId, int LeaveDays)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (employee != null)
            {
                // Update leave request details
                var leaveReq = _context.LeaveRequests.FirstOrDefault(lr => lr.EmployeeId == employeeId);
                if (leaveReq != null)
                {
                    leaveReq.StartDate = DateTime.Now; // Update start date assuming leave starts now
                    leaveReq.EndDate = leaveReq.StartDate.AddDays(LeaveDays); // Update end date
                    leaveReq.LeaveDays = LeaveDays; // Update leave days
                }
                else
                {
                    // Create new leave request if not exists
                    leaveReq = new LeaveRequest
                    {
                        EmployeeId = employeeId,
                        StartDate = DateTime.Now, // Start date assuming leave starts now
                        EndDate = DateTime.Now.AddDays(LeaveDays), // End date
                        LeaveDays = LeaveDays // Leave days
                    };
                    _context.LeaveRequests.Add(leaveReq);
                }

                // Update salary details for deductions
                var salaryDetail = _context.SalaryDetails.FirstOrDefault(sd => sd.EmployeeId == employeeId);
                if (salaryDetail != null)
                {
                    // Calculate deducted amount based on base salary and leave days
                    decimal deductedAmount = CalculateDeductedSalary(employeeId, LeaveDays);

                    // Update net salary by deducting the deducted amount
                    salaryDetail.NetSalary -= deductedAmount;

                    // Save deduction details
                    var deductionDetail = new SalaryPayDeductionDetail
                    {
                        EmployeeId = employeeId,
                        Month = DateTime.Now.Month, // Assuming current month
                        Year = DateTime.Now.Year, // Assuming current year
                       // You can provide a meaningful deduction name
                        DeductedAmount = deductedAmount
                    };
                    _context.SalaryPayDeductionDetails.Add(deductionDetail);

                    _context.SaveChanges();
                }

                else
                {
                    throw new InvalidOperationException("Employee not found");
                }
            }



        }



    }
}


