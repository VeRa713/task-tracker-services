namespace WebApiTest.Commands;

using WebApiTest.Models;
using System.Text.Json;

public class BuildStatusFromDictionary
{
    private Dictionary<string, object> data;

    public BuildStatusFromDictionary(Dictionary<string, object> hash)
    {
        this.data = hash;
    }

    public Status Execute()
    {
        Status newStatus = new Status();

        if(data.ContainsKey("id"))
        {
           newStatus.Id = int.Parse(data["id"].ToString());
        }

        if(data.ContainsKey("status_name"))
        {
            newStatus.StatusName = data["status_name"].ToString();
        }

        return newStatus;
    }
}
