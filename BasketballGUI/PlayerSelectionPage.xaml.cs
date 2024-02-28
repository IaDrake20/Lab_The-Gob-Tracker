using API_Gob_Tracker.Models;

namespace BasketballGUI;
public partial class PlayerSelectionPage : ContentPage
{
    public List<Player> Players { get; set; }
    public PlayerSelectionPage()
    {
        InitializeComponent();

        Players = new List<Player>
        {
            new Player { Fname = "Player 1" },
            new Player { Fname = "Player 2" },
            new Player { Fname = "Player 3" }
        };

        // Bind the list of players to the CollectionView
        PlayersCollectionView.ItemsSource = Players;
        // Initialize your list of players and bind it to the CollectionView
    }

    private void OnPlayerSelected(object sender, SelectionChangedEventArgs e)
    {
        // Handle player selection
        // Optionally, close the modal page after selection
        Navigation.PopModalAsync();
    }
}