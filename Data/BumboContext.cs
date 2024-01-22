using System.Security.Principal;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data;


public class BumboContext : IdentityDbContext<AppUser, IdentityRole, string>
{
    public BumboContext(DbContextOptions<BumboContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.User)
            .WithOne(u => u.Employee)
            .HasForeignKey<Employee>(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Shift>()
            .HasOne(s => s.Employee)
            .WithMany(e => e.Shifts)
            .HasForeignKey(s => s.EmployeeId)
            .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<Shift>()
            .HasOne(s => s.Branch)
            .WithMany(b => b.Shifts)
            .HasForeignKey(s => s.BranchId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<Shift>()
            .HasOne(s => s.Department)
            .WithMany(b => b.Shifts)
            .HasForeignKey(s => s.DepartmentName)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<RegisteredHour>()
            .HasOne(rh => rh.Employee)
            .WithMany(e => e.RegisteredHours)
            .HasForeignKey(rh => rh.EmployeeId)
            .OnDelete(DeleteBehavior.NoAction);  // Adjust the delete behavior as needed
        
        
        modelBuilder.Entity<EmployeeDepartment>()
            .HasKey(ed => new { ed.EmployeeId, ed.DepartmentId });

        modelBuilder.Entity<EmployeeDepartment>()
            .HasOne(ed => ed.Employee)
            .WithMany(e => e.EmployeeDepartments)
            .HasForeignKey(ed => ed.EmployeeId);

        modelBuilder.Entity<EmployeeDepartment>()
            .HasOne(ed => ed.Department)
            .WithMany(d => d.EmployeeDepartments)
            .HasForeignKey(ed => ed.DepartmentId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<ReplacementRequest>()
            .HasKey(rr => new { rr.EmployeeId, rr.ShiftId });
    }

    public DbSet<Branch> Branches { get; set; }
    public DbSet<Norm> Norms { get; set; }
    public DbSet<DailyPrognosis> DailyPrognosis { get; set; }
    public DbSet<DailyExpectations> DailyExpectations { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<SchoolHours> SchoolHours { get; set; }
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<Absence> Absences { get; set; }
    public DbSet<Availability> Availabilities { get; set; }
    public DbSet<EmployeeDepartment> EmployeeDepartments { get; set; }
    public DbSet<ReplacementRequest> ReplacementRequests { get; set; }
    
    public DbSet<RegisteredHour> RegisteredHours { get; set; }
}