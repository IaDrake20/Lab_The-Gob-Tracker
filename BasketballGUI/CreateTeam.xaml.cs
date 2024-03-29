using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Numerics;
using System.Text;
//using static Java.Util.Jar.Attributes;
namespace BasketballGUI;

public partial class CreateTeam : ContentPage
{
    public ObservableCollection<Team> MasterList { get; set; }
    public CreateTeam()
	{
        MasterList = new ObservableCollection<Team>();
        InitializeComponent();
    }

    async private Task postTeamAsync(Team team)
    {
        string apiURL = "https://localhost:7067/api/Teams";
        using (HttpClient client = new HttpClient())
        {
            try
            {
                string jsonString = JsonConvert.SerializeObject(team);
                HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiURL, content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Team successfully added.");
                    entry.Text = string.Empty;
                }
                else
                {
                    Debug.WriteLine("Failed to add team. Status code: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                    Debug.Write(ex.ToString());
            }
        }
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

    private async void btnAddTeam_Clicked(object sender, EventArgs e)
    {
        Team team = new Team();
        team.Name = entry.Text;
        team.Ranking = 0;
        await postTeamAsync(team);
    }

    private async void btnFinished_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Teams());
    }
}