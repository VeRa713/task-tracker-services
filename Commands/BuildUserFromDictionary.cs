namespace WebApiTest.Commands;

using WebApiTest.Models;
using System.Text.Json;

public class BuildUserFromDictionary
{
    private Dictionary<string, object> data;

    public BuildUserFromDictionary(Dictionary<string, object> hash)
    {
        this.data = hash;
    }

    public User Execute()
    {
        User newUser = new User();

        if (data.ContainsKey("id"))
        {
            newUser.Id = int.Parse(data["id"].ToString());
        }

        if (data.ContainsKey("first_name"))
        {
            newUser.FirstName = data["first_name"].ToString();
        }

        if (data.ContainsKey("last_name"))
        {
            newUser.LastName = data["last_name"].ToString();
        }

        if (data.ContainsKey("email"))
        {
            newUser.Email = data["email"].ToString();
        }

        return newUser;
    }
}
