using Data.Models;

namespace BusinessLogic.Services.HoursCalculationService.Interfaces;

public interface IHourPolicy 
{
    Dictionary<int, double> CalculateHours(Shift shift);
}