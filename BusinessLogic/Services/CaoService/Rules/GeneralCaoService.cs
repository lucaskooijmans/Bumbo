using BusinessLogic.Services.CaoService.Interfaces;
using Data.Models;
using Utility.Extensions;

namespace BusinessLogic.Services.CaoService.Rules;

public class GeneralCaoService : ICaoService
{
    public bool ValidateShift(Shift shift, Employee employee)
    {
        // Rule: Maximum 12 hours per shift.    
        if (shift.Duration > 12)
        {
            return false;
        }

        // Rule: Maximum 60 hours per week.
        if (MaxWeeklyHours(employee) + shift.Duration > 60)
        {
            return false;
        }
        
        return true;
    }
    

    public int MaxWeeklyHours(Employee employee)
    {
        if (employee.Shifts == null) 
        {
            return 0;
        }

        DateTime lastWeekStart = DateTime.Today.StartOfWeek();
        DateTime lastWeekEnd = DateTime.Today.EndOfWeek();
        
        return employee.Shifts
            .Where(s => s.Start >= lastWeekStart && s.Start <= lastWeekEnd)
            .Sum(s => s.Duration);    
    }
    
}