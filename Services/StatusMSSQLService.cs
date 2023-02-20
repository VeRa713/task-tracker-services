namespace WebApiTest.Services;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using WebApiTest.Interfaces;
using WebApiTest.Models;
using WebApiTest.Data;
using WebApiTest.Commands;

public class StatusMSSQLService : IStatusService
{
    public readonly DataContext _dataContext;

    public StatusMSSQLService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public Status Find(int id)
    {
        return _dataContext.Statuses.SingleOrDefault(o => o.Id == id);
    }

    public List<Status> GetAll()
    {
        return _dataContext.Statuses.ToList<Status>();
    }

    public void Save(Status status)
    {
        if (status.Id == null || status.Id == 0)
        {
            //insert
            _dataContext.Statuses.Add(status);
        }
        else
        {
            //update
            Status temp = this.Find(status.Id);
            temp.StatusName = status.StatusName;
        }

        _dataContext.SaveChanges();
    }
}
