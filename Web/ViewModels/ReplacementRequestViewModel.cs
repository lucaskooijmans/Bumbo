using Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels;
public class ReplacementRequestViewModel
{
    [Required]
    public int EmployeeId { get; set; }
    [Required]
    public int ShiftId { get; set; }
    public Employee? Employee { get; set; }
    public Shift? Shift { get; set; }
}

