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
    public readonly IStatusService _statusService;

    public TaskItemsMSSQLService(DataContext dataContext, IPriorityService priorityService, IUserService userService, IStatusService statusService)
    {
        _dataContext = dataContext;
        _priorityService = priorityService;      
        _userService = userService;
        _statusService = statusService;
    }

    public void Delete(int id)
    {
        TaskItem temp = this.Find(id);
        _dataContext.TaskItems.Remove(temp);
        _dataContext.SaveChanges();
    }

    public TaskItem Find(int id)
    {
        return _dataContext.TaskItems.SingleOrDefault(o => o.Id == id);
    }

    public List<TaskItem> FindByUser(int userId)
    {
        return GetAll().FindAll(x => x.UserId == userId);
    }

    public List<TaskItem> GetAll()
    {
        List<TaskItem> taskItem = _dataContext.TaskItems.ToList<TaskItem>();

        foreach (TaskItem task in taskItem)
        {
            task.Priority = _priorityService.Find(task.PriorityId);
            task.User = _userService.Find(task.UserId);
            task.Status = _statusService.Find(task.StatusId);
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
            temp.StatusId = task.StatusId;
            temp.Status = _statusService.Find(task.StatusId);
            temp.Desc = task.Desc;
            temp.UserId = task.UserId;
            temp.PriorityId = task.PriorityId;
            temp.Priority = _priorityService.Find(task.PriorityId);
        }

        _dataContext.SaveChanges();
    }

    public int[] CountByStatus()
    {
        int todoCount = _dataContext.TaskItems.Where(t => t.StatusId == 1).Count();
        int inProgressCount = _dataContext.TaskItems.Where(t => t.StatusId == 2).Count();
        int doneCount = _dataContext.TaskItems.Where(t => t.StatusId == 3).Count();

        int[] count = new int[3] {todoCount,inProgressCount,doneCount};

        return count;
    }
}