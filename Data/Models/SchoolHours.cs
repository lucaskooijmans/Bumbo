using Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data.Models;

public class SchoolHours
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public WeekDays DayOfWeek { get; set; }
    [Range(0, 24, ErrorMessage = "Hours must be between 0 and 24.")]
    public int Hours { get; set; }

    public Employee? Employee { get; set; }
}

