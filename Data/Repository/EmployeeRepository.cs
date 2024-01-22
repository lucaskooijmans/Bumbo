using Data.Enums;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly BumboContext _context;

    public EmployeeRepository(BumboContext context)
    {
        _context = context;
    }

    public IEnumerable<Employee> GetAll(string searchString)
    {
        var query = _context.Employees.Include(e => e.User)
            .Include(e => e.EmployeeDepartments)
            .Where(e => e.EmployeeDepartments.Any());

        if (!String.IsNullOrEmpty(searchString))
        {
            query = query.Where(e => e.FirstName.Contains(searchString) || e.LastName.Contains(searchString));
        }

        return query.ToList();
    }

    public IEnumerable<Employee> GetAllAvailableEmployees(DateTime start, DateTime end)
    {
        var availableEmployees = _context.Employees
            .Include(e => e.User)
            .Include(e => e.Branch)
            .Include(e => e.Shifts) // Load shifts for each employee
            .Include(e => e.Availabilities) // Load availabilities for each employee
            .Include(e => e.Absences) // Load absence requests for each employee
            .ToList();

        availableEmployees = availableEmployees
            .Where(e =>
                !e.Shifts.Any(s => s.Start.Date == start.Date) &&
                !e.Availabilities.Any(a =>
                    a.DayOfWeek == WeekDaysMethods.Convert(start.DayOfWeek) &&
                    !(a.EndTime <= start.TimeOfDay || a.StartTime >= end.TimeOfDay)) &&
                !e.Absences.Any(ar =>
                    ar.Status == AbsenceStatus.Approved &&
                    !(ar.End < start.Date || ar.Start > end.Date)))
            .ToList();

        return availableEmployees;
    }

    public Employee Get(int id)
    {
        return _context.Employees.Include(e => e.Branch)
            .Include(e => e.User)
            .Include(ed => ed.EmployeeDepartments)
            .First(m => m.Id == id);
    }

    public IEnumerable<Employee> GetAllWhere(List<int> employeeIds, DateTime shiftDate)
    {
        var currentDate = shiftDate;
        var startDate = currentDate.AddDays(-28);

        var query = _context.Employees
            .Include(e => e.Shifts)
            .Include(e => e.SchoolHours)
            .Where(e => employeeIds.Contains(e.Id))
            .Select(e => new Employee
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                DateOfBirth = e.DateOfBirth,
                Shifts = e.Shifts
                    .Where(shift => shift.Start >= startDate && shift.End <= currentDate)
                    .ToList(),
                SchoolHours = e.SchoolHours
            });

        return query.ToList();
    }

    public Employee GetByUserId(string userId)
    {
        return _context.Employees.First(m => m.UserId == userId);
    }

    public bool Add(Employee employee)
    {
        _context.Employees.Add(employee);
        return Save();
    }

    public bool Update(Employee employee)
    {
        _context.Employees.Update(employee);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0;
    }

    public Employee GetEmployeeByEmail(string name)
    {
        return _context.Employees.First(e => e.Email == name);
    }
}