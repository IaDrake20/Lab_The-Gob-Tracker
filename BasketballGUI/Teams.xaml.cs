using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;
namespace BasketballGUI;

public partial class Teams : ContentPage
{
	public Teams()
	{
		InitializeComponent();
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
        await Navigation.PushAsync(new AddPlayer());
    }

    private async void btnRemovePlayers_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddPlayer());
    }

    private async void btnGameStats_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddPlayer());
    }

    private async void btnTotalStats_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddPlayer());
    }

    private async void btnSchedule_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddPlayer());
    }
}