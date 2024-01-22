namespace Web.ViewModels;

public class EmployeeDashboardViewModel
{
    public ShiftViewModel? nextShift { get; set; }
    public string EmployeeName { get; set; }
    
    public IEnumerable<ShiftViewModel> ChangeRequests { get; set; }
}