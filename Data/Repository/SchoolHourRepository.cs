using Data.Interfaces;
using Data.Models;

namespace Data.Repository;
public class SchoolHourRepository : ISchoolHoursRepository
{
    private readonly BumboContext _context;

    public SchoolHourRepository(BumboContext context)
    {
        _context = context;
    }

    public List<SchoolHours> GetAllHoursOfEmployee(int employeeId)
    {
        return _context.SchoolHours
            .Where(sh => sh.EmployeeId == employeeId)
            .ToList();
    }

    public void UpdateSchoolHours(IEnumerable<SchoolHours> schoolHours)
    {
        _context.SchoolHours.UpdateRange(schoolHours);
        _context.SaveChanges();
    }

    public void CreateSchoolHours(IEnumerable<SchoolHours> newHours)
    {
        _context.SchoolHours.AddRange(newHours);
        _context.SaveChanges();
    }

    public void DeleteSchoolHours(List<SchoolHours> zeroHoursLeft)
    {
        _context.SchoolHours.RemoveRange(zeroHoursLeft);
        _context.SaveChanges();
    }

}

