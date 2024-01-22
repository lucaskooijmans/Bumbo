
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Web.ViewComponents;

public class OpenAbsenceRequests: ViewComponent
{
    private readonly IAbsenceRepository _absenceRepository;
    
    public OpenAbsenceRequests(IAbsenceRepository absenceRepository)
    {
        _absenceRepository = absenceRepository;
    }

    public IViewComponentResult Invoke()
    {
        var absenceRequests = _absenceRepository.GetOpenAbsenceRequests();
        
        return View(absenceRequests);
    }
    
}