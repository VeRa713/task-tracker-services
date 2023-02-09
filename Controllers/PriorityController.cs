namespace WebApiTest.Controllers;

using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.Interfaces;
using WebApiTest.Models;
using WebApiTest.Commands;
using WebApiTest.Operations;

// base route = task_items/HttpGet
[ApiController]
[Route("priorities")]
public class PriorityController : ControllerBase
{
    private readonly IPriorityService _priorityService;

    public PriorityController(IPriorityService priorityService)
    {
        _priorityService = priorityService;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        Dictionary<string, object> data = new Dictionary<string, object>();

        data.Add("priorities", _priorityService.GetAll());

        return Ok(data);
    }

    [HttpPost("")]
    public IActionResult Save([FromBody] object payload)
    {
        // curl -X POST -H "Content-Type: application/json" -d @payloads/priority.json http://localhost:5003/priority | jq
        Dictionary<string, object> hash = JsonSerializer.Deserialize<Dictionary<string, object>>(payload.ToString());

        BuildPriorityFromDictionary builder = new BuildPriorityFromDictionary(hash);

        Priority priority = builder.Execute();

        _priorityService.Save(priority);

        Dictionary<string, object> message = new Dictionary<string, object>();
        message.Add("message", "OK");

        return Ok(message);
    }
}
