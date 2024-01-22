using Data.Enums;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;

    public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel input)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(input.Email, input.Password, false, false);
            
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(input.Email);
                
                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles.Contains(Roles.Manager.ToString()))
                    {
                        return RedirectToAction("Index", "ManagerDashboard"); // Redirect to manager area
                    }
                    
                    return RedirectToAction("Index", "EmployeeDashboard"); // Redirect to employee area
                }
            }

            ModelState.AddModelError(string.Empty, "De logingegevens zijn onjuist.");
        }

        return View();
    }
    
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }
}