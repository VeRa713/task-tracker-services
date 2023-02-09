namespace WebApiTest.Models;

public class Priority
{
    public int Id { get; set; }
    public String PriorityName { get; set; }
    public List<TaskItem> TaskItems { get; set; }
}