using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Models;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Web.ViewModels;
using Data.Enums;
using Web.Controllers.Api;

namespace Web
{
    public class ShiftController : Controller
    {
        private readonly IShiftRepository _shiftRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public ShiftController(IShiftRepository shiftRepository, IEmployeeRepository employeeRepository)
        {
            _shiftRepository = shiftRepository;
            _employeeRepository = employeeRepository;
        }

        // GET: Shift
        public IActionResult Index()
        {
            var shifts = _shiftRepository.GetBranchShifts();
            ShiftViewModel shiftViewModel = new ShiftViewModel
            {
                Shifts = shifts
            };
            return View(shiftViewModel);
        }

        // GET: Shift/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            
            Shift shift = _shiftRepository.GetShift(id);
            
            if (shift == null) return NotFound();
            
            return View(shift);
        }

        // GET: Shift/Create
        public IActionResult Create()
        {
            var employees = _employeeRepository.GetAll();
            
            ShiftEditCreateViewModel shiftEditCreateViewModel = new ShiftEditCreateViewModel
            {
                Shift = new Shift(),
                Employees = employees.Select(employee => new EmployeeViewModel
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    PhoneNumber = employee.PhoneNumber, 
                    Email = employee.Email,
                    DateOfBirth = employee.DateOfBirth,
                    BSN = employee.BSN,
                    PostalCode = employee.PostalCode,
                    City = employee.City,
                    HouseNumber = employee.HouseNumber,
                    Street = employee.Street,
                })
            };
            
            return View(shiftEditCreateViewModel);
        }

        // POST: Shift/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ShiftEditCreateViewModel shiftEditCreateViewModel)
        {
            
                foreach (var employee in shiftEditCreateViewModel.Employees)
                {
                    Shift shift = new Shift
                    {
                        EmployeeId = employee.Id,
                        Start = shiftEditCreateViewModel.Shift.Start,
                        End = shiftEditCreateViewModel.Shift.End,
                        BranchId = 1
                    };
                    _shiftRepository.Add(shift);
                }
                return RedirectToAction(nameof(Index));
            return View(shiftEditCreateViewModel);
        }

        // GET: Shift/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            Shift shift = _shiftRepository.GetShift(id);
            
            if (shift == null) return NotFound();
                
            return View(shift);
        }

        // POST: Shift/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,BranchId,Start,End")] Shift shift)
        {
            _shiftRepository.UpdateShift(shift);
            return View(shift);
        }

        // GET: Shift/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            
            Shift shift = _shiftRepository.GetShift(id);

            return View(shift);
        }

        // POST: Shift/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Shift shift)
        {

            _shiftRepository.DeleteShift(shift);
            return RedirectToAction(nameof(Index));
        }
    }
}
