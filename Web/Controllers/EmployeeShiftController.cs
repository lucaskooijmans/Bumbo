using Data.Enums;
using Data.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.ViewModels;

namespace Web.Controllers;

public class EmployeeShiftController : Controller
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IShiftRepository _shiftRepository;

    public EmployeeShiftController(IEmployeeRepository employeeRepository, IShiftRepository shiftRepository)
    {
        _employeeRepository = employeeRepository;
        _shiftRepository = shiftRepository;
    }

    // GET
    public IActionResult Index()
    {
        var employee = _employeeRepository.GetByUserId(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        var shifts = _shiftRepository.GetShifts(employee.Id);

        var shiftsViewModel = shifts.Select(shift => new ShiftViewModel()
        {
            Id = shift.Id,
            Status = shift.Status,
            Start = shift.Start,
            End = shift.End,
            Department = shift.DepartmentName
        });

        return View(shiftsViewModel);
    }

    public IActionResult Evaluate()
    {
        var replacementRequests = _shiftRepository.GetAnsweredReplaceableShifts();

        var shiftsViewModel = replacementRequests.Select(replacementRequest => new ShiftViewModel
        {
            Id = replacementRequest.Shift.Id,
            Status = replacementRequest.Shift.Status,
            Start = replacementRequest.Shift.Start,
            End = replacementRequest.Shift.End,
            Employee = replacementRequest.Shift.Employee,
            ReplacingEmployee = replacementRequest.Employee
        });
        return View(shiftsViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Evaluate(ShiftViewModel model, int employeeId, bool status)
    {
        if (!status)
        {
            _shiftRepository.UpdateReplacementRequest(model.Id, employeeId, false);
        }
        else
        {
            _shiftRepository.Add(new Shift()
            {
                EmployeeId = employeeId,
                BranchId = 1,
                Start = model.Start,
                End = model.End,
                Status = ShiftStatus.Published
            });
            _shiftRepository.UpdateShiftStatus(model.Id, ShiftStatus.Replaced); 
        }
        return RedirectToAction("Evaluate", "EmployeeShift");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AcceptDenyChangeRequest(int id, bool AcceptDeny)
    {
        var model = new ReplacementRequest()
        {
            ShiftId = id,
            EmployeeId = _employeeRepository.GetEmployeeByEmail(User.Identity.Name).Id,
            Status = AcceptDeny
        };
        _shiftRepository.AcceptDenyShiftReplacement(model);
        return RedirectToAction("Index", "EmployeeDashboard");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateReplacementRequest(int id)
    {
        _shiftRepository.UpdateShiftStatus(id, ShiftStatus.OpenForReplacement);
        return RedirectToAction("Index", "EmployeeShift");
    }
}