using Data.Enums;
using Data.Models;

namespace Web.ViewModels;

public class ShiftViewModel
{
    public IEnumerable<Shift> Shifts { get; set; }
    public int Id { get; set; }
    public bool? ReplacementRequestAccepted { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public ShiftStatus? Status { get; set; }
    public Departments Department { get; set; }

    public Employee? Employee { get; set; }
    public Employee? ReplacingEmployee { get; internal set; }
    
    public float CalculateHours()
    {
        float totalHours = 0;
        TimeSpan difference = End - Start;
        
        totalHours = (float)(difference.TotalMinutes / 60);
        
        return totalHours;
    }
    public int CalculateBreak()
    {
        int breakTime = 0;

        if (CalculateHours() >= 5.5 && CalculateHours() <= 10) breakTime = 30;
        if (CalculateHours() >= 10) breakTime = 45;
        
        return breakTime;
    }
}