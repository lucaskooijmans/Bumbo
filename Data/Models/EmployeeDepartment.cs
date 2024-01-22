using Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Data.Models;

public class EmployeeDepartment
{ 
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }

    public Departments DepartmentId { get; set; }
    public Department Department { get; set; }

}

