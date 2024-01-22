using Data.Enums;

namespace Web.ViewModels;

public class RegisteredHourViewModel
{
    
    public int Id { get; set; }
    public EmployeeViewModel Employee { get; set; }    
    
    public DateTime Start { get; set; }
    
    public DateTime? End { get; set; }
    public RegisteredHourStatus Status { get; set; }
    
    public EmployeeViewModel? ApprovedBy{ get; set; }
    
    public ShiftViewModel? Shift { get; set; }
    
    public double TimeDifferencePercentage
    {
        get
        {
            if (Shift == null)
                return 100.0;

            TimeSpan shiftDuration = Shift.End - Shift.Start;

            if (End == null)
                return 50.0;

            TimeSpan registeredHourDuration = End.Value - Start;

            double absoluteDifference = Math.Abs(registeredHourDuration.TotalSeconds - shiftDuration.TotalSeconds);
            double percentageDifference = (absoluteDifference / shiftDuration.TotalSeconds) * 100.0;

            return percentageDifference;
        }
    }


    public string BackgroundColor
    {
        get
        {
            double? timeDifferencePercentage = TimeDifferencePercentage;
            
            var backgroundColor = "bg-success";
            
            if (timeDifferencePercentage >= 20.0) backgroundColor = "bg-warning";
            if (timeDifferencePercentage >= 70.0) backgroundColor = "bg-error";
            return backgroundColor;
        }
    }
}