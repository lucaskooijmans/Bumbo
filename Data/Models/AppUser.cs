using Microsoft.AspNetCore.Identity;

namespace Data.Models;

public class AppUser : IdentityUser
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }

}