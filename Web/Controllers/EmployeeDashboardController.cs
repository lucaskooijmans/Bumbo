using System.Security.Claims;
using Data.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers;


[Authorize]
public class EmployeeDashboardController : Controller
{
    private readonly IShiftRepository _shiftRepository;
    private readonly IEmployeeRepository _employeeRepository;
    public EmployeeDashboardController(IShiftRepository shiftRepository, IEmployeeRepository employeeRepository)
    {
        _shiftRepository = shiftRepository;
        _employeeRepository = employeeRepository;
    }

    public IActionResult Index()
    {
        var employee = _employeeRepository.GetByUserId(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        var employeeShifts = _shiftRepository.GetShifts(employee.Id);
        var ReplacementRequests = _shiftRepository.GetReplaceableShifts(employee.Id)
            .Where(rr => !employeeShifts.Any(es => es.Start.Date == rr.Start.Date))
            .ToList();

        var answeredChangeRequests = _shiftRepository.GetAnsweredReplaceableShifts(employee.Id);
        var shift = _shiftRepository.GetNextShift(employee.Id);

        EmployeeDashboardViewModel employeeDashboardViewModel;

        if (shift != null)
        {
            employeeDashboardViewModel = new EmployeeDashboardViewModel
            {
                nextShift = new ShiftViewModel
                {
                    Start = shift.Start,
                    End = shift.End,
                    Department = shift.DepartmentName
                },
                EmployeeName = employee.FirstName + " " + employee.LastName,
                ChangeRequests = ReplacementRequests.Select(c => new ShiftViewModel
                {
                    Id = c.Id,
                    ReplacementRequestAccepted = answeredChangeRequests.Any(a => a.ShiftId == c.Id),
                    Start = c.Start,
                    End = c.End,
                })
            };
        }
        else
        {
            employeeDashboardViewModel = new EmployeeDashboardViewModel
            {
                EmployeeName = employee.FirstName + " " + employee.LastName,
                ChangeRequests = ReplacementRequests.Select(c => new ShiftViewModel
                {
                    Id = c.Id,
                    ReplacementRequestAccepted = answeredChangeRequests.Any(a => a.ShiftId == c.Id),
                    Start = c.Start,
                    End = c.End,
                })
            };
        }


        return View(employeeDashboardViewModel);
    }

    public IActionResult Settings()
    {
        return View();
    }
}