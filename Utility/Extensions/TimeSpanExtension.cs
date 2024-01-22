namespace Utility.Extensions;

public static class TimeSpanExtension
{
    public static bool IsMidnight(this TimeSpan time)
    {
        return time == TimeSpan.Zero;
    }
}