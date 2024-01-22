using Data.Enums;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository;

public class AbsenceRepository : IAbsenceRepository
{
    private readonly BumboContext _context;
    public AbsenceRepository(BumboContext context)
    {
        _context = context;
    }

    public IEnumerable<Absence> GetAllAbsencesRequests()
    {
        return _context.Absences.Where(ab => ab.Status == AbsenceStatus.Pending).Include(ab => ab.Employee).ToList();
    }

    public Absence GetAbsenceById(int id)
    {
        return _context.Absences.FirstOrDefault(a => a.Id == id);
    }

    public void AddAbsence(Absence absence)
    {
        _context.Absences.Add(absence);
        _context.SaveChanges();
    }

    public void DeleteAbsence(int id)
    {
        var absence = _context.Absences.FirstOrDefault(a => a.Id == id);

        if (absence != null)
        {
            _context.Absences.Remove(absence);
            _context.SaveChanges();
        }
    }

    public IEnumerable<Absence> GetAllAbsencesByEmployeeId(int EmployeeId)
    {
        return _context.Absences.Where(a => a.EmployeeId == EmployeeId).ToList();
    }

    public int GetOpenAbsenceRequests()
    {
        return _context.Absences.Count(a => a.Status == AbsenceStatus.Pending);
    }

    public void Update(Absence absence)
    {
        _context.Absences.Update(absence);
        _context.SaveChanges();
    }
}


