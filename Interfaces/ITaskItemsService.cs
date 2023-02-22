namespace WebApiTest.Interfaces;

using WebApiTest.Models;

public interface ITaskItemsService
{
    public List<TaskItem> GetAll();
    public void Save(TaskItem hash);
    public TaskItem Find(int id);
    public List<TaskItem> FindByUser(int userId);
    public void Delete(int id);
    public int[] CountByStatus();
}