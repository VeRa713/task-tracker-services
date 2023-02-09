namespace WebApiTest.Interfaces;

using WebApiTest.Models;

public interface IPriorityService
{
    public List<Priority> GetAll();
    public void Save(Priority priority);
    public Priority Find(int id);
}
