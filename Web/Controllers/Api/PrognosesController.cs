using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class PrognosesController : ControllerBase
{
    private readonly IPrognosisRepository _prognosisRepository;
    
    public PrognosesController(IPrognosisRepository prognosisRepository)
    {
        _prognosisRepository = prognosisRepository;
    }
    
    [HttpPost("GetPrognosis")]
    public IActionResult GetPrognosis([FromQuery]string date)
    {
        Console.WriteLine(date);
        DateTime dateParsed = DateTime.Parse(date);
        Console.WriteLine(dateParsed);
        var prognosis = _prognosisRepository.GetDailyPrognosis(dateParsed);
        Console.WriteLine(prognosis.Count);
        return Ok(prognosis);
    }
}