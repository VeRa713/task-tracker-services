namespace WebApiTest.Interfaces;

using WebApiTest.Models;

public interface IStatusService
{
    public List<Status> GetAll();
    public void Save(Status status);
    public Status Find(int id);
}
