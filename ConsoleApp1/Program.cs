using System;

class Program
{
    static void Main()
    {
        // Example usage:
        DateTime startTime = new DateTime(2023, 1, 1, 14, 0, 0); // 2:00 PM
        DateTime endTime = new DateTime(2023, 1, 1, 20, 20, 0); // 8:20 PM
        TimeSpan bonusStartTime = new TimeSpan(19, 0, 0); // 7:00 PM
        TimeSpan bonusEndTime = new TimeSpan(21, 0, 0); // 9:00 PM

        TimeSpan duration = CalculateDuration(startTime, endTime, bonusStartTime, bonusEndTime);

        Console.WriteLine($"Worked Duration: {duration.Hours} hours and {duration.Minutes} minutes");
    }

    static TimeSpan CalculateDuration(DateTime startTime, DateTime endTime, TimeSpan bonusStartTime, TimeSpan bonusEndTime)
    {
        // Ensure the start time is not after the end time
        if (startTime > endTime)
        {
            throw new ArgumentException("Start time must be before end time");
        }

        // Calculate the total duration
        TimeSpan totalDuration = endTime - startTime;

        // Calculate the bonus duration within the specified time period
        TimeSpan bonusDuration = TimeSpan.Zero;

        if (endTime.TimeOfDay > bonusStartTime)
        {
            DateTime bonusStart = startTime.Date.Add(bonusStartTime);
            DateTime bonusEnd = endTime.TimeOfDay > bonusEndTime ? endTime.Date.Add(bonusEndTime) : endTime;

            bonusDuration = bonusEnd - bonusStart;

            // If the bonus duration exceeds the total duration, limit it to the total duration
            bonusDuration = TimeSpan.FromMinutes(Math.Min(bonusDuration.TotalMinutes, totalDuration.TotalMinutes));
        }

        // Subtract the bonus duration from the total duration
        TimeSpan netDuration = totalDuration - bonusDuration;

        return bonusDuration;
    }
}