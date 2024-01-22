using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models;

public class Branch
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [MaxLength(45)] [Required] public string Name { get; set; }

    [Required] 
    [MaxLength(45)] 
    public string PostalCode { get; set; }

    [Required] [MaxLength(45)] public string City { get; set; }

    [Required] [MaxLength(45)] public string Street { get; set; }

    [Required] public int Number { get; set; }

    public ICollection<DailyPrognosis>? DailyPrognosis { get; }
    public ICollection<DailyExpectations>? DailyExpection { get; }
    public ICollection<Norm>? Norm { get; }
    public ICollection<Employee>? Employees { get; }
    public ICollection<Shift>? Shifts { get; }
}