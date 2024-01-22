using Data.Enums;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Utility.Extensions;

namespace Data;

public class DataSeeder
{
    private readonly BumboContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DataSeeder(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, BumboContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
    }

    public void SeedData()
    {
        SeedRoles();
        SeedBranchesNormsDepartments();
        SeedEmployeesAndUsers();
        SeedDailyExpectations();
        SeedShifts();
        SeedWorkedHours();
    }

    private void SeedRoles()
    {
        if (!_roleManager.RoleExistsAsync("Manager").Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = "Manager";
            IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
        }

        if (!_roleManager.RoleExistsAsync("Employee").Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = "Employee";
            IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
        }
    }

    private void SeedEmployeesAndUsers()
    {
        if (_userManager.FindByEmailAsync("admin@example.com").Result == null)
        {
            AppUser adminUser = new AppUser();
            adminUser.UserName = "admin@example.com";
            adminUser.Email = "admin@example.com";

            Employee adminEmployee = new Employee
            {
                FirstName = "Admin",
                LastName = "User",
                DateOfBirth = GenerateRandomDateOfBirth(14, 26),
                BSN = GenerateRandomNumber(9).ToString(),
                PostalCode = "1234AB",
                City = "City",
                HouseNumber = 1,
                Street = "Street",
                BranchId = 1,
                PhoneNumber = "0612345678",
                Email = "admin@example.com"
            };

            _context.Employees.Add(adminEmployee);
            _context.SaveChanges();

            adminUser.EmployeeId = adminEmployee.Id;

            IdentityResult result = _userManager.CreateAsync(adminUser, "Test123!").Result;

            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(adminUser, Roles.Manager.ToString()).Wait();

                adminEmployee.UserId = adminUser.Id;

                _context.Employees.Update(adminEmployee);
            }

            _context.SaveChanges();
        }

        string[] names = { "Piet Hein", "Jan Janssen", "Cas De jong", "Frans De vries", "Nienke Visser" };
        Random random = new Random();

        foreach (var name in names)
        {
            string email = GenerateEmail(name);
            (string firstName, string lastName) = SplitFullName(name);

            if (_userManager.FindByEmailAsync(email).Result == null)
            {
                AppUser user = new AppUser();
                user.UserName = email;
                user.Email = email;

                Employee employee = new Employee();
                employee.FirstName = firstName;
                employee.LastName = lastName;
                employee.DateOfBirth = GenerateRandomDateOfBirth(14, 26);
                employee.BSN = GenerateRandomNumber(9).ToString();
                employee.PostalCode = "1234AB";
                employee.City = "City";
                employee.HouseNumber = 1;
                employee.Street = "Street";
                employee.BranchId = 1;
                employee.PhoneNumber = "0612345678";
                employee.Email = email;

                List<Department> allDepartments = _context.Departments.ToList();
                int numberOfDepartments = random.Next(1, allDepartments.Count + 1);

                List<Department> randomDepartments =
                    allDepartments.OrderBy(d => random.Next()).Take(numberOfDepartments).ToList();
                List<EmployeeDepartment> employeeDepartments = new List<EmployeeDepartment>();
                foreach (var department in randomDepartments)
                {
                    employeeDepartments.Add(new EmployeeDepartment
                    {
                        DepartmentId = department.Name
                    });
                }

                employee.EmployeeDepartments = employeeDepartments;

                // Save the employee to the database
                _context.Employees.Add(employee);
                _context.SaveChanges();

                // Now the employee should have an assigned ID
                user.EmployeeId = employee.Id;

                IdentityResult result = _userManager.CreateAsync(user, "Test123!").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, Roles.Employee.ToString()).Wait();

                    employee.UserId = user.Id;

                    _context.Employees.Update(employee);
                }

