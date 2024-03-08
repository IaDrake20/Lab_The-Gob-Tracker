using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BasketballGUI;

public partial class PlayerPage : ContentPage
{
    public ObservableCollection<TeamRoster> MasterList { get; set; }
    public TeamRoster SelectedPlayer { get; set; }
    public PlayerPage(int id)
    {
        InitializeComponent();
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
                        MasterList.Add(player);
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

        SelectedPlayer = e.SelectedItem as TeamRoster;

        await Navigation.PushAsync(new TeamChoice(SelectedPlayer.Id));
    }
}