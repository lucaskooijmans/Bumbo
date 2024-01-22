using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Enums;

namespace Data.Models;

public class DailyPrognosis
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public DateTime Date { get; set; }
    public int BranchId { get; set; }

    public Departments Department { get; set; }

    public int NumberOfHours { get; set; }
    public Branch Branch { get; set; }
}