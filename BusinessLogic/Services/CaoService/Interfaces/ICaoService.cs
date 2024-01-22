using Data.Models;

namespace BusinessLogic.Services.CaoService.Interfaces;

public interface ICaoService
{
    bool ValidateShift(Shift shift, Employee employee);
}
