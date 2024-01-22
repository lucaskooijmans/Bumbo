using BusinessLogic.Services.CaoService.Interfaces;
using BusinessLogic.Services.CaoService.Rules;
using Data.Models;

namespace BusinessLogic.Services.CaoService.Factories;

public class CaoServiceFactory
{
    public ICaoService GetCaoService(Employee employee)
    {
        switch (employee.Age)
        {
            case < 16:
                return new UnderSixteenCaoService();
            case >= 16 and <= 17:
                return new MinorSixteenSeventeenCaoService();
            default:
                return new GeneralCaoService();
        }
    }
}
