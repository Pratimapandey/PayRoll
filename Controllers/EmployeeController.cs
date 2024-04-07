using Microsoft.AspNetCore.Mvc;
using PayRollManagement.Model;
using PayRollManagement.Services.Interface;
using PayRollManagement.ViewModel;
using System;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet("{id}")]
    public ActionResult<EmployeeViewModel> GetEmployeeById(int id)
    {
        var employee = _employeeService.GetEmployeeViewModelById(id);
        if (employee == null)
        {
            return NotFound();
        }

        var employeeViewModel = new EmployeeViewModel
        {
            EmployeeId=employee.EmployeeId,
            Name = employee.Name,
            Address = employee.Address,
            BaseSalary = employee.BaseSalary,
            NetSalary= employee.NetSalary,
            DeductedAmount= employee.DeductedAmount,
        };

        return employeeViewModel;
    }

    [HttpGet]
    public ActionResult<IEnumerable<EmployeeViewModel>> GetAllEmployees()
    {
        var employees = _employeeService.GetAllEmployeeViewModels();
        var employeeViewModels = new List<EmployeeViewModel>();

        foreach (var employee in employees)
        {
            var employeeViewModel = new EmployeeViewModel
            {
                EmployeeId=employee.EmployeeId,
                Name = employee.Name,
                Address = employee.Address,
                BaseSalary = employee.BaseSalary
            };

            employeeViewModels.Add(employeeViewModel);
        }

        return employeeViewModels;
    }

    [HttpPost]
    public IActionResult AddEmployee(Employee employee)
    {
        var Employee = new Employee
        {
            Name = employee.Name,
            Address = employee.Address,
            BaseSalary = employee.BaseSalary
        };

        _employeeService.AddEmployee(employee);
        return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.EmployeeId }, employee);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateEmployee(int id, EmployeeViewModel employeeViewModel)
    {
        var existingEmployee = _employeeService.GetEmployeeViewModelById(id);
        if (existingEmployee == null)
        {
            return NotFound();
        }

        // Update properties of existing employee entity
        existingEmployee.Name = employeeViewModel.Name;
        existingEmployee.Address = employeeViewModel.Address;
        existingEmployee.BaseSalary = employeeViewModel.BaseSalary;

        _employeeService.UpdateEmployee(existingEmployee);
        return NoContent();
    }


[HttpDelete("{id}")]
    public IActionResult DeleteEmployee(int id)
    {
        var existingEmployee = _employeeService.GetEmployeeViewModelById(id);
        if (existingEmployee == null)
        {
            return NotFound();
        }

        _employeeService.DeleteEmployee(id);
        return NoContent();
    }

    [HttpPost("take-leave")]
  
    public IActionResult TakeLeave(LeaveRequest leaveRequest)
    {
        try
        {
            // Convert EmployeeId to int if necessary
            int employeeId = Convert.ToInt32(leaveRequest.EmployeeId);

            // Calculate leave days
            int leaveDays = (leaveRequest.EndDate - leaveRequest.StartDate).Days;

            // Take leave and update salary details
            _employeeService.TakeLeave(employeeId, leaveDays);

            return Ok("Leave taken successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while taking leave: {ex.Message}");
        }
    }

}
