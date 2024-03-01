namespace BasketballGUI;

public partial class MainMenu : ContentPage
{

    public Color originalColor = Colors.Red;
    public Color clickColor = Colors.White;

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
        await Navigation.PushAsync(new NewGame());
    }

    async private void btnTeams_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Teams());
    }

    private void btnViewGame_Clicked(object sender, EventArgs e)
    {

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
}