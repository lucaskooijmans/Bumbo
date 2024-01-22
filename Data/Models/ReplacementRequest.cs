namespace Data.Models;
public class ReplacementRequest
{
    public int EmployeeId { get; set; }
    public int ShiftId { get; set; }
    public Employee? Employee { get; set; }
    public Shift? Shift { get; set; }
    public bool Status { get; set; }
}

