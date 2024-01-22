using BusinessLogic.Services.CaoService.Rules;
using BusinessLogic.Services.HoursCalculationService.Interfaces;
using BusinessLogic.Services.HoursCalculationService.Policies;
using Data.Models;

namespace BusinessLogic.Services.HoursCalculationService.Factories;

public class HoursPolicyFactory
{
    public IHourPolicy GetHourPolicy(Shift shift)
    {
        switch (shift.Start.DayOfWeek)
        {
            case DayOfWeek.Saturday:
                return new SaturdayHoursPolicy();
            case DayOfWeek.Sunday:
                return new SundayHoursPolicy();
            default:
                return new WeekdayHoursPolicy();
        }
    }
}