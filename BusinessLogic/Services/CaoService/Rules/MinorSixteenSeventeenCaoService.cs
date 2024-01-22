using BusinessLogic.Services.CaoService.Interfaces;
using Data.Models;

namespace BusinessLogic.Services.CaoService.Rules;

public class MinorSixteenSeventeenCaoService : ICaoService
{
    public bool ValidateShift(Shift shift, Employee employee)
    {
        // General CAO rules still apply
        if (!new GeneralCaoService().ValidateShift(shift, employee))
        {
            return false;
        }

        // Rule: Max 9 hours per day including school.
        if (shift.Duration > 9)
        {
            return false;
        }

        // Rule: Not more than 40 hours averaged over 4 weeks.
        if (AvarageHoursOverFourWeeks(employee) + shift.Duration > 40)
        {
            return false;
        }
        
        return true;
    }

    public double? AvarageHoursOverFourWeeks(Employee employee)
    {
        if (employee.Shifts == null)
        {
            return 0.0;
        } 
        
       return employee.Shifts.Average(s => s.Duration);
    }
    
}