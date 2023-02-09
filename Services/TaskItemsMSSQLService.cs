namespace WebApiTest.Services;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using WebApiTest.Interfaces;
using WebApiTest.Models;
using WebApiTest.Data;
using WebApiTest.Commands;
using System.Text.Json.Serialization;

public class TaskItemsMSSQLService : ITaskItemsService
{
    public readonly DataContext _dataContext;
    public readonly IUserService _userService;
    public readonly IPriorityService _priorityService;

    public TaskItemsMSSQLService(DataContext dataContext, IPriorityService priorityService, IUserService userService)
    {
        _dataContext = dataContext;
        _priorityService = priorityService;      
        _userService = userService;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public TaskItem Find(int id)
    {
        return _dataContext.TaskItems.SingleOrDefault(o => o.Id == id);
    }

    public List<TaskItem> GetAll()
    {
        List<TaskItem> taskItem = _dataContext.TaskItems.ToList<TaskItem>();

        foreach (TaskItem task in taskItem)
        {
            task.Priority = _priorityService.Find(task.PriorityId);
            task.User = _userService.Find(task.UserId);
        }

        return taskItem;
    }

    public void Save(TaskItem task)
    {
        if (task.Id == null || task.Id == 0)
        {
            //insert
            _dataContext.TaskItems.Add(task);
        }
        else
        {
            //update
            TaskItem temp = this.Find(task.Id);
            temp.TaskName = task.TaskName;
            temp.Status = task.Status;
            temp.Desc = task.Desc;
            temp.UserId = task.UserId;
            temp.PriorityId = task.PriorityId;
            temp.Priority = _priorityService.Find(task.PriorityId);
        }

        _dataContext.SaveChanges();
    }
}