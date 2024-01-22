using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Data.Repository;
using Data.Interfaces;
using Data.Models;
using Web.ViewModels;
using Data.Enums;
using System;
using Utility.Extensions;

namespace Web.Controllers;

[Authorize]
public class AvailabilityController : Controller
{
    private readonly IAvailabilityRepository _availabilityRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public AvailabilityController(IAvailabilityRepository availabilityRepository,
        IEmployeeRepository employeeRepository)
    {
        _availabilityRepository = availabilityRepository;
        _employeeRepository = employeeRepository;
    }

    public IActionResult Index()
    {
        var loggedInEmployee = _employeeRepository.GetEmployeeByEmail(User.Identity.Name);
        
        // Check if the logged-in user is an employee
        // Get availabilities only for the logged-in employee
        var availabilities = _availabilityRepository.GetAvailabilitiesOfEmployee(loggedInEmployee.Id);
        
        var viewModel = new AvailabilitiesViewModel
        {
            Availabilities = availabilities.Select(a => new AvailabilityViewModel
            {
                Id = a.Id,
                EmployeeId = a.EmployeeId,
                DayOfWeek = a.DayOfWeek,
                StartTime = a.StartTime,
                EndTime = a.EndTime
            }).ToList()
        };

        return View(viewModel);
    }


    [HttpGet]
    public IActionResult EditCreate()
    {
        var loggedInEmployee = _employeeRepository.GetEmployeeByEmail(User.Identity.Name); //EmployeeId
        var excistingAvailabilities = _availabilityRepository.GetAvailabilitiesOfEmployee(loggedInEmployee.Id);
        List<Availability> availabilities = new List<Availability>();
        foreach (var day in Enum.GetValues(typeof(WeekDays)).Cast<WeekDays>())
        {
            var schoolHour = new Availability
            {
                DayOfWeek = day,
                EmployeeId = loggedInEmployee.Id
            };
            availabilities.Add(schoolHour);
        }

        // Merge the existing and  school hours dummys based on the day of the week
        List<Availability> mergedAvailabilities = excistingAvailabilities
            .Concat(availabilities)
            .GroupBy(sh => sh.DayOfWeek)
            .Select(group => group.First()).OrderBy(sh => sh.DayOfWeek)
            .ToList();

        var availabilitiesViewModels = mergedAvailabilities.Select(a => new AvailabilityViewModel
        {
            Id = a.Id,
            EmployeeId = a.EmployeeId,
            DayOfWeek = a.DayOfWeek,
            StartTime = a.StartTime,
            EndTime = a.EndTime
        }).ToList();


        var viewModel = new AvailabilitiesViewModel()
        {
            Availabilities = availabilitiesViewModels,
        };

        return View(viewModel);
    }


    // POST: HomeController/Edit/5
    [HttpPost]
    public IActionResult EditCreate(AvailabilitiesViewModel model)
    {
        if (ModelState.IsValid)
        {
            var nonZeroHours = model.Availabilities.Where(av => av.Id != 0).ToList();

            var zeroHoursLeft = nonZeroHours
                .Where(av => av.StartTime.Value.IsMidnight() || av.EndTime.Value.IsMidnight())
                .Select(av => new Availability
                {
                    Id = av.Id,
                    EmployeeId = av.EmployeeId,
                    DayOfWeek = av.DayOfWeek,
                    StartTime = (TimeSpan)av.StartTime,
                    EndTime = (TimeSpan)av.EndTime,
                }).ToList();

            // 0 hours means delete
            var existingHours = nonZeroHours
                .Where(av => !av.StartTime.Value.IsMidnight() && !av.EndTime.Value.IsMidnight())
                .Select(av => new Availability
                {
                    Id = av.Id,
                    EmployeeId = av.EmployeeId,
                    DayOfWeek = av.DayOfWeek,
                    StartTime = (TimeSpan)av.StartTime,
                    EndTime = (TimeSpan)av.EndTime,
                }).ToList();

            var newHours = model.Availabilities
                .Where(sh =>
                    sh.Id == 0 && sh.StartTime.HasValue && sh.EndTime.HasValue && !sh.StartTime.Value.IsMidnight() &&
                    !sh.EndTime.Value.IsMidnight())
                .Select(av => new Availability
                {
                    EmployeeId = av.EmployeeId,
                    DayOfWeek = av.DayOfWeek,
                    StartTime = (TimeSpan)av.StartTime,
                    EndTime = (TimeSpan)av.EndTime,
                }).ToList();

            // Delete SchoolHours
            _availabilityRepository.DeleteAvailabilities(zeroHoursLeft);
            // Update existing SchoolHours
            _availabilityRepository.UpdateAvailabilities(existingHours);
            // Create new SchoolHours
            _availabilityRepository.CreateAvailabilities(newHours);

            return RedirectToAction("Index");
        }

        return View(model);
    }
}