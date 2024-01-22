using Data.Enums;
using Data.Models;

namespace Web.ViewModels;

using System.ComponentModel.DataAnnotations;

public class EmployeeViewModel
{
    public int? Id { get; set; }
    
    [Display(Name = "Voornaam")]
    [Required(ErrorMessage = "Voornaam is verplicht")]
    [StringLength(50, ErrorMessage = "Voornaam mag niet langer zijn dan 50 tekens")]
    public string FirstName { get; set; }

    [Display(Name = "Achternaam")]
    [Required(ErrorMessage = "Achternaam is verplicht")]
    [StringLength(50, ErrorMessage = "Achternaam mag niet langer zijn dan 50 tekens")]
    public string LastName { get; set; }

    [Display(Name = "Telefoonnummer")]
    [Required(ErrorMessage = "Telefoonnummer is verplicht")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Ongeldig telefoonnummer. Voer 10 cijfers in.")]
    public string PhoneNumber { get; set; }

    [Display(Name = "E-mail")]
    [Required(ErrorMessage = "E-mail is verplicht")]
    [EmailAddress(ErrorMessage = "Ongeldig e-mailadres")]
    public string Email { get; set; }

    [Display(Name = "Geboortedatum")]
    [Required(ErrorMessage = "Geboortedatum is verplicht")]
    [DataType(DataType.Date, ErrorMessage = "Ongeldige geboortedatum")]
    public DateTime DateOfBirth { get; set; }

    [Display(Name = "BSN")]
    [Required(ErrorMessage = "BSN is verplicht")]
    public string BSN { get; set; }

    [Display(Name = "Postcode")]
    [Required(ErrorMessage = "Postcode is verplicht")]
    [RegularExpression(@"^\d{4}\s?[A-Za-z]{2}$", ErrorMessage = "Ongeldige postcode. Gebruik het formaat 1234 AB.")]
    public string PostalCode { get; set; }

    [Display(Name = "Stad")]
    [Required(ErrorMessage = "Stad is verplicht")]
    public string City { get; set; }

    [Display(Name = "Huisnummer")]
    [Required(ErrorMessage = "Huisnummer is verplicht")]
    public int HouseNumber { get; set; }

    [Display(Name = "Straatnaam")]
    [Required(ErrorMessage = "Straatnaam is verplicht")]
    public string Street { get; set; }
    
    [Display(Name = "Functie")]
    public string Role { get; set; }
    
    public List<Departments> Departments { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    
}
