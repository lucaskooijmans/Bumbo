using BusinessLogic.Services.HoursCalculationService.Factories;
using BusinessLogic.Services.HoursCalculationService.Interfaces;
using Data.Models;

namespace BusinessLogic.Services.HoursCalculationService;

public class HoursCalculationManager
{
    private readonly HoursPolicyFactory _hoursPolicyFactory ;
    
    public HoursCalculationManager(HoursPolicyFactory hoursCalculationManager)
    {
        _hoursPolicyFactory = hoursCalculationManager;
    }
    
   

    
    // This method is called from the controller that will create the export. From the controller, the method will receive a list of shifts.
    // NOTE: void needs to be changed to a type that will be returned to the controller.
    public Dictionary<int, decimal> CalculateHours(List<Shift> allShifts)
    {
        List<Shift> shifts = new List<Shift>();
        Dictionary<int, decimal> hourBonuses = new Dictionary<int, decimal>();

        foreach (Shift shift in allShifts)
        {
            shifts.AddRange(SplitShifts(shift));
        }
        
        foreach (Shift shift in shifts)
        {
            IHourPolicy hourPolicy = _hoursPolicyFactory.GetHourPolicy(shift);
           var a = hourPolicy.CalculateHours(shift);
            foreach (var item in a)
            {
                if (hourBonuses.ContainsKey(item.Key))
                {
                    hourBonuses[item.Key] += (decimal)item.Value;
                }
                else
                {
                    hourBonuses.Add(item.Key, (decimal)item.Value);
                }
            }
        }
        return hourBonuses;
    }
    public List<Shift> SplitShifts(Shift shift)
    {
        List<Shift> splittedShifts = new List<Shift>();

        if (shift.Start.Date < shift.End.Date)
        {
            splittedShifts.Add(new Shift
            {
                Start = shift.Start,
                End = new DateTime(shift.Start.Year, shift.Start.Month, shift.Start.Day, 23, 59, 59),
                Employee = shift.Employee,
                EmployeeId = shift.EmployeeId,
            });

            splittedShifts.Add(new Shift
            {
                Start = new DateTime(shift.End.Year, shift.End.Month, shift.End.Day, 0, 0, 0),
                End = shift.End,
                Employee = shift.Employee,
                EmployeeId = shift.EmployeeId,
            });
        }
        else
        {
            splittedShifts.Add(shift);
        }
        
        return splittedShifts;
    }
}