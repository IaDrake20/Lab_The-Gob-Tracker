using System.Runtime.CompilerServices;

namespace BasketballGUI;

public partial class MainMenu : ContentPage
{
    IConnectivity connectivity;

    public MainMenu(IConnectivity connectivity)
	{
		InitializeComponent();
        this.connectivity = connectivity;
	}

    private void btnQuit_Clicked(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }

    async private void btnNewGame_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NewGame());
    }

    async private void btnTeams_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Teams(this.connectivity));
    }

    private async void btnViewGame_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ViewGame());
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
}