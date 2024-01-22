using Data.Models;

namespace Data.Interfaces;
public interface IAbsenceRepository
{
    IEnumerable<Absence> GetAllAbsencesRequests();
    Absence GetAbsenceById(int id);
    void AddAbsence(Absence absence);
    void DeleteAbsence(int id);
    IEnumerable<Absence> GetAllAbsencesByEmployeeId(int id);
    int GetOpenAbsenceRequests();
    void Update(Absence absence);
}


