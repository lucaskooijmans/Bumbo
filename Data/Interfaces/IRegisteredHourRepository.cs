using Data.Models;

namespace Data.Interfaces;

public interface IRegisteredHourRepository
{
    RegisteredHour? GetRunningRegisteredHour(int employeeId);
    
    bool Add(RegisteredHour registeredHour);
    
    bool Update(RegisteredHour registeredHour);
    
    IEnumerable<RegisteredHour> GetRegisteredHoursByDate(DateTime date);
    
    IEnumerable<RegisteredHour> GetRegisteredHoursByEmployeeId(int employeeId);
    IEnumerable<RegisteredHour> GetRegisteredHoursByDateRange(DateTime startDate, DateTime endDate);
    bool UpdateRange(IEnumerable<RegisteredHour> registeredHours);
    bool Save();
}