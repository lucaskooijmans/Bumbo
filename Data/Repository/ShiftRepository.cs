using Data.Enums;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository;

public class ShiftRepository : IShiftRepository
{
    private readonly BumboContext _context;

    public ShiftRepository(BumboContext context)
    {
        _context = context;
    }

    public IEnumerable<Shift> GetShifts(int employeeId)
    {
        return _context.Shifts
            .Where(s => s.Start >= DateTime.Today && s.EmployeeId == employeeId &&
                        (s.Status == ShiftStatus.Published || s.Status == ShiftStatus.OpenForReplacement))
            .OrderBy(s => s.Start)
            .ToList();
    }

    public Shift GetShift(int? shiftId)
    {
        return _context.Shifts.FirstOrDefault(s => s.Id == shiftId)!;
    }

    public void UpdateShift(Shift shift)
    {
        _context.Update(shift);
        _context.SaveChanges();
    }

    public void DeleteShift(Shift shift)
    {
        _context.Remove(shift);
        _context.SaveChanges();
    }

    public void AddShift(Shift shift)
    {
        _context.Add(shift);
        _context.SaveChanges();
    }

    public IEnumerable<Shift> GetBranchShifts(int branchId = 1)
    {
        return _context.Shifts.Where(s => s.Start >= DateTime.Today && s.BranchId == branchId).Include(s => s.Employee)
            .ToList();
    }
    
    public IEnumerable<Shift> GetDateShifts(DateTime date, int branchId = 1)
    {
        return _context.Shifts.Where(s => s.Start.Date == date.Date && s.BranchId == branchId).Include(s => s.Employee)
            .ToList();
    }


    public Shift GetNextShift(int employeeId)
    {
        return _context.Shifts
            .Where(s => s.Start >= DateTime.Today && s.EmployeeId == employeeId &&
                        (s.Status == ShiftStatus.Published || s.Status == ShiftStatus.OpenForReplacement))
            .OrderBy(s => s.Start).FirstOrDefault();
    }

    public IEnumerable<Shift> GetReplaceableShifts(int employeeId)
    {
        return _context.Shifts
            .Where(s => s.Status == ShiftStatus.OpenForReplacement && s.EmployeeId != employeeId &&
                        s.Start >= DateTime.Today)
            .Where(s => !s.ReplacementRequests
                .Any(rr => rr.EmployeeId == employeeId && rr.Status == false))
            .ToList();
    }

    public void UpdateShiftStatus(int shiftId, ShiftStatus status)
    {
        _context.Shifts.First(s => s.Id == shiftId).Status = status;
        _context.SaveChanges();
    }

    public void AcceptDenyShiftReplacement(ReplacementRequest replacementRequest)
    {
        _context.ReplacementRequests.Add(replacementRequest);
        _context.SaveChanges();
    }

    public IEnumerable<ReplacementRequest> GetAnsweredReplaceableShifts(int id)
    {
        return _context.ReplacementRequests.Where(rr => rr.EmployeeId == id).ToList();
    }

    public void Add(Shift shift)
    {
        _context.Shifts.Add(shift);
        _context.SaveChanges();
    }

    public void UpdateReplacementRequest(int shiftId, int employeeId, bool acceptDeny)
    {
        _context.ReplacementRequests.First(rr => rr.ShiftId == shiftId && rr.EmployeeId == employeeId).Status =
            acceptDeny;
        _context.SaveChanges();
    }

    public IEnumerable<ReplacementRequest> GetAnsweredReplaceableShifts()
    {
        return _context.ReplacementRequests
            .Where(rr => rr.Shift.Status == ShiftStatus.OpenForReplacement)
            .Include(rr => rr.Shift)
            .Include(rr => rr.Employee)
            .Include(rr => rr.Shift.Employee) // Include the Employee for each Shift
            .ToList();
    }
}