namespace WebApiTest.Controllers;

using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.Interfaces;
using WebApiTest.Models;
using WebApiTest.Commands;
using WebApiTest.Operations;

// base route = task_items/HttpGet
[ApiController]
[Route("statuses")]
public class StatusController : ControllerBase
{
    private readonly IStatusService _statusService;

    public StatusController(IStatusService statusService)
    {
        _statusService = statusService;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        Dictionary<string, object> data = new Dictionary<string, object>();

        data.Add("statuses", _statusService.GetAll());

        return Ok(data);
    }

    [HttpPost("")]
    public IActionResult Save([FromBody] object payload)
    {
        Dictionary<string, object> hash = JsonSerializer.Deserialize<Dictionary<string, object>>(payload.ToString());

        BuildStatusFromDictionary builder = new BuildStatusFromDictionary(hash);

        Status status = builder.Execute();

        _statusService.Save(status);

        Dictionary<string, object> message = new Dictionary<string, object>();
        message.Add("message", "OK");

        return Ok(message);
    }
}