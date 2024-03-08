using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace BasketballGUI
{
    public partial class AddPlayer : ContentPage
    {
        private int teamId;
        public int PlayerId { get; set; }

        public AddPlayer(int teamID)
        {
            teamId = teamID;
            InitializeComponent();
        }

        private async void btnAddPlayer_Clicked(object sender, EventArgs e)
        {
            var firstName = firstNameEntry.Text;
            var lastName = lastNameEntry.Text;

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                await DisplayAlert("Validation", "Please enter both first and last names.", "OK");
                return;
            }

            int playerId = await MakePlayer(firstName, lastName);
            if (playerId != -1)
            {
                bool success = await AddPlayerToTeam(teamId, playerId);

                if (success)
                {
                    Debug.WriteLine("Player added to team successfully");
                    await DisplayAlert("Success", "Player added to team successfully", "OK");
                }
                else
                {
                    Debug.WriteLine("Failed to add player to team.");
                    await DisplayAlert("Error", "Failed to add player to team.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Failed to create player.", "OK");
            }

            // Clear the entry after adding
            firstNameEntry.Text = string.Empty;
            lastNameEntry.Text = string.Empty;
        }

        private async Task<int> MakePlayer(string firstName, string lastName)
        {
            string apiUrl = "https://localhost:7067/api/Players";
            var newPlayer = new PlayerDTO { FirstName = firstName, LastName = lastName };
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(newPlayer);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    // Assuming the API returns just the PlayerId as an integer within the response content
                    try
                    {
                        var playerResponse = JsonConvert.DeserializeObject<PlayerResponseDTO>(responseContent);
                        return playerResponse.PlayerId; // Return the playerId from the deserialized object
                    }
                    catch (JsonException ex)
                    {
                        Debug.WriteLine($"JSON parsing error: {ex.Message}");
                        return -1; // Indicate failure due to parsing error
                    }
                }
                else
                {
                    Debug.WriteLine($"Failed to create player. Status code: {response.StatusCode}");
                    return -1; // Indicate failure due to non-success status code
                }
            }
        }

        private async Task<bool> AddPlayerToTeam(int teamId, int playerId)
        {
            string apiUrl = "https://localhost:7067/api/PlayerTeams";
            var newPlayerTeam = new { TeamId = teamId, PlayerId = playerId };
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(newPlayerTeam);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(apiUrl, content);
                return response.IsSuccessStatusCode;
            }
        }
        private async void btnFinish_Clicked(object sender, EventArgs e)
        {
            // Navigate back to the previous page
            await Navigation.PopAsync();
        }
    }
}
