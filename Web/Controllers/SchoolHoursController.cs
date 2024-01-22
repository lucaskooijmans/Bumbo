using Data.Enums;
using Data.Interfaces;
using Data.Models;
using Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    [Authorize]
    public class SchoolHoursController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ISchoolHoursRepository _schoolHoursRepository;
        public SchoolHoursController(IEmployeeRepository employeeRepository, ISchoolHoursRepository schoolHoursRepository)
        {
            _employeeRepository = employeeRepository;
            _schoolHoursRepository = schoolHoursRepository;
        }
        // GET: HomeController
        public IActionResult Index()
        {
            // Get the logged-in user
            var loggedInEmployee = _employeeRepository.GetEmployeeByEmail(User.Identity.Name); //EmployeeId
            var existingSchoolHours = _schoolHoursRepository.GetAllHoursOfEmployee(loggedInEmployee.Id);
            List<SchoolHours> SchoolHoursAllDays = new List<SchoolHours>();
            foreach (var day in Enum.GetValues(typeof(WeekDays)).Cast<WeekDays>().Where(d => d != WeekDays.Saturday && d != WeekDays.Sunday)) // loop all days except saturday and sunday
            {
                var schoolHour = new SchoolHours
                {
                    DayOfWeek = day,
                    EmployeeId = loggedInEmployee.Id
                };
                SchoolHoursAllDays.Add(schoolHour);
            }

            // Merge the existing and  school hours dummys based on the day of the week
            List<SchoolHours> mergedSchoolHours = existingSchoolHours
                .Concat(SchoolHoursAllDays)
                .GroupBy(sh => sh.DayOfWeek)
                .Select(group => group.First()).OrderBy(sh => sh.DayOfWeek)
                .ToList();

            var viewModel = new SchoolHoursViewModel
            {
                SchoolHours = mergedSchoolHours,
            };

            return View(viewModel);
        }

        // GET: HomeController/Edit/5
        public IActionResult EditCreate(int id)
        {
            var loggedInEmployee = _employeeRepository.GetEmployeeByEmail(User.Identity.Name); //EmployeeId

            var existingSchoolHours = _schoolHoursRepository.GetAllHoursOfEmployee(loggedInEmployee.Id);
            List<SchoolHours> SchoolHoursAllDays = new List<SchoolHours>();
            foreach (var day in Enum.GetValues(typeof(WeekDays)).Cast<WeekDays>().Where(d => d != WeekDays.Saturday && d != WeekDays.Sunday)) // loop all days except saturday and sunday
            {
                var schoolHour = new SchoolHours
                {
                    DayOfWeek = day,
                    EmployeeId = loggedInEmployee.Id
                };
                SchoolHoursAllDays.Add(schoolHour);
            }

            // Merge the existing and  school hours dummys based on the day of the week
            List<SchoolHours> mergedSchoolHours = existingSchoolHours
                .Concat(SchoolHoursAllDays)
                .GroupBy(sh => sh.DayOfWeek)
                .Select(group => group.First()).OrderBy(sh => sh.DayOfWeek)
                .ToList();

            var viewModel = new SchoolHoursViewModel
            {
                SchoolHours = mergedSchoolHours,
            };

            return View(viewModel);
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        public IActionResult EditCreate(SchoolHoursViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Filter out unnessecary SchoolHours
                var nonZeroHours = model.SchoolHours.Where(sh => sh.Hours != 0 || sh.Id != 0).ToList();

                // Separate existing and new SchoolHours
                var zeroHoursLeft = nonZeroHours.Where(sh => sh.Hours == 0).ToList(); // 0 hours means delete
                var existingHours = nonZeroHours.Where(sh => sh.Id != 0 && sh.Hours != 0).ToList();
                var newHours = nonZeroHours.Where(sh => sh.Id == 0).ToList();

                // Delete SchoolHours
                _schoolHoursRepository.DeleteSchoolHours(zeroHoursLeft);
                // Update existing SchoolHours
                _schoolHoursRepository.UpdateSchoolHours(existingHours);
                // Create new SchoolHours
                _schoolHoursRepository.CreateSchoolHours(newHours);

                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
