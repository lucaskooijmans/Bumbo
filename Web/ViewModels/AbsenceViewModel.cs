using System.ComponentModel;
using Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels;
public class AbsenceViewModel : IValidatableObject
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }

    [Required]
    [DisplayName("Vanaf datum")]
    public DateTime? StartDate { get; set; }

    [DisplayName("vanaf tijd")]
    public DateTime? StartTime { get; set; }
    
    [Required]
    [DisplayName("tot datum")]
    public DateTime? EndDate { get; set; }
    
    [DisplayName("tot tijd")]
    public DateTime? EndTime { get; set; }
    
    public EmployeeViewModel? Employee { get; set; }
    
    [MaxLength(100)]
    [DisplayName("Omschrijving")]
    public string? Description { get; set; }
    public AbsenceStatus? Status { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndDate.HasValue && StartTime.HasValue && EndDate.Value.Date == StartDate.Value.Date && EndTime <= StartTime)
        {
            yield return new ValidationResult("End time must be greater than the start time if on the same day.", new[] { nameof(EndTime) });
        }
        else if (EndDate.HasValue && EndDate < StartDate)
        {
            yield return new ValidationResult("End date must be greater than or equal to the start date.", new[] { nameof(EndDate) });
        }
    }
}