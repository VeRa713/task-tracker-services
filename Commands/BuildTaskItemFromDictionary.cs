namespace WebApiTest.Commands;

using WebApiTest.Models;
using System.Text.Json;
using WebApiTest.Interfaces;

public class BuildTaskItemFromDictionary
{
    private Dictionary<string, object> data;
    private IUserService _userService;
    private IPriorityService _priorityService;

    // 1 - Constructor that passes all inputs required
    public BuildTaskItemFromDictionary(Dictionary<string, object> hash, IPriorityService priorityService, IUserService userService)
    {
        this.data = hash;
        _priorityService = priorityService;
        _userService = userService;
    }

    // 2 - Executable Method. Like its own Main method
    public TaskItem Execute()
    {
        TaskItem newTaskitem = new TaskItem();
        Priority priority = new Priority();

        // newTaskitem.Id = int.Parse(data["id"].ToString());
        // newTaskitem.TaskName = data["task_name"].ToString();
        // newTaskitem.Status = int.Parse(data["status"].ToString());
        // newTaskitem.Priority = int.Parse(data["priority"].ToString());
        // newTaskitem.Desc = data["desc"].ToString();
        // newTaskitem.TeamId = int.Parse(data["team_id"].ToString());

        if (data.ContainsKey("id"))
        {
            newTaskitem.Id = int.Parse(data["id"].ToString());
        }

        if (data.ContainsKey("task_name"))
        {
            newTaskitem.TaskName = data["task_name"].ToString();
        }

        if (data.ContainsKey("status"))
        {
            newTaskitem.Status = int.Parse(data["status"].ToString());
        }

        //if desc is an empty string or does not exist set it to empty
        if (data.ContainsKey("desc"))
        {
            newTaskitem.Desc = data["desc"].ToString();
        }
        else
        {
            newTaskitem.Desc = "";
        }

        //if team id is empty or does not exist set it to unassigned
        if (data.ContainsKey("user_id"))
        {
            int userId = int.Parse(data["user_id"].ToString());
            newTaskitem.UserId = userId;
            newTaskitem.User = _userService.Find(userId);
        }

        if (data.ContainsKey("priority_id"))
        {
            //if it has priority Id, assign priority id and then find priority for display
            int priorityId = int.Parse(data["priority_id"].ToString());
            newTaskitem.PriorityId = priorityId;
            newTaskitem.Priority = _priorityService.Find(priorityId);
        }

        return newTaskitem;
    }
}
