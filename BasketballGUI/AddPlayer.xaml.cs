using Newtonsoft.Json;
using System.Diagnostics;

namespace BasketballGUI;

public partial class AddPlayer : ContentPage
{
    private int teamId;
	public AddPlayer(int teamID)
	{
        teamId = teamID;
		InitializeComponent();
	}

    private async void btnAddPlayer_Clicked(object sender, EventArgs e)
    {
        var playerName = entry.Text; // Capture player's name from the Entry control
        if (string.IsNullOrWhiteSpace(playerName))
        {
            // Optionally, notify the user to enter a valid name
            await DisplayAlert("Validation", "Please enter a player's name.", "OK");
            return;
        }

        // Assuming you have a method to add a player to the team
        await AddPlayerToTeam(teamId, playerName);

        // Clear the entry after adding
        entry.Text = string.Empty;
    }

    private async Task AddPlayerToTeam(int teamId, string playerName)
    {
        // Implementation depends on how you're storing and managing data
        // For example, sending a POST request to a REST API
        var newPlayer = new { Name = playerName, TeamId = teamId };
        string apiUrl = "https://localhost:7067/api/PlayerTeams"; // Adjust URL as necessary

        using (var client = new HttpClient())
        {
            var json = JsonConvert.SerializeObject(newPlayer);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync(apiUrl, content);
            if (response.IsSuccessStatusCode)
            {
                // Optionally, notify the user that the player was added successfully
                Debug.WriteLine("Player added successfully");
            }
            else
            {
                // Handle failure
                Debug.WriteLine($"Failed to add player. Status code: {response.StatusCode}");
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

    private async void btnFinish_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}