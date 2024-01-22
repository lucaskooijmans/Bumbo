using Data.Enums;
using Data.Models;

namespace Data.Interfaces;

public interface IShiftRepository
{
    public IEnumerable<Shift> GetShifts(int employeeId);
    public Shift GetShift(int? shiftId);
    public void UpdateShift(Shift shift);
    public void DeleteShift(Shift shift);
    public IEnumerable<Shift> GetBranchShifts(int branchId = 1);
    public IEnumerable<Shift> GetDateShifts(DateTime date, int branchId = 1);

    public Shift GetNextShift(int employeeId);
    public IEnumerable<Shift> GetReplaceableShifts(int id);
    void AcceptDenyShiftReplacement(ReplacementRequest replacementRequest);
    void UpdateShiftStatus(int id, ShiftStatus openForReplacement);
    public IEnumerable<ReplacementRequest> GetAnsweredReplaceableShifts();
    public IEnumerable<ReplacementRequest> GetAnsweredReplaceableShifts(int id);
    public void Add(Shift shift);
    void UpdateReplacementRequest(int shiftId, int employeeId, bool acceptDeny);
}