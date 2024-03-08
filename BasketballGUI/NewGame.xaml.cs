using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BasketballGUI;

public partial class NewGame : ContentPage
{
    public Color originalColor = Colors.Red;
    public Color clickColor = Colors.White;
    public Team SelectedTeam { get; set; }
    public ObservableCollection<Team> MasterList { get; set; }
    public int GameId;
    public int T1Id;
    public int T2Id;
    public NewGame()
	{
        MasterList = new ObservableCollection<Team>();
        InitializeComponent();
        GetTeamsAsync();
        BindingContext = this;
    }

    /*private async void btnStartScoring_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LiveEnterStats());
    }*/

    private async void btnNewTeam_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateTeam());
    }

    private void btnPressed(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button != null)
        {
            originalColor = button.BackgroundColor;
            button.BackgroundColor = clickColor;
        }
    }

    private void btnReleased(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button != null)
        {
            button.BackgroundColor = originalColor;
        }
    }

    private async Task GetTeamsAsync()
    {

        string apiUrl = "https://localhost:7067/api/Teams";

        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);


                if (response.IsSuccessStatusCode)
                {
                    string jsonString = await response.Content.ReadAsStringAsync();

                    List<Team> TeamList = JsonConvert.DeserializeObject<List<Team>>(jsonString);

                    MasterList.Clear();
                    foreach (Team team in TeamList)
                    {
                        MasterList.Add(team);
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

    private void btnSaveForLater_Clicked(object sender, EventArgs e)
    {

    }
}