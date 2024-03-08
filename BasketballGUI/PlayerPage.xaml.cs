using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BasketballGUI;

public partial class PlayerPage : ContentPage
{
    public ObservableCollection<TeamRoster> MasterList { get; set; }
    public TeamRoster SelectedPlayer { get; set; }
    public int teamId;
    public PlayerPage(int myId)
    {
        InitializeComponent();
        teamId = myId;
        MasterList = new ObservableCollection<TeamRoster>();
        GetGamesAsync();
        BindingContext = this;
    }

    async private Task GetGamesAsync()
    {
        MasterList = new ObservableCollection<TeamRoster>();
        string apiUrl = "https://localhost:7067/api/TeamRoster";

        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);


                if (response.IsSuccessStatusCode)
                {
                    string jsonString = await response.Content.ReadAsStringAsync();

                    List<TeamRoster> games = JsonConvert.DeserializeObject<List<TeamRoster>>(jsonString);

                    foreach (TeamRoster player in games)
                    {
                        if (player.TeamID == teamId)
                        {
                            MasterList.Add(player);
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


    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null)
        {
            return;
        }

        var selectedPlayer = e.SelectedItem as TeamRoster;
        if (selectedPlayer != null)
        {
            // Assuming TeamRoster has Id and FullName properties
            await Navigation.PushAsync(new PlayerStats(selectedPlayer.TeamID, selectedPlayer.FullName));
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddPlayer(teamId));
    }
}