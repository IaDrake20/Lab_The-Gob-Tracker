using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;


namespace BasketballGUI;

public partial class LiveGame : ContentPage
{
    public int GameId;
    public int T1Id;
    public int T2Id;

	public LiveGame(int gameID)
	{
        GameId = gameID;
        Debug.WriteLine(gameID);
        InitializeComponent();
        GetGameAsync(gameID);
        BindingContext = this;
        KeepScoreAsync();
    }


    private async Task KeepScoreAsync()
    {
        while (true)
        {
            await Task.Delay(5000);
            await GetScoreStatsAsync();
        }
    }


    private async Task GetScoreStatsAsync()
    {
        string apiUrl = "https://localhost:7067/api/ScoringStat";

        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);


                if (response.IsSuccessStatusCode)
                {
                    string jsonString = await response.Content.ReadAsStringAsync();

                    List<ScoringStat> stats = JsonConvert.DeserializeObject<List<ScoringStat>>(jsonString);


                    foreach (ScoringStat stat in stats)
                    {
                        if (stat.GameID == GameId)
                        {
                            if (stat.TeamID == T1Id)
                            {
                                Debug.WriteLine("Updating home score");
                                lblHomeScore.Text = stat.TotalPtsMade.ToString();
                            }
                            else if (stat.TeamID == T2Id)
                            {
                                Debug.WriteLine("Updating away score");
                                lblAwayScore.Text = stat.TotalPtsMade.ToString();
                            }
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
                    T1Id = game.Team1Id;
                    T2Id = game.Team2Id;
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
