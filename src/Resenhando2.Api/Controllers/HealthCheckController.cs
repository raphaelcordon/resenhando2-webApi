using Microsoft.AspNetCore.Mvc;

namespace Resenhando2.Api.Controllers;

[ApiController]
[Route("")]
public class HealthCheckController : ControllerBase
{
    [HttpGet("/")]
    public IActionResult Home()
    {
        return Ok("RESENHANDO 2.0 Web API is up and running.\n \n" +
                  "Go to '/api/healthcheck' for more information.");
    }
    
    [HttpGet("api/healthcheck")]
    public IActionResult HealthCheck()
    {
        return Ok("Health check is under construction.");
    }
}