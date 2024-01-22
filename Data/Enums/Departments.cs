using System.ComponentModel.DataAnnotations;

namespace Data.Enums;

public enum Departments
{
    [Display(Name = "Kassa")]
    Checkout,
    [Display(Name = "DKW")]
    Dkw,
    [Display(Name = "Vers")]
    Fresh
}