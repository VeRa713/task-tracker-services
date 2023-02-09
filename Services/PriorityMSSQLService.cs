namespace WebApiTest.Services;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using WebApiTest.Interfaces;
using WebApiTest.Models;
using WebApiTest.Data;
using WebApiTest.Commands;

public class PriorityMSSQLService : IPriorityService
{
    public readonly DataContext _dataContext;

    public PriorityMSSQLService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public Priority Find(int id)
    {
        return _dataContext.Priorities.SingleOrDefault(o => o.Id == id);
    }

    public List<Priority> GetAll()
    {
        return _dataContext.Priorities.ToList<Priority>();
    }

    public void Save(Priority priority)
    {
        if (priority.Id == null || priority.Id == 0)
        {
            //insert
            _dataContext.Priorities.Add(priority);
        }
        else
        {
            //update
            Priority temp = this.Find(priority.Id);
            temp.PriorityName = priority.PriorityName;
        }

        _dataContext.SaveChanges();
    }
}
