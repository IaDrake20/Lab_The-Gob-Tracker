using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;


namespace BasketballGUI;

public partial class LiveGame : ContentPage
{
	public LiveGame(int gameID)
	{
        Debug.WriteLine(gameID);
        InitializeComponent();
        GetGameAsync(gameID);
        BindingContext = this;
	}


    private async Task GetGameAsync(int id)
    {
        Game game;

        string apiUrl = "https://localhost:7067/api/Games/";

        apiUrl += id.ToString();

        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);


                if (response.IsSuccessStatusCode)
                {
                    string jsonString = await response.Content.ReadAsStringAsync();
                    game = JsonConvert.DeserializeObject<Game>(jsonString);

                    List<Team> teams = await GetTeamsAsync();

                    foreach (Team team in teams)
                    {
                        if(team.Id == game.Team1Id)
                        {
                            HTeam.Text = team.Name;
                        }
                        else if(team.Id == game.Team2Id)
                        {
                            ATeam.Text = team.Name;
                        }
                    }
                }
                else
                {
                    Debug.WriteLine("API request failed with status code:" + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);

            }
        }
    }


    private async Task<List<Team>> GetTeamsAsync()
    {
        string apiUrl = "https://localhost:7067/api/Teams";
        List<Team> team = new List<Team>();
        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);


                if (response.IsSuccessStatusCode)
                {
                    string jsonString = await response.Content.ReadAsStringAsync();
                    team = JsonConvert.DeserializeObject<List<Team>>(jsonString);

                    return team;
                }
                else
                {
                    Debug.WriteLine("API request failed with status code:" + response.StatusCode);
                    return team;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                return team;
            }
        }
    }
}
