using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;
namespace BasketballGUI;

public partial class Teams : ContentPage
{
    public Team SelectedTeam { get; set; }
    public ObservableCollection<Team> MasterList { get; set; }
	public Teams()
	{
        MasterList = new ObservableCollection<Team>();
        InitializeComponent();
        GetTeamsAsync();
        BindingContext = this;
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
                    foreach(Team team in TeamList)
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
    private async void btnNewTeam_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateTeam());
    }

    private async void btnViewStats_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ViewStats());
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
    public Color originalColor = Colors.Red;
    public Color clickColor = Colors.White;

    private async void btnAddPlayers_Clicked(object sender, EventArgs e)
    {
        if (SelectedTeam != null)
        {
            var teamId = SelectedTeam.Id;
            // Use teamId as needed, e.g., passing to another page or making an API call
            Debug.WriteLine($"Using selected Team ID: {teamId}");
            await Navigation.PushAsync(new PlayerPage(teamId)); // Assuming you modify PlayerPage to accept an ID
        }
    }

    private async void btnSchedule_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TeamSchedule(homePicker.SelectedIndex));
    }

    private async void btnBack_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainMenu());
    }
}