using Newtonsoft.Json;
using System.Security.Policy;


    public class PlayerDTO
    {
    [JsonProperty("FName")]
    public string FirstName { get; set; } = string.Empty;
    [JsonProperty("LName")]
    public string LastName { get; set; } = string.Empty;
    }
