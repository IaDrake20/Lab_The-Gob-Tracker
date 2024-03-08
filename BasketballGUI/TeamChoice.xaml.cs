namespace BasketballGUI;

public partial class TeamChoice : ContentPage
{
    public int myId;

    public TeamChoice(int gameID)
	{
        myId = gameID;
		InitializeComponent();
	}

    private async void ViewButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LiveGame(myId));
}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LiveEnterStats(myId));
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
