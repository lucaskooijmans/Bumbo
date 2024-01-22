using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Data.Interfaces;
using Web.ViewModels;
using Data.Repository;

namespace Web.Controllers;

[Authorize]
public class RegisteredHoursController : Controller
{
    private readonly IRegisteredHourRepository _registeredHourRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public RegisteredHoursController(IRegisteredHourRepository registeredHourRepository, IEmployeeRepository employeeRepository)
    {
        _registeredHourRepository = registeredHourRepository;
        _employeeRepository = employeeRepository;
    }

    public IActionResult Index()
    {
        var loggedInEmployee = _employeeRepository.GetEmployeeByEmail(User.Identity.Name);

        var registeredHours = _registeredHourRepository.GetRegisteredHoursByEmployeeId(loggedInEmployee.Id);

        var viewModel = new RegisteredHoursViewModel
        {
            RegisteredHours = registeredHours.Select(rh => new RegisteredHourViewModel
            {
                Id = rh.Id,
                Employee = new EmployeeViewModel {Id = rh.Employee.Id, FirstName = rh.Employee.FirstName, LastName = rh.Employee.LastName},
                Start = rh.Start,
                End = rh.End,
                Status = rh.Status,
            }).ToList(),
        };

        return View(viewModel);
    }
}
