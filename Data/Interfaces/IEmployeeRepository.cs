using Data.Enums;
using Data.Models;

namespace Data.Interfaces;

public interface IEmployeeRepository
{
    IEnumerable<Employee> GetAll(string searchString = null);
    Employee GetEmployeeByEmail(string name);

    IEnumerable<Employee> GetAllAvailableEmployees(DateTime start, DateTime end);

    Employee Get(int id);

    IEnumerable<Employee> GetAllWhere(List<int> employeeIds, DateTime shiftDate);

    Employee GetByUserId(string userId);
    bool Add(Employee norm);

    bool Update(Employee norm);

    bool Save();
}