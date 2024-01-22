using Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Utility.Extensions;

namespace Web.ViewModels;

public class ShiftEditCreateViewModel
{
    public Shift Shift { get; set; }
    public DateTime Date { get; set; } = DateTime.Today;
    public IEnumerable<EmployeeViewModel> Employees { get; set; }
    
    public List<int> EmployeeIds { get; set; } = new List<int>();
    
}