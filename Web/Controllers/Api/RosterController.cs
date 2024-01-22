using System.Diagnostics;
using System.Net.Mime;
using System.Runtime.InteropServices.JavaScript;
using System.Text.RegularExpressions;
using BusinessLogic.Services.CaoService;
using BusinessLogic.Services.CaoService.Interfaces;
using Data.Enums;
using Data.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging.Console;
using Newtonsoft.Json;
using NuGet.Protocol;
using Utility.Extensions;

namespace Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class RosterController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IShiftRepository _shiftRepository;
    private readonly IShiftManager _shiftManager;

    public RosterController(IEmployeeRepository employeeRepository, IShiftRepository shiftRepository, IShiftManager shiftManager)
    {
        _employeeRepository = employeeRepository;
        _shiftRepository = shiftRepository;
        _shiftManager = shiftManager;
    }

    public string getColor(Departments department)
    {
        return department switch
        {
            Departments.Checkout => "ff0000",
            Departments.Dkw => "00ff00",
            Departments.Fresh => "0000ff",
            _ => "d1d1d1"
        };
    }

    [HttpGet("Events")]
    public ActionResult Get([FromQuery] string department)
    {
        var shifts = _shiftRepository.GetBranchShifts();

        var data = shifts.Select(s => new
        {
            id = s.Id.ToString(),
            resourceId = $"{s.EmployeeId}_{s.Department}",
            start = s.Start,
            end = s.End,
            title = s.Start.Time() + " - " + s.End.Time() + (s.CalculateBreak() != 0 ? " - Pauze: " + s.CalculateBreak() + " minuten" : ""),
            employee = s.Employee.FirstName + " " + s.Employee.LastName,
            shiftStart = s.Start.Time(),
            shiftEnd = s.End.Time(),
            department = s.DepartmentName.ToString(),
            status = s.Status.ToString(),
        }).Where(s => s.department == department);
        
        return Ok(data);
    }
    
    [HttpGet("GroupedEvents")]
    public ActionResult GroupedEvents([FromQuery] string department)
    {
        var shifts = _shiftRepository.GetBranchShifts();

        var separateShift = shifts.Select(s => new
        {
            id = s.Id.ToString(),
            resourceId = s.EmployeeId.ToString(),
            start = s.Start,
            end = s.End,
            title = s.Start.Time() + " - " + s.End.Time(),
            employee = s.Employee.FirstName + " " + s.Employee.LastName,
            shiftStart = s.Start.Time(),
            shiftEnd = s.End.Time(),
            department = s.DepartmentName.ToString(),
        }).Where(s => s.department == department);

        var groupedPerDepartment = separateShift.GroupBy(s => new { s.start, s.end }).Select(group => new 
        {
            resourceId = department,
            title = group.Key.start.Time() + " - " + group.Key.end.Time() + " - Totaal medewerkers: " + group.Count(),
            start = group.Key.start,
            end = group.Key.end,
        });


        return Ok(groupedPerDepartment);
    }

    [HttpGet("Resources")]
    public ActionResult Resource()
    {
        var departments = Enum.GetValues(typeof(Departments)).Cast<Departments>();

        var departmentsData = departments.Select(d => new
        {
            id = d.ToString(),
            group = "Afdelingen",
            title = d.GetDisplayName(),
        });

        var employees = _employeeRepository.GetAll();

        
        var employeesData = employees.SelectMany(e =>
        {
            // For each department of the employee, create a separate group
            var employeeGroups = e.EmployeeDepartments.Select(ed => new
            {
                id = $"{e.Id}_{ed.Department}",
                group = $"Medewerkers - {ed.DepartmentId.GetDisplayName()}",
                title = $"{e.FirstName} {e.LastName}"
            });

            return employeeGroups;
        });
        
        var data = departmentsData.Concat(employeesData);

        return Ok(data);
    }

    [HttpPost("Update")]
    public IActionResult Update([FromBody] UpdateData data)
    {
        Departments department = (Departments)Enum.Parse(typeof(Departments), data.Department);
        var shift = _shiftRepository.GetShift(data.ShiftId);
        shift.Start = shift.Start.Date.SetTime(data.StartHour, data.StartMinute);
        shift.End = shift.End.Date.SetTime(data.EndHour, data.EndMinute);
        shift.DepartmentName = department;

        _shiftRepository.UpdateShift(shift);
        return Ok();
    }

    [HttpPost("Delete")]
    public IActionResult Delete([FromBody] DeleteData data)
    {
        var shift = _shiftRepository.GetShift(data.ShiftId);
        _shiftRepository.DeleteShift(shift);
        return Ok();
    }

    [HttpPost("setSick")]
    public IActionResult SetSick([FromBody] DeleteData data)
    {
        Shift shift = _shiftRepository.GetShift(data.ShiftId);
        shift.Status = ShiftStatus.Sick;
        _shiftRepository.UpdateShift(shift);
        return Ok();
    }

    [HttpPost("getAvailableEmployees")]
    public IActionResult GetAvailableEmployees([FromBody] EmployeeData data)
    {
        DateTime startDateTime = new DateTime(data.CurrentYear, data.CurrentMonth, data.CurrentDate, data.StartHour, data.StartMinute, 0);
        DateTime endDateTime = new DateTime(data.CurrentYear, data.CurrentMonth, data.CurrentDate, data.EndHour, data.EndMinute, 0);

        IEnumerable<Employee> employees = _employeeRepository.GetAllAvailableEmployees(startDateTime, endDateTime);

        var employeesArray = employees.Select(e => new Employee()
        {
            Id = e.Id,
            FirstName = e.FirstName,
            LastName = e.LastName,
        });
        
        return Ok(employeesArray);
    }

    [HttpPost("Create")]
    public IActionResult Create([FromBody] CreateData data)
    {
        Departments department = (Departments)Enum.Parse(typeof(Departments), data.Department);
        
        var employees = _employeeRepository.GetAllWhere(data.Employees.Values.ToList(), new DateTime(data.CurrentYear, data.CurrentMonth, data.CurrentDate));
        
        var failedEmployees = new List<Employee>();
        
        foreach (var employee in employees)
        {
            var shift = new Shift
            {
                BranchId = 1,
                EmployeeId = employee.Id,
                DepartmentName = department,
                Start = new DateTime(data.CurrentYear, data.CurrentMonth, data.CurrentDate, data.StartHour, data.StartMinute, 0),
                End = new DateTime(data.CurrentYear, data.CurrentMonth, data.CurrentDate, data.EndHour, data.EndMinute, 0),
            };
            
            if(_shiftManager.CreateShift(shift, employee))
            {
                continue;
            }

            failedEmployees.Add(employee);
        }

        return Ok(failedEmployees);
    }
    
    [HttpPost("Publish")]
    public IActionResult Publish([FromBody] PublishData data)
    {
        DateTime date = new DateTime(data.CurrentYear, data.CurrentMonth, data.CurrentDate);
        var shifts = _shiftRepository.GetDateShifts(date);
        foreach (var shift in shifts)
        {
            shift.Status = ShiftStatus.Published;
            _shiftRepository.UpdateShift(shift);
        }
        
        return Ok();
    }
}

public class PublishData
{
    public int CurrentDate { get; set; }
    public int CurrentMonth { get; set; }
    public int CurrentYear { get; set; }
}

public class CreateData
{
    public int StartHour { get; set; }
    public int StartMinute { get; set; }
    public int EndHour { get; set; }
    public int EndMinute { get; set; }
    
    public int CurrentDate { get; set; }
    
    public int CurrentMonth { get; set; }
    
    public int CurrentYear { get; set; }
    public Dictionary<int, int>? Employees { get; set; } // TODO: Change to Employee class when ready to use
    public string? Department { get; set; }
}

public class EmployeeData
{
    public int StartHour { get; set; }
    public int StartMinute { get; set; }
    public int EndHour { get; set; }
    public int EndMinute { get; set; }
    public int CurrentDate { get; set; }
    public int CurrentMonth { get; set; }
    public int CurrentYear { get; set; }
}

public class UpdateData
{
    public int StartHour { get; set; }
    public int StartMinute { get; set; }
    public int EndHour { get; set; }
    public int EndMinute { get; set; }
    public int ShiftId { get; set; }
    public string? Department { get; set; }
}

public class DeleteData
{
    public int ShiftId { get; set; }
}