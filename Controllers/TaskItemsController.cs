namespace WebApiTest.Controllers;

using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.Interfaces;
using WebApiTest.Models;
using WebApiTest.Commands;
using WebApiTest.Operations;

// base route = task_items/HttpGet
[ApiController]
[Route("task_items")]
public class TaskItemsController : ControllerBase
{
    //Endpoint to return all taskitems
    //URL: GET /task_items

    private readonly ITaskItemsService _taskItemsService;
    private readonly IUserService _userService;
    private readonly IPriorityService _priorityService;
    private readonly IStatusService _statusService;

    public TaskItemsController(ITaskItemsService taskItemsService, IPriorityService priorityService, IUserService userService, IStatusService statusService)
    {
        _taskItemsService = taskItemsService;
        _priorityService = priorityService;
        _userService = userService;
        _statusService = statusService;
    }


    [HttpGet("")]
    public IActionResult Index()
    {
        return Ok(_taskItemsService.GetAll());
    }

    //Endpoint to return a single expense item based on id
    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        TaskItem task = _taskItemsService.Find(id);

        if (task == null)
        {
            return Ok("Task Item Not Found");
        }

        return Ok(task);
    }

    [HttpGet("user/{id}")]
    public IActionResult ShowTasks(int id)
    {
        List<TaskItem> tasks = _taskItemsService.FindByUser(id);

        if (tasks == null)
        {
            return Ok("No Tasks found for this user");
        }

        return Ok(tasks);
    }

    [HttpPost("")]
    public IActionResult Save([FromBody] object payload)
    {
        // curl -X POST -H "Content-Type: application/json" -d @payloads/taskItem.json http://localhost:5003/task_items | jq
        Dictionary<string, object> hash = JsonSerializer.Deserialize<Dictionary<string, object>>(payload.ToString());

        ValidateSaveTaskItem validator = new ValidateSaveTaskItem(hash);
        validator.Execute();

        if (validator.HasErrors())
        {
            return UnprocessableEntity(validator.Errors);
        }
        else
        {
            BuildTaskItemFromDictionary builder = new BuildTaskItemFromDictionary(hash, _priorityService, _userService, _statusService);

            TaskItem newTask = builder.Execute();

            _taskItemsService.Save(newTask);

            Dictionary<string, object> message = new Dictionary<string, object>();
            message.Add("message", "OK");

            return Ok(message);
        }
    }

    [HttpDelete("delete_task/{id}")]
    public IActionResult Delete(int id)
    {
        // Dictionary<string, object> hash = JsonSerializer.Deserialize<Dictionary<string, object>>(payload.ToString());
        // int id = int.Parse(hash["id"].ToString());

        _taskItemsService.Delete(id);

        Dictionary<string, object> message = new Dictionary<string, object>();
        message.Add("message", "Task successfully deleted!");

        return Ok(message);
    }
}
