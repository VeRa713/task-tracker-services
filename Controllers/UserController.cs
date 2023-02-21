namespace WebApiTest.Controllers;

using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.Interfaces;
using WebApiTest.Models;
using WebApiTest.Commands;
using WebApiTest.Operations;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        // Dictionary<string, object> data = new Dictionary<string, object>();

        // data.Add("users", _userService.GetAll());

        // return Ok(data);

        return Ok(_userService.GetAll());
    }

     [HttpPost("")]
    public IActionResult Save([FromBody] object payload)
    {
        // curl -X POST -H "Content-Type: application/json" -d @payloads/priority.json http://localhost:5003/priority | jq
        Dictionary<string, object> hash = JsonSerializer.Deserialize<Dictionary<string, object>>(payload.ToString());

        BuildUserFromDictionary builder = new BuildUserFromDictionary(hash);

        User user = builder.Execute();

        _userService.Save(user);

        Dictionary<string, object> message = new Dictionary<string, object>();
        message.Add("message", "OK");

        return Ok(message);
    }

    [HttpDelete("delete_user/{id}")]
    public IActionResult Delete(int id)
    {
        _userService.Delete(id);

        Dictionary<string, object> message = new Dictionary<string, object>();
        message.Add("message", "User successfully deleted!");

        return Ok(message);
    }

    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        User user = _userService.Find(id);

        if (user == null)
        {
            return Ok("Task Item Not Found");
        }

        return Ok(user);
    }
}
