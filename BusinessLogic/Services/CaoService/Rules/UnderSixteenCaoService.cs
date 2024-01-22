using BusinessLogic.Services.CaoService.Interfaces;
using Data.Models;
using Utility.Extensions;

namespace BusinessLogic.Services.CaoService.Rules;

public class UnderSixteenCaoService : ICaoService
{
    public bool ValidateShift(Shift shift, Employee employee)
    {
        // General CAO rules still apply
        if (!new GeneralCaoService().ValidateShift(shift, employee))
        {
            return false;
        }

        // Rule: Max 8 hours per day including school.
        if (shift.Duration + NumberOfSchoolHours(employee, shift) > 8)
        {
            return false;
        }

        // Rule: Max until 19:00.
        if (new DateTime(shift.End.Year, shift.End.Month, shift.End.Day, shift.End.Hour, shift.End.Minute, 0) >
               new DateTime(shift.Start.Year, shift.Start.Month, shift.Start.Day, 19, 0, 0))
        {
            return false;
        }
        
        return true;
    }

    public int NumberOfSchoolHours(Employee employee, Shift shift)
    {
        if (employee.SchoolHours == null)
        {
            return 0;
        }
        
        return employee.SchoolHours.Where(sh => sh.DayOfWeek.ToString() == shift.Start.DayOfWeek.ToString())
            .Sum(sh => sh.Hours);
    }
}