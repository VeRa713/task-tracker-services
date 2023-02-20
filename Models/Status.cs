namespace WebApiTest.Models;

public class Status
{
    public int Id { get; set; }
    public String StatusName { get; set; }
    public List<TaskItem> TaskItems { get; set; }
}