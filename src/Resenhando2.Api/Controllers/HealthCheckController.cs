using Microsoft.AspNetCore.Mvc;

namespace Resenhando2.Api.Controllers;

[ApiController]
[Route("api/v1/healthcheck")]
public class HealthCheckController : ControllerBase
{
    [HttpGet("")]
    public IActionResult HealthCheck()
    {
        return Ok("RESENHANDO 2.0 Web API is up and running.");
    }
}