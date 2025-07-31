using MVC_Example.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace MVC_Example.Controllers;

public class SiteController : Controller
{
    private readonly DatabaseContext databaseContext = new();

    public IActionResult Home()
    {
        return View();
    }

    [HttpGet]
    public IActionResult NewEmployee()
    {
        return View();

    }

    [HttpPost]
    public IActionResult NewEmployee(string employeeName, int employeeSalary)
    {
        databaseContext.Employees.Add(new Employee { Name = employeeName, Salary = employeeSalary });
        databaseContext.SaveChanges();
        return RedirectToAction("AllEmployees");

    }

    public async Task<ActionResult<Employee>> AllEmployees()
    {
        ViewData["Data"] = await databaseContext.Employees.ToListAsync();
        return View();
    }

    [HttpGet]
    public async Task<ActionResult<Employee>> EditEmployee(int employeeId)
    {
        ViewData["Data"] = await databaseContext.Employees.FindAsync(employeeId);
        return View();
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> EditEmployee(int employeeId, string employeeName, int employeeSalary)
    {
        Employee employee = await databaseContext.Employees.FindAsync(employeeId);
        employee.Name = employeeName;
        employee.Salary = employeeSalary;
        databaseContext.Employees.Update(employee);
        databaseContext.SaveChanges();
        return RedirectToAction("AllEmployees");
    }

    public async Task<ActionResult<Employee>> DeleteEmployee(int employeeId)
    {
        Employee employee = await databaseContext.Employees.FindAsync(employeeId);
        databaseContext.Employees.Remove(employee);
        databaseContext.SaveChanges();
        return RedirectToAction("AllEmployees");
    }
}
