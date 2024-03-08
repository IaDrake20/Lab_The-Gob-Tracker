namespace BasketballGUI;

public partial class ViewGame : ContentPage
{
	public ViewGame()
	{
		InitializeComponent();
	}
    private async void btnViewGame_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LiveGame());
    }
}