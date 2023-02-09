namespace WebApiTest.Services;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using WebApiTest.Interfaces;
using WebApiTest.Models;
using WebApiTest.Data;
using WebApiTest.Commands;

public class UserMSSQLService : IUserService
{
    public readonly DataContext _dataContext;

    public UserMSSQLService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public User Find(int id)
    {
        return _dataContext.Users.SingleOrDefault(o => o.Id == id);
    }

    public List<User> GetAll()
    {
        return _dataContext.Users.ToList<User>();
    }

    public void Save(User user)
    {
        if (user.Id == null || user.Id == 0)
        {
            //insert
            _dataContext.Users.Add(user);
        }
        else
        {
            //update
            User temp = this.Find(user.Id);
            
            temp.FirstName = user.FirstName;
            temp.LastName = user.LastName;
            temp.Email = user.Email;
        }

        _dataContext.SaveChanges();
    }
}
