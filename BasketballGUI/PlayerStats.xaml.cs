using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace BasketballGUI;

public partial class PlayerStats : ContentPage
{
    public PlayerStats(int playerId, string playerName)
    {
        InitializeComponent();
        playerNameLabel.Text = playerName;
        FetchAndDisplayPlayerStats(playerId);
    }

    private void FetchAndDisplayPlayerStats(int playerId)
    {
        // Fetch player stats based on playerId. This is just a placeholder.
        // You might fetch data from an API, a database, etc.
        // For demonstration, let's assume we add static data:
        var playerStats = new ObservableCollection<string>
        {
            "Points per Game: 25",
            "Rebounds per Game: 11",
            // Add more stats as needed
        };

        foreach (var stat in playerStats)
        {
            statsStackLayout.Children.Add(new Label { Text = stat });
        }
    }
}
