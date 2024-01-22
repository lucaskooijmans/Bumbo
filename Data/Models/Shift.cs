using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Enums;

namespace Data.Models;

public class Shift
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int BranchId { get; set; }
    public Branch Branch { get; set; }

    public Departments DepartmentName { get; set; }
    public Department Department { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }
    public ShiftStatus Status { get; set; }
    public int? EmployeeId { get; set; }

    public Employee? Employee { get; set; }
    public ICollection<ReplacementRequest>? ReplacementRequests { get; set; }
    public int Duration => (int) (End - Start).TotalHours;
    public int CalculateBreak()
    {
        int breakTime = 0;

        if (CalculateHours() >= 5.5 && CalculateHours() <= 10) breakTime = 30;
        if (CalculateHours() >= 10) breakTime = 45;
        
        return breakTime;
    }

    public float CalculateHours()
    {
        float totalHours = 0;
        TimeSpan difference = End - Start;
        
        totalHours = (float)(difference.TotalMinutes / 60);
        
        return totalHours;
    }
}