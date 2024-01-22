using System.ComponentModel.DataAnnotations;
using Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

[PrimaryKey(nameof(Name))]
public class Department
{
    public int BranchId { get; set; }

    [MaxLength(45)] public Departments Name { get; set; }

    public int Meters { get; set; }

    public Branch Branch { get; set; }
    public ICollection<EmployeeDepartment>? EmployeeDepartments { get; set; }
    public IEnumerable<Shift>? Shifts { get; set; }
}