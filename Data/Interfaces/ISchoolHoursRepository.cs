using Data.Models;

namespace Data.Interfaces;
public interface ISchoolHoursRepository
{
    List<SchoolHours> GetAllHoursOfEmployee(int employeeId);
    void UpdateSchoolHours(IEnumerable<SchoolHours> schoolHours);
    void CreateSchoolHours(IEnumerable<SchoolHours> newHours);
    void DeleteSchoolHours(List<SchoolHours> zeroHoursLeft);
}

