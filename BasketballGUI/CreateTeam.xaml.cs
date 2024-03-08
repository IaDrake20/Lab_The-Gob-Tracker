namespace BasketballGUI;

public partial class CreateTeam : ContentPage
{
	public CreateTeam()
	{
		InitializeComponent();
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

    private async void btnAddTeam_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddPlayer());
    }
}