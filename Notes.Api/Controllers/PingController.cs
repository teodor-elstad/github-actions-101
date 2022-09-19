namespace Notes.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class PingController : ControllerBase
{
    /// <summary>
    /// Returns PONG.
    /// </summary>
    [HttpGet]
    public string GetPing() => "PONG";
}