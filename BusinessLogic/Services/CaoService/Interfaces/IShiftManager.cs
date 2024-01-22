using Data.Models;

namespace BusinessLogic.Services.CaoService.Interfaces;

public interface IShiftManager
{
    bool CreateShift(Shift shift, Employee employee);
    bool UpdateShift(Shift shift, Employee employee);
}