                _context.SaveChanges();
            }
        }
    }


    private void SeedDailyExpectations()
    {
        if (!_context.DailyExpectations.Any())
        {
            int numberOfDaysInWeek = 7;
            int amountOfPreviousWeeks = 8;
            int amountOfNextWeeks = 2;
            int amountOfTotalDays = (amountOfNextWeeks + amountOfPreviousWeeks) * numberOfDaysInWeek;
            DateTime startDate =
                DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek - numberOfDaysInWeek * amountOfPreviousWeeks + 1);

            Random random = new Random();
            for (int day = 0; day <= amountOfTotalDays; day++)
            {
                double randomNumber = random.NextDouble() + 0.5;
                string? weather = null;

                _context.DailyExpectations.Add(
                    new DailyExpectations
                    {
                        Date = startDate.AddDays(day),
                        BranchId = 1,
                        ExpectedColli = (int)(150 * randomNumber),
                        ExpectedCustomers = (int)(170 * randomNumber),
                        ExpectedTemperature = (int)(20 * randomNumber),
                        ExpectedWeather = "sunny",
                    }
                );
            }
        }
    }

    private void SeedBranchesNormsDepartments()
    {
        if (!_context.Branches.Any())
        {
            _context.Branches.Add(
                new Branch
                {
                    Name = "Branch 1",
                    PostalCode = "12345",
                    City = "City 1",
                    Street = "Street 1",
                    Number = 123
                }
            );
        }

        List<Norm> norms = new List<Norm>();
        if (!_context.Norms.Any())
        {
            norms.AddRange(new List<Norm>
            {
                new Norm
                {
                    BranchId = 1,
                    Type = NormTypes.Mirroring,
                    Value = 30
                },
                new Norm
                {
                    BranchId = 1,
                    Type = NormTypes.Stock,
                    Value = 30
                },
                new Norm
                {
                    BranchId = 1,
                    Type = NormTypes.Unpack,
                    Value = 5
                },
                new Norm
                {
                    BranchId = 1,
                    Type = NormTypes.Fresh,
                    Value = 100
                },
                new Norm
                {
                    BranchId = 1,
                    Type = NormTypes.Checkout,
                    Value = 30
                }
            });

            _context.Norms.AddRange(norms);
        }

        if (!_context.Branches.Any())
        {
            List<Department> departments = new List<Department>();
            foreach (Departments departmentType in Enum.GetValues(typeof(Departments)))
            {
                Department department = new Department
                {
                    BranchId = 1, // Pas de juiste tak-ID aan
                    Name = departmentType,
                    Meters = 100 // Pas de juiste waarde aan
                };

                departments.Add(department);
            }

            _context.Departments.AddRange(departments);
        }

        _context.SaveChanges();
    }

    private void SeedShifts()
    {
        if (_context.Shifts.Count(s => s.Start >= DateTime.Now) < 20)
        {
            var employees = _context.Employees
                .Include(e => e.EmployeeDepartments)
                .Where(e => e.EmployeeDepartments.Any()).ToList();

            List<Shift> shifts = new List<Shift>();

            foreach (var employee in employees)
            {
                for (int i = 0; i < 15; i++)
                {
                    // Ensure no overlapping shifts for the same employee
                    DateTime randomDate, randomStart, randomEnd;
                    Random random = new Random();
                    var departments = employee.EmployeeDepartments.ToList();
                    var randomDepartment = departments.OrderBy(x => random.Next()).FirstOrDefault();
                    do
                    {
                        randomDate = GenerateRandomDate();
                        randomStart = GenerateRandomDateTime(randomDate, 7, 19);
                        randomEnd = GenerateRandomDateTime(randomDate, randomStart.Hour + 3, 22);
                    } while (ShiftOverlapsWithExisting(shifts, employee.Id, randomStart));

                    shifts.Add(new Shift
                    {
                        BranchId = 1,
                        EmployeeId = employee.Id,
                        DepartmentName = randomDepartment.DepartmentId,
                        Start = randomStart,
                        End = randomEnd,
                        Status = ShiftStatus.Published
                    });
                }
            }

            _context.Shifts.AddRange(shifts);
            _context.SaveChanges();
        }
    }


    private void SeedWorkedHours()
    {
        if (_context.RegisteredHours.Count(s => s.Start >= DateTime.Now.AddDays(-7) && s.End <= DateTime.Now) < 10)
        {
            var employees = _context.Employees
                .Include(e => e.EmployeeDepartments)
                .Where(e => e.EmployeeDepartments.Any()).ToList();
            
            List<RegisteredHour> registeredHours = new List<RegisteredHour>();
            
            foreach (var employee in employees)
            {
                for (int i = 0; i < 5; i++)
                {
                    DateTime randomDate, randomStart, randomEnd;
                    do
                    {
                        randomDate = GenerateRandomDate();
                        randomStart = GenerateRandomDateTime(randomDate, 7, 19);
                        randomEnd = GenerateRandomDateTime(randomDate, randomStart.Hour + 3, 22);
                    } while (RegisteredHoursOverlapsWithExisting(registeredHours, employee.Id, randomStart));

                    registeredHours.Add(new RegisteredHour
                    {
                        EmployeeId = employee.Id,
                        Start = randomStart,
                        End = randomEnd,
                        Status = RegisteredHourStatus.Registered
                    });
                }
            }
            
            
            _context.RegisteredHours.AddRange(registeredHours);
            _context.SaveChanges();
        }
    }


    private bool RegisteredHoursOverlapsWithExisting(List<RegisteredHour> existingHours, int employeeId,
        DateTime newStart)
    {
        foreach (var existingHour in existingHours)
        {
            // Check if the employee is the same
            if (existingHour.EmployeeId == employeeId)
            {
                // Check for overlap
                if (newStart.Date == existingHour.Start.Date)
                {
                    // Overlapping shift found
                    return true;
                }
            }
        }

        return false;
    }
    
    private bool ShiftOverlapsWithExisting(List<Shift> existingShifts, int employeeId, DateTime newStart)
    {
        foreach (var existingShift in existingShifts)
        {
            // Check if the employee is the same
            if (existingShift.EmployeeId == employeeId)
            {
                // Check for overlap
                if (newStart.Date == existingShift.Start.Date)
                {
                    // Overlapping shift found
                    return true;
                }
            }
        }

        return false;
    }

    public static string GenerateEmail(string fullName)
    {
        // Remove spaces from the full name and split into first and last names
        string[] nameParts = fullName.Replace(" ", "").Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        // If you want the domain to be constant, you can replace "example.com" with your desired domain
        string domain = "example.com";

        // Create the email by joining the names with dots and appending the domain
        string email = $"{string.Join(".", nameParts).ToLower()}@{domain}";

        return email;
    }


    private static (string firstName, string lastName) SplitFullName(string fullName)
    {
        string[] nameParts = fullName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        if (nameParts.Length == 1)
        {
            // If there's only one part, consider it as the first name
            return (nameParts[0], "");
        }
        else
        {
            // If there are multiple parts, the last part is the last name
            string lastName = nameParts[nameParts.Length - 1];

            // The remaining parts are the first name
            string firstName = string.Join(" ", nameParts, 0, nameParts.Length - 1);

            return (firstName, lastName);
        }
    }

    public static DateTime GenerateRandomDateOfBirth(int minAge, int maxAge)
    {
        Random random = new Random();

        // Calculate the maximum and minimum birth years based on the desired age range
        int currentYear = DateTime.Now.Year;
        int maxBirthYear = currentYear - minAge;
        int minBirthYear = currentYear - maxAge;

        // Generate a random birth year within the specified range
        int randomBirthYear = random.Next(minBirthYear, maxBirthYear + 1);

        // Generate random month and day
        int randomMonth = random.Next(1, 13);
        int randomDay = random.Next(1, DateTime.DaysInMonth(randomBirthYear, randomMonth) + 1);

        // Create a DateTime object with the generated values
        DateTime randomDateOfBirth = new DateTime(randomBirthYear, randomMonth, randomDay);

        return randomDateOfBirth;
    }

    public static int GenerateRandomNumber(int length)
    {
        if (length < 1 || length > 9)
        {
            throw new ArgumentException("Length must be between 1 and 9.");
        }

        Random random = new Random();

        // Calculate the upper bound for the specified number of digits
        int upperBound = (int)Math.Pow(10, length);

        // Generate a random number within the specified range
        int randomNumber = random.Next(upperBound);

        return randomNumber;
    }

    private DateTime GenerateRandomDateTime(DateTime date, int startHour, int endHour)
    {
        if (date == DateTime.MinValue)
        {
            // If no date is provided, generate a random date within a reasonable range
            date = GenerateRandomDate();
        }

        Random random = new Random();

        // Generate random hours, minutes, and seconds
        int randomHour = random.Next(startHour, endHour + 1); // Ensure endHour is inclusive
        int randomMinute = random.Next(0, 4) * 15; // Generate 0, 15, 30, or 45
        int randomSecond = random.Next(0, 60);

        // Combine date and time components to create a DateTime object
        DateTime randomDateTime = new DateTime(date.Year, date.Month, date.Day, randomHour, randomMinute, randomSecond);

        return randomDateTime;
    }


    private DateTime GenerateRandomDate()
    {
        // Generate a random date within a reasonable range, e.g., the next 30 days
        Random random = new Random();
        int daysToAdd = random.Next(1, 31);

        DateTime currentDate = DateTime.Now.Date.AddDays(-2);
        DateTime randomDate = currentDate.AddDays(daysToAdd);

        return randomDate;
    }
}