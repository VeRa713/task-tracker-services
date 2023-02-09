using System.Text.RegularExpressions;

namespace WebApiTest.Operations;

public class ValidateSaveTaskItem
{
    private Dictionary<string, object> payload;
    public Dictionary<string, List<string>> Errors { get; private set; }

    public ValidateSaveTaskItem(Dictionary<string, object> payload)
    {
        this.payload = payload;

        this.Errors = new Dictionary<string, List<string>>();

        Errors.Add("id", new List<string>());
        Errors.Add("task_name", new List<string>());
        Errors.Add("status", new List<string>());
        Errors.Add("priority_id", new List<string>());
        Errors.Add("desc", new List<string>());
        Errors.Add("user_id", new List<string>());
    }

    public bool HasErrors()
    {
        bool ans = false;

        if (Errors["task_name"].Count > 0)
        {
            ans = true;
        }

        if (Errors["status"].Count > 0)
        {
            ans = true;
        }

        if (Errors["priority_id"].Count > 0)
        {
            ans = true;
        }

        if (Errors["desc"].Count > 0)
        {
            ans = true;
        }

        if (Errors["user_id"].Count > 0)
        {
            ans = true;
        }

        return ans;
    }

    public bool HasNoErrors()
    {
        return !HasErrors();
    }

    public void Execute()
    {
        //Task Name validation
        //check if not an empty string
        if (!payload.ContainsKey("task_name"))
        {
            Errors["task_name"].Add("Task Name is required");
        }

        //Status validation
        //check if not an empty string
        //check data type
        if (!payload.ContainsKey("status"))
        {
            Errors["status"].Add("Status is required");
        }

        //Priority validation
        //check if not an empty string 
        //check if within range (1-3)
        //check data type
        if (!payload.ContainsKey("priority_id"))
        {
            Errors["priority_id"].Add("Priority is required");
        }

        //check data type
        //check if existing id (once team model is created)
        if (!payload.ContainsKey("user_id"))
        {
            Errors["user_id"].Add("User Id is required");
        }

        // if (!payload.ContainsKey("deadline"))
        // {
        //     Errors["deadline"].Add("Deadline is required");
        // }
        // else
        // {
        //     //check format
        //     // Define a regular expression for repeated words.

        //     //I should transfer this somewhere to make it reusable....
        //     Regex rx = new Regex(@"^[1-2][0-9]{3}-((0[1-9])|(1[0-2]))-((0[1-9])|([1-2][0-9])|(3[0-1]))$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        //     // Define a test string.
        //     string deadline = payload["deadline"].ToString();
        //     Console.WriteLine("Deadline: " + deadline);

        //     // Find matches.
        //     MatchCollection matches = rx.Matches(deadline);

        //     // Report the number of matches found.
        //     Console.WriteLine("{0} matches found in:\n   {1}", matches.Count, deadline);

        //     if (matches.Count == 0)
        //     {
        //         Errors["deadline"].Add("Deadline is does not match required format");
        //     }

        //     //should not be past today's date
        // }
    }
}
