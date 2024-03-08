namespace BasketballGUI;

public partial class AddPlayer : ContentPage
{
	public AddPlayer()
	{
		InitializeComponent();
	}

    private void btnAddPlayer_Clicked(object sender, EventArgs e)
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
    public Color originalColor = Colors.Red;
    public Color clickColor = Colors.White;

    private async void btnFinish_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}