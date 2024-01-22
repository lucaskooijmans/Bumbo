using Data.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class ClockController : ControllerBase
{
    private readonly IRegisteredHourRepository _registeredHourRepository;
    
    public ClockController(IRegisteredHourRepository registeredHourRepository)
    {
        _registeredHourRepository = registeredHourRepository;
    }
    
    
    // GET api/clock
    [HttpPost]
    public IActionResult Post([FromQuery] int employeeId, string timestampString)
    {
        DateTime timestamp = DateTime.ParseExact(timestampString, "yyyy-MM-dd HH:mm:ss", null);
        
        var runningRegistered = _registeredHourRepository.GetRunningRegisteredHour(employeeId);

        if (runningRegistered != null) 
        {
            runningRegistered.End = timestamp;
            _registeredHourRepository.Update(runningRegistered);
        }
        else
        {
            var Register = new RegisteredHour
            {
                EmployeeId = employeeId,
                Start = timestamp
            };
            
            _registeredHourRepository.Add(Register);
        }
        
        return Ok();
    }
    
    // GET api/clock
    [HttpGet("register")]
    public ActionResult<string> Register(string ip)
    {
        // Your API logic here
        return $"Hello from ClockController! {ip}";
    }
}


