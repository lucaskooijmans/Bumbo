using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Enums;

namespace Data.Models;

public class RegisteredHour
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public int EmployeeId { get; set; }
    
    [ForeignKey("EmployeeId")]
    public Employee Employee { get; set; }    
    
    public DateTime Start { get; set; }
    
    public DateTime? End { get; set; }
    
    public RegisteredHourStatus Status { get; set; }
    
    public int? ApprovedById { get; set; }
    
    [ForeignKey("ApprovedById")]
    public Employee ApprovedBy { get; set; }
}