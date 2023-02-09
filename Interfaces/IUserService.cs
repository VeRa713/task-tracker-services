namespace WebApiTest.Interfaces;

using WebApiTest.Models;

public interface IUserService
{
    public List<User> GetAll();
    public void Save(User user);
    public User Find(int id);
}
