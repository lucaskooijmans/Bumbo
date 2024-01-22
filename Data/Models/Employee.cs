using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Data.Models;

public class Employee
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? UserId { get; set; }
    public AppUser? User { get; set; }
    public int BranchId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string BSN { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public int HouseNumber { get; set; }
    public string Street { get; set; }
    public Branch Branch { get; set; }
    public ICollection<SchoolHours>? SchoolHours { get; set; }
    public ICollection<Shift>? Shifts { get; set; }
    public ICollection<Absence>? Absences { get; set; }
    public ICollection<Availability>? Availabilities { get; set; }
    public ICollection<EmployeeDepartment>? EmployeeDepartments { get; set; }
    public ICollection<ReplacementRequest>? replacementRequests { get; set; }
    public ICollection<RegisteredHour>? RegisteredHours { get; set; }
    public int Age
    {
        get
        {
            var age = DateTime.Today.Year - DateOfBirth.Year;
            if (DateOfBirth.Date > DateTime.Today.AddYears(-age)) age--;
            return age;
        }
    }
}