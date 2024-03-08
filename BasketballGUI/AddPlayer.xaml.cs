using Newtonsoft.Json;
using System.Diagnostics;
using Windows.Networking;

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
        var firstName = firstNameEntry.Text; // Capture first name
        var lastName = lastNameEntry.Text; // Capture last name
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
        {
            // Notify the user to enter both names
            await DisplayAlert("Validation", "Please enter both first and last names.", "OK");
            return;
        }
        // Assuming your API requires a full name, you can concatenate them or adjust as needed
        var fullName = $"{firstName} {lastName}";

        await AddPlayerToTeam(teamId, fullName);

        // Clear the entry after adding
        firstNameEntry.Text = string.Empty;
        lastNameEntry.Text = string.Empty;
    }

    private async Task AddPlayerToTeam(int teamId, string playerName)
    {
        // Implementation depends on how you're storing and managing data
        // For example, sending a POST request to a REST API
        var newPlayer = new { FullName = playerName, TeamId = teamId };
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