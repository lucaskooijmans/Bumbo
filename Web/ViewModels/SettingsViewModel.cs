using System.ComponentModel.DataAnnotations;
using Data.Enums;
using Data.Models;

namespace Web.ViewModels;

public class SettingsViewModel : IValidatableObject
{
    private IValidatableObject _validatableObjectImplementation;
    public IEnumerable<Norm> Norms { get; set; }
    
    public Dictionary<NormTypes, int> NormValues { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        foreach (var key in NormValues.Keys)
        {
            if (NormValues[key] < 0)
            {
                yield return new ValidationResult("Norm moet een positieve waarde zijn.");
            }
        }
    }
}
