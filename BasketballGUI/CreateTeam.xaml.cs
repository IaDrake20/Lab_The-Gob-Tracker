using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
//using static Java.Util.Jar.Attributes;
namespace BasketballGUI;

public partial class CreateTeam : ContentPage
{
	public CreateTeam()
	{
		InitializeComponent();
	}

    public ObservableCollection<Teams> MasterList { get; set; }

    async private Task postTeamAsync(int r, string n)
    {
        string apiURL = "https://localhost:7067/swagger/index.html";
        using (HttpClient client = new HttpClient())
        {
            try
            {
                var team = new { Ranking = r, Name = n }; 
                string jsonString = JsonConvert.SerializeObject(team);
                HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiURL, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Team successfully added.");
                }
                else
                {
                    Console.WriteLine("Failed to add team. Status code: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
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
        await Navigation.PushAsync(new AddPlayer());
    }

    private async void btnFinished_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}