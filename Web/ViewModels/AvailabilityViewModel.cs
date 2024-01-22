using System.ComponentModel.DataAnnotations;
using Data.Enums;
using Utility.Extensions;

namespace Web.ViewModels;

public class AvailabilityViewModel : IValidatableObject
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public WeekDays DayOfWeek { get; set; }
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (StartTime.HasValue && EndTime.HasValue && StartTime.Value >= EndTime.Value && !StartTime.Value.IsMidnight() && !EndTime.Value.IsMidnight())
        {
            yield return new ValidationResult("End time must be greater than the start time.", new[] { nameof(EndTime) });
        }
    }


}