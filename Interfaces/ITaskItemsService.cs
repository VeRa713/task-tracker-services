namespace WebApiTest.Interfaces;

using WebApiTest.Models;

public interface ITaskItemsService
{
    public List<TaskItem> GetAll();
    public void Save(TaskItem hash);
    public TaskItem Find(int id);
    public void Delete(int id);
}