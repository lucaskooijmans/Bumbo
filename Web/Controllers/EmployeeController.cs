using Data.Enums;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Web.ViewModels;


namespace Web.Controllers;

[Authorize]
public class EmployeeController : Controller
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly UserManager<AppUser> _userManager;

    public EmployeeController(IEmployeeRepository employeeRepository, UserManager<AppUser> userManager)
    {
        _employeeRepository = employeeRepository;
        _userManager = userManager;
    }

    // GET: Employee
    public IActionResult Index(string searchString)
    {
        var employees = _employeeRepository.GetAll(searchString);

        var employeeViewModel = employees.Select(employee => new EmployeeViewModel
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            PhoneNumber = employee.PhoneNumber,
            Email = employee.Email,
            DateOfBirth = employee.DateOfBirth,
            BSN = employee.BSN,
            PostalCode = employee.PostalCode,
            City = employee.City,
            HouseNumber = employee.HouseNumber,
            Street = employee.Street,
            Role = "Temporary"
        });

        return View(employeeViewModel);
    }

    public IActionResult Create()
    {
        return View();
    }

    // POST: Employee/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(EmployeeViewModel createEmployeeViewModel)
    {
        if (ModelState.IsValid)
        {
            var employee = new Employee
            {
                FirstName = createEmployeeViewModel.FirstName,
                LastName = createEmployeeViewModel.LastName,
                PhoneNumber = createEmployeeViewModel.PhoneNumber,
                Email = createEmployeeViewModel.Email,
                DateOfBirth = createEmployeeViewModel.DateOfBirth,
                BSN = createEmployeeViewModel.BSN,
                PostalCode = createEmployeeViewModel.PostalCode,
                City = createEmployeeViewModel.City,
                HouseNumber = createEmployeeViewModel.HouseNumber,
                Street = createEmployeeViewModel.Street,
                EmployeeDepartments = new List<EmployeeDepartment>(), 
                BranchId = 1
            };
            
            foreach (var department in createEmployeeViewModel.Departments)
            {
                employee.EmployeeDepartments.Add(new EmployeeDepartment
                {
                    DepartmentId = department
                });
            }
            
            AppUser user = new AppUser();
            user.UserName = createEmployeeViewModel.Email;
            user.Email = createEmployeeViewModel.Email;

            IdentityResult result = _userManager.CreateAsync(user, "Test123!").Result;

            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, createEmployeeViewModel.Role).Wait();
            }

            employee.User = user;
            _employeeRepository.Add(employee);

            return RedirectToAction(nameof(Index));
        }

        return View();
    }

    //
    // GET: Employee/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        var employee = _employeeRepository.Get(id.Value);

        var model = new EmployeeViewModel
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            PhoneNumber = employee.PhoneNumber,
            Email = employee.Email,
            DateOfBirth = employee.DateOfBirth,
            BSN = employee.BSN,
            PostalCode = employee.PostalCode,
            City = employee.City,
            HouseNumber = employee.HouseNumber,
            Street = employee.Street,
            Departments = employee.EmployeeDepartments
                .Select(ed => (Departments)ed.DepartmentId)
                .ToList(),
        };

        return View(model);
    }

    // POST: Employee/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(EmployeeViewModel editEmployeeViewModel)
    {
        if (ModelState.IsValid)
        {
            // List<Departments> departments = new List<Departments>();
            // foreach (var departmentsName in editEmployeeViewModel.DepartmentsNames)
            // {
            //     departments.Add(Enum.Parse<Departments>(departmentsName));
            // }
            var employee = _employeeRepository.Get(editEmployeeViewModel.Id.Value);

            employee.FirstName = editEmployeeViewModel.FirstName;
            employee.LastName = editEmployeeViewModel.LastName;
            employee.PhoneNumber = editEmployeeViewModel.PhoneNumber;
            employee.Email = editEmployeeViewModel.Email;
            employee.DateOfBirth = editEmployeeViewModel.DateOfBirth;
            employee.BSN = editEmployeeViewModel.BSN;
            employee.PostalCode = editEmployeeViewModel.PostalCode;
            employee.City = editEmployeeViewModel.City;
            employee.HouseNumber = editEmployeeViewModel.HouseNumber;
            employee.Street = editEmployeeViewModel.Street;

            employee.EmployeeDepartments.Clear();

            foreach (var departmentId in editEmployeeViewModel.Departments)
            {
                employee.EmployeeDepartments.Add(new EmployeeDepartment
                {
                    DepartmentId = departmentId
                });
            }

            
            if (_employeeRepository.Update(employee))
            {
                return RedirectToAction("Index");
            }
        }

        return View(editEmployeeViewModel);
    }
}