namespace Data.Enums;

public enum WeekDays
{
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday,
    Sunday
}

static class WeekDaysMethods
{
    public static WeekDays Convert(DayOfWeek dayOfWeek)
    {
        return dayOfWeek switch
        {
            DayOfWeek.Sunday => WeekDays.Monday,
            DayOfWeek.Monday => WeekDays.Tuesday,
            DayOfWeek.Tuesday => WeekDays.Wednesday,
            DayOfWeek.Wednesday => WeekDays.Thursday,
            DayOfWeek.Thursday => WeekDays.Friday,
            DayOfWeek.Friday => WeekDays.Saturday,
            DayOfWeek.Saturday => WeekDays.Sunday,
            _ => throw new ArgumentOutOfRangeException(nameof(dayOfWeek), dayOfWeek, null)
        };
    }
}