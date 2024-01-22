using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models;

public class DailyExpectations
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int BranchId { get; set; }

    [Required] public DateTime Date { get; set; }

    [Required] public int? ExpectedColli { get; set; }

    [Required] public int? ExpectedCustomers { get; set; }

    public int ?ExpectedTemperature { get; set; }

    public string ?ExpectedWeather { get; set; }

    public string ?Holiday { get; set; }

    public Branch Branch { get; set; }
}