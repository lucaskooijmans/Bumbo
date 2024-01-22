using BusinessLogic.Services.CaoService.Factories;
using BusinessLogic.Services.CaoService.Interfaces;
using Data.Interfaces;
using Data.Models;

namespace BusinessLogic.Services.CaoService;

public class ShiftManager : IShiftManager
{
    private readonly IShiftRepository _shiftRepository;
    private readonly CaoServiceFactory _caoServiceFactory;

    public ShiftManager(IShiftRepository shiftRepository, CaoServiceFactory caoServiceFactory)
    {
        _shiftRepository = shiftRepository;
        _caoServiceFactory = caoServiceFactory;
    }
    
    public bool CreateShift(Shift shift, Employee employee)
    {
        var caoService = _caoServiceFactory.GetCaoService(employee);
        
        if (caoService.ValidateShift(shift, employee))
        {
            // Store the shift in the database
            _shiftRepository.Add(shift);
            return true;
        }

        return false;
    }
    
    public bool UpdateShift(Shift shift, Employee employee)
    {
        var caoService = _caoServiceFactory.GetCaoService(employee);

        if (caoService.ValidateShift(shift, employee))
        {
            // Store the shift in the database
            _shiftRepository.UpdateShift(shift);
            return true;
        }

        return false;
    }
}