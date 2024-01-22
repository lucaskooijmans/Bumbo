using Data.Enums;
using Data.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers;

public class AbsenceController : Controller
{
    private readonly IAbsenceRepository _absenceRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public AbsenceController(IAbsenceRepository absenceRepository, IEmployeeRepository employeeRepository)
    {
        _absenceRepository = absenceRepository;
        _employeeRepository = employeeRepository;
    }

    // GET: Absence/Index
    public IActionResult Index()
    {
        Employee employee = _employeeRepository.GetEmployeeByEmail(User.Identity.Name);

        if (employee == null)
        {
            return NotFound();
        }

        var absences = _absenceRepository.GetAllAbsencesByEmployeeId(employee.Id)
            .Select(a => new AbsenceViewModel
            {
                Id = a.Id,
                EmployeeId = a.EmployeeId,
                StartDate = a.Start.Date,
                EndDate = a.End.Date,
                StartTime = a.Start,
                EndTime = a.End,
                Description = a.Description,
                Status = a.Status
            });

        var model = new EmployeeOverviewViewModel
        {
            Absences = absences
        };

        return View(model);
    }


    // GET: Absence/Create
    public IActionResult Create()
    {
        var model = new AbsenceViewModel()
        {
            Status = AbsenceStatus.Pending,
        };
        return View(model);
    }

    // POST: Absence/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(AbsenceViewModel model)
    {
        Employee employee = _employeeRepository.GetEmployeeByEmail(User.Identity.Name);
        
        if (ModelState.IsValid)
        {
            var newAbsence = new Absence
            {
                EmployeeId = employee.Id,
                Start = model.StartDate + model.StartTime?.TimeOfDay ?? (DateTime)model.StartDate,
                End = (model.EndDate + model.EndTime?.TimeOfDay) ?? (DateTime)model.EndDate,
                Description = model.Description,
                Status = (AbsenceStatus) model.Status
            };

            _absenceRepository.AddAbsence(newAbsence);

            return RedirectToAction("Index"); // Redirect to the index page or any other appropriate page
        }

        // If invalid
        return View("Create", model);
    }

    public IActionResult Delete(int id)
    {
        Absence absence = _absenceRepository.GetAbsenceById(id);

        if (absence == null)
        {
            return NotFound();
        }

        // Perform the deletion
        _absenceRepository.DeleteAbsence(id);
        return RedirectToAction("Index");
    }

    public IActionResult Evaluate()
    {
        var absences = _absenceRepository.GetAllAbsencesRequests();

        var absenceslist = absences.Select(a => new AbsenceViewModel
        {
            Id = a.Id,
            EmployeeId = a.EmployeeId,
            Employee = new EmployeeViewModel
            {
                FirstName = a.Employee.FirstName,
                LastName = a.Employee.LastName,
            },
            StartDate = a.Start.Date,
            EndDate = a.End.Date,
            StartTime = a.Start,
            EndTime = a.End,
            Description = a.Description,
            Status = a.Status,
        });

        return View(absenceslist);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Evaluate(int id, AbsenceStatus status)
    {
        Absence absence = _absenceRepository.GetAbsenceById(id);

        if (absence == null)
        {
            return NotFound();
        }

        absence.Status = status;
        _absenceRepository.Update(absence);

        return RedirectToAction("Evaluate");
    }

}