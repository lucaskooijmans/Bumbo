using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels;

public class CreateRegisteredHourViewModel : IValidatableObject
{
    public int EmployeeId   { get; set; }
    public TimeSpan Start  { get; set; }
    public TimeSpan End    { get; set; }
    public DateTime Date   { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Start >= End)
        {
            yield return new ValidationResult("End time must be greater than the start time.", new[] { nameof(End) });
        }
    }
}