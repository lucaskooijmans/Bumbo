using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

[PrimaryKey(nameof(Type))]
public class Norm
{    
    [Key] 
    public NormTypes Type { get; set; }
    
    [Required] public int BranchId { get; set; }
    
    [Required] public int Value { get; set; }

    public Branch Branch { get; set; } = null!;
}