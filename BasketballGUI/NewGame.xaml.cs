namespace BasketballGUI;

public partial class NewGame : ContentPage
{
    public Color originalColor = Colors.Red;
    public Color clickColor = Colors.White;
    public NewGame()
	{
		InitializeComponent();
	}

    private async void btnStartScoring_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LiveEnterStats());
    }

    private async void btnNewTeam_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateTeam());
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

    private void btnSaveForLater_Clicked(object sender, EventArgs e)
    {

    }
}