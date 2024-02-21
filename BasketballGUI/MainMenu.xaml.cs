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
}