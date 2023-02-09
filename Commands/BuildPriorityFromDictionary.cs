namespace WebApiTest.Commands;

using WebApiTest.Models;
using System.Text.Json;

public class BuildPriorityFromDictionary
{
    private Dictionary<string, object> data;

    public BuildPriorityFromDictionary(Dictionary<string, object> hash)
    {
        this.data = hash;
    }

    public Priority Execute()
    {
        Priority newPriority = new Priority();

        if(data.ContainsKey("id"))
        {
           newPriority.Id = int.Parse(data["id"].ToString());
        }

        if(data.ContainsKey("priority_name"))
        {
            newPriority.PriorityName = data["priority_name"].ToString();
        }

        return newPriority;
    }
}
