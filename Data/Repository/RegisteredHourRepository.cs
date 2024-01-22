

using Data.Enums;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository;

public class RegisteredHourRepository : IRegisteredHourRepository
{
    private readonly BumboContext _context;

    public RegisteredHourRepository(BumboContext context)
    {
        _context = context;
    }
    
    public RegisteredHour? GetRunningRegisteredHour(int employeeId)
    {
        return _context.RegisteredHours
            .Where(rh => rh.EmployeeId == employeeId && rh.End == null)
            .OrderByDescending(rh => rh.Start)
            .FirstOrDefault();
    }
    public bool Add(RegisteredHour norm)
    {
        _context.RegisteredHours.Add(norm);
        return Save();
    }
    public bool Update(RegisteredHour registeredHour)
    {
        _context.RegisteredHours.Update(registeredHour);
        return Save();
    }
    public IEnumerable<RegisteredHour> GetRegisteredHoursByDate(DateTime date)
    {
        return _context.RegisteredHours.Where(rh => rh.Start.Date == date.Date).OrderBy(rh => rh.Status)
            .Include(rh => rh.Employee).ToList();
    }
    
    public IEnumerable<RegisteredHour> GetRegisteredHoursByEmployeeId(int employeeId)
    {
        return _context.RegisteredHours
            .Where(rh => rh.EmployeeId == employeeId && rh.Status == RegisteredHourStatus.Approved)
            .OrderBy(rh => rh.Start)
            .ToList();
    }
    public IEnumerable<RegisteredHour> GetRegisteredHoursByDateRange(DateTime startDate, DateTime endDate)
    {
        return _context.RegisteredHours
            .Where(rh => rh.Start.Date >= startDate.Date && rh.Start.Date <= endDate.Date)
            .OrderBy(rh => rh.Status)
            .Include(rh => rh.Employee)
            .ToList();
    }

    public bool UpdateRange(IEnumerable<RegisteredHour> registeredHours)
    {
        _context.RegisteredHours.UpdateRange(registeredHours);
        return Save();
    }
    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0;
    }
}