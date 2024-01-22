using Data.Enums;
using Data.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Utility.Extensions;
using Web.ViewModels;

namespace Web.Controllers;

public class WorkedHoursController : Controller
{

    private readonly IRegisteredHourRepository _registeredHourRepository;
    private readonly IShiftRepository _shiftRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public WorkedHoursController(IRegisteredHourRepository registeredHourRepository, IShiftRepository shiftRepository,
        IEmployeeRepository employeeRepository)
    {
        _registeredHourRepository = registeredHourRepository;
        _shiftRepository = shiftRepository;
        _employeeRepository = employeeRepository;
    }

    public IActionResult Index(DateTime date, string? error, string? success)
    {
        var workedHours = _registeredHourRepository.GetRegisteredHoursByDate(date);
        var AllWorkedHoursInCurrentWeekAreApproved =
            _registeredHourRepository.GetRegisteredHoursByDateRange(date.StartOfWeek(), date.EndOfWeek());
        var shifts = _shiftRepository.GetDateShifts(date);
        if (error != null)
        {
            ModelState.AddModelError("", error);
        }
        if (success != null)
        {
            ModelState.AddModelError("", success);
        }

        WorkedHoursViewModel workedHoursViewModel = new WorkedHoursViewModel
        {
            WeekNumber = date.Week(),
            Date = date,
            RegisteredHours = workedHours.Select(rh => new RegisteredHourViewModel
            {
                Id = rh.Id,
                Employee = new EmployeeViewModel
                    { Id = rh.Employee.Id, FirstName = rh.Employee.FirstName, LastName = rh.Employee.LastName },
                Start = rh.Start,
                End = rh.End,
                Status = rh.Status,
                Shift = shifts.Where(shift => shift.EmployeeId == rh.EmployeeId).Select(shift => new ShiftViewModel
                {
                    Id = shift.Id,
                    Start = shift.Start,
                    End = shift.End,
                    Status = shift.Status,
                }).FirstOrDefault(),
            }).ToList(),
            Employees = _employeeRepository.GetAll().Select(e => new EmployeeViewModel
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
            }).ToList(),
            AllWorkedHoursAreApproved = AllWorkedHoursInCurrentWeekAreApproved.All(rh => rh.Status == RegisteredHourStatus.Approved),
        };


        return View(workedHoursViewModel);
    }


    [HttpPost]
    public IActionResult Approve(WorkedHoursViewModel model, string type)
    {
        var registeredHours = new List<RegisteredHour>();
        var ApprodvedById = _employeeRepository.GetEmployeeByEmail(User.Identity.Name).Id;
        if (type == "everything")
        {
            var workedHours = _registeredHourRepository.GetRegisteredHoursByDate(model.Date);

            foreach (var workedHour in workedHours)
            {
                if (workedHour.End != null)
                {
                    workedHour.Status = RegisteredHourStatus.Approved;
                    workedHour.ApprovedById = ApprodvedById;
                    registeredHours.Add(workedHour);
                }

            }

            _registeredHourRepository.UpdateRange(registeredHours);

            if (workedHours.Count() != registeredHours.Count())
            {
                string message = "Some hours could not be approved because they do not have an end time.";
                return RedirectToAction("Index", "WorkedHours", new { date = model.Date, error = message });
            }
        }
        else
        {
            if (!ValidateSelectedHoursHaveEnd(model.SelectedHoursIds))
            {
                string message = "Some hours could not be approved because they do not have an end time.";
                return RedirectToAction("Index", "WorkedHours", new { date = model.Date, error = message });
            }

            foreach (var id in model.SelectedHoursIds)
            {
                var start = Request.Form["Start_" + id];
                var end = Request.Form["End_" + id];
                var updated = new RegisteredHour
                {
                    Id = id,
                    EmployeeId = int.TryParse(Request.Form["Employee_id" + id], out var employeeId) ? employeeId : 0,
                    Start = model.Date + DateTime.Parse(start).TimeOfDay,
                    End = model.Date + DateTime.Parse(end).TimeOfDay,
                    Status = RegisteredHourStatus.Approved,
                    ApprovedById = ApprodvedById
                };

                registeredHours.Add(updated);
            }

            _registeredHourRepository.UpdateRange(registeredHours);
        }


        return RedirectToAction("Index", "WorkedHours", new { date = model.Date });
    }

    private bool ValidateSelectedHoursHaveEnd(int[] selectedHourIds)
    {
        // Check if all selected hours have an end time
        foreach (var id in selectedHourIds)
        {
            var end = Request.Form["End_" + id];
            if (string.IsNullOrEmpty(end))
            {
                return false;
            }
        }

        return true;
    }

    [HttpPost]
    public IActionResult Create(CreateRegisteredHourViewModel model)
    {

        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index", "WorkedHours", new { date = model.Date });
        }

        var registeredHour = new RegisteredHour
        {
            EmployeeId = model.EmployeeId,
            Start = model.Date.Date + model.Start,
            End = model.Date.Date + model.End,
            Status = RegisteredHourStatus.Approved,
        };

        _registeredHourRepository.Add(registeredHour);

        return RedirectToAction("Index", "WorkedHours", new { date = model.Date });
    }

    [HttpPost]
    public IActionResult Edit(RegisteredHourViewModel registeredHourViewModel)
    {
        var start = Request.Form["StartDate_" + registeredHourViewModel.Id];

        if (registeredHourViewModel.Start.TimeOfDay == null)
        {
            return RedirectToAction("Index", "WorkedHours", new { date = registeredHourViewModel.Start, error = "Starttijd kan niet leeg zijn"  });
        }
        
        registeredHourViewModel.Start = DateTime.Parse(start).Date + registeredHourViewModel.Start.TimeOfDay;
        
        if (registeredHourViewModel.End != null)
        {
            if(registeredHourViewModel.End.Value.TimeOfDay < registeredHourViewModel.Start.TimeOfDay)
            {
                registeredHourViewModel.End = DateTime.Parse(start).Date.AddDays(1) + registeredHourViewModel.End.Value.TimeOfDay;
            }
            else
            {
                registeredHourViewModel.End = DateTime.Parse(start).Date + registeredHourViewModel.End.Value.TimeOfDay;
            }
        }
        
        int? employeeId = registeredHourViewModel.Employee.Id;
        
        var registeredHour = new RegisteredHour
        { 
            Id = registeredHourViewModel.Id,
            EmployeeId = employeeId.Value,
            Start = registeredHourViewModel.Start,
            End = registeredHourViewModel.End,
            Status = registeredHourViewModel.Status,
        };
        
        _registeredHourRepository.Update(registeredHour);

        return RedirectToAction("Index", "WorkedHours", new { date = registeredHour.Start, success = "Tijdregistratie succesvol bijgewerkt"  });
    }
} 