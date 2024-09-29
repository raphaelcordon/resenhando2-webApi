using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Resenhando2.Api.Controllers;

[ApiController]
[Route("/")]
public class HomeController : ControllerBase
{
    [HttpGet("")]
    public async Task<IActionResult> HealthCheck()
    {
        return Ok("RESENHANDO 2.0 Web API is up and running.");
    }
}