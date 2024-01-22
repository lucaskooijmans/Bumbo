using System.Globalization;

namespace Utility.Extensions;

public static class DateTimeExtensions
{
    public static DateTime StartOfWeek(this DateTime date)
    {
        int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
        return date.AddDays(-diff);
    }

    public static DateTime EndOfWeek(this DateTime date)
    {
        int diff = (7 - (date.DayOfWeek - DayOfWeek.Sunday)) % 7;
        return date.AddDays(diff);
    }

    public static int Week(this DateTime date)
    {
        CultureInfo cultureInfo = CultureInfo.CurrentCulture;
        Calendar calendar = cultureInfo.Calendar;
        CalendarWeekRule rule = CalendarWeekRule.FirstDay;
        DayOfWeek firstDayOfWeek = DayOfWeek.Monday;

       
        return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                        date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
    }

    public static string DayName(this DateTime date)
    {
        return date.ToString("dddd", new CultureInfo("nl-NL"));
    }

    public static string Time(this DateTime date)
    {
        return date.ToString("HH:mm");
    }

    public static string DateName(this DateTime date)
    {
        return date.ToString("dddd d MMMM yyyy", new CultureInfo("nl-NL"));
    }

    public static string DateNameShort(this DateTime date)
    {
        string dayOfWeek = date.ToString("ddd", new CultureInfo("nl-NL"));
        string dayOfMonth = date.ToString("d MMM", new CultureInfo("nl-NL"));
        string year = date.ToString("yyyy");

        return $"{dayOfWeek} {dayOfMonth} {year}"; // return format example: ma 27 nov. 2023 instead of maandag 27 november 2023
    }

    public static bool IsMidnight(this DateTime dateTime)
    {
        return dateTime.TimeOfDay == TimeSpan.Zero;
    }
    public static DateTime SetTime(this DateTime date, int hour, int minute)
    {
        return new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
    }
}