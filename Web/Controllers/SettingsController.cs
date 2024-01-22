using System.ComponentModel.DataAnnotations;
using Data.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers;

[Authorize]
public class SettingsController : Controller
{
    private readonly INormRepository _normRepository; 

    public SettingsController(INormRepository normRepository)
    {
        _normRepository = normRepository;
    }
    
    // GET
    public IActionResult Settings()
    {
        var Settings = new SettingsViewModel
        {
            Norms = _normRepository.GetAll(),
        };        
        return View(Settings);
    }


    [HttpPost]
    public IActionResult EditNorms(SettingsViewModel settingsModel)
    {
        if (settingsModel.NormValues != null)
        {
            var validationContext = new ValidationContext(settingsModel, serviceProvider: null, items: null);
            var validationResults = settingsModel.Validate(validationContext);

            if (validationResults.Any())
            {
                settingsModel.Norms =  _normRepository.GetAll();
                foreach (var validationResult in validationResults)
                {
                    ModelState.AddModelError("Negatieve waarde", validationResult.ErrorMessage);
                }           
            } else
            {               
                var normsToUpdate = new List<Norm>();
                foreach (var kvp in settingsModel.NormValues)
                {
                    normsToUpdate.Add(new Norm { Type = kvp.Key, Value = kvp.Value, BranchId = 1 });
                }
                _normRepository.UpdateRange(normsToUpdate);
            }
        }        
        settingsModel.Norms =  _normRepository.GetAll();
        
        return View("Settings", settingsModel);
    }
}
