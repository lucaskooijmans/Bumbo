namespace Web.ViewModels;

public class WorkedHoursViewModel
{
    public int WeekNumber { get; set; }
    public int CurrentWeekNumber { get; set; }
    public DateTime Date { get; set; }   
    public List<RegisteredHourViewModel> RegisteredHours { get; set; }
    
    public List<EmployeeViewModel> Employees { get; set; }
    public int[] SelectedHoursIds { get; set; }
    
    public bool AllWorkedHoursAreApproved { get; set; }
    

}