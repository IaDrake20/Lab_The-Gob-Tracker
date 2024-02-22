namespace BasketballGUI;

public partial class MainMenu : ContentPage
{
	public MainMenu()
	{
		InitializeComponent();
	}

    private void btnQuit_Clicked(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }

    async private void btnNewGame_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LiveEnterStats());
    }

    async private void btnTeams_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Teams());
    }
}