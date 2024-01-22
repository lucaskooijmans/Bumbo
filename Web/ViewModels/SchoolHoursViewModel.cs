using Data.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace Web.ViewModels;

public class SchoolHoursViewModel
{
    public List<SchoolHours>? SchoolHours { get; set; }
}

