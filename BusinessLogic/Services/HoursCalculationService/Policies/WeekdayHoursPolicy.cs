using BusinessLogic.Services.HoursCalculationService.Interfaces;
using Data.Models;

namespace BusinessLogic.Services.HoursCalculationService.Policies;

public class WeekdayHoursPolicy : IHourPolicy
{
    public Dictionary<int, double> CalculateHours(Shift shift)
    {
        List<Bonus> bonuses = new List<Bonus>()
        {
            new Bonus(6, 20, 0), 
            new Bonus(20, 21, 33),
            new Bonus(21, 23, 50),
            new Bonus(0, 6, 50)
        };
        Dictionary<int, double> hourBonuses = new Dictionary<int, double>();

        for (DateTime date = shift.Start.AddMinutes(60); date < shift.End.AddMinutes(60); date = date.AddMinutes(1))
        {
            foreach (Bonus bonus in bonuses)
            {
                int hour = date.TimeOfDay.Hours;
                if (hour >= bonus.startTime && hour <= bonus.endTime)
                {
                    if (hourBonuses.ContainsKey(bonus.bonusPercentage))
                    {
                        hourBonuses[bonus.bonusPercentage] += 1;
                        break;
                    }
                    else
                    {
                        hourBonuses.Add(bonus.bonusPercentage, 1);
                        break;
                    }
                }
            }
        }
        foreach (var hourBonus in hourBonuses)
        {
            hourBonuses[hourBonus.Key] = Math.Round(hourBonuses[hourBonus.Key] / 60, 2);
        }
        return hourBonuses;
    }
}