using BusinessLogic.Services.HoursCalculationService;
using BusinessLogic.Services.HoursCalculationService.Factories;
using BusinessLogic.Services.HoursCalculationService.Interfaces;
using Data.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Utility.Extensions;

namespace Web.Controllers
{
    public class ExcelExportController : Controller
    {
        private readonly HoursCalculationManager hoursCalculationManager;
        private readonly IRegisteredHourRepository registeredHourRepository;
        public ExcelExportController(IRegisteredHourRepository registeredHourRepository)
        {
            this.registeredHourRepository = registeredHourRepository;
            this.hoursCalculationManager = new HoursCalculationManager(new HoursPolicyFactory());
        }
        public MemoryStream GetEmployeesHoursStream(IEnumerable<RegisteredHour> employeesHours, DateTime date)
        {
            var groupedHoursPerEmployee = employeesHours
                .GroupBy(rh => rh.EmployeeId)
                .Select(employeeGroup => new
                {
                    EmployeeId = employeeGroup.Key,
                    Employee = employeeGroup.First().Employee,
                    Charges = CalculateEmployeeHours(employeeGroup.ToList()) // Get hours for each employee
                })
                .OrderBy(entry => entry.EmployeeId)
                .ThenBy(entry => entry.Charges.Keys.First()) // Assuming EmployeeHours returns a dictionary with EmployeeId
                .ToList();

            var stream = new MemoryStream();

            using (var writeFile = new StreamWriter(stream, leaveOpen: true))
            {
                writeFile.WriteLine($"Jaar: {date.Year}; Week: {date.Week()}");
                writeFile.WriteLine($"EmployeeID;Naam;Uren;Toeslag") ;

                foreach (var employeeData in groupedHoursPerEmployee)
                {
                    var employeeName = $"{employeeData.Employee.FirstName} {employeeData.Employee.LastName}";
                    writeFile.WriteLine($"{employeeData.EmployeeId};{employeeName}");

                    foreach (var employeeHours in employeeData.Charges)
                    {
                        var charge = employeeHours.Key;
                        var totalWorkedHours = employeeHours.Value;

                        writeFile.WriteLine($";;{totalWorkedHours};{charge};");
                    }
                    writeFile.WriteLine();
                }
            }
            stream.Position = 0;
            return stream;
        }
        public Dictionary<int, decimal> CalculateEmployeeHours(List<RegisteredHour> hours)
        {
            List<Shift> shifts = new List<Shift>();

            foreach (RegisteredHour registeredHour in hours)
            {
                Shift shift = new Shift
                {
                    // Assuming EmployeeId is used for the corresponding property in Shift
                    EmployeeId = registeredHour.EmployeeId,
                    Start = registeredHour.Start,
                    End = registeredHour.End ?? registeredHour.Start, // Handle nullable End DateTime
                };
                shifts.Add(shift);
            }
            return hoursCalculationManager.CalculateHours(shifts);
        }

        public ActionResult DownloadExcel(DateTime date)
        {

            var registeredHours = registeredHourRepository.GetRegisteredHoursByDateRange(date.StartOfWeek(), date.EndOfWeek());

            var stream = GetEmployeesHoursStream(registeredHours, date);

            string fileName = $"Verloning-{date.Year}-Week{date.Week()}.csv";

            return File(stream, "application/octet-stream", fileName);
        }
    }
}
