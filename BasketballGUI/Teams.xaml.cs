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

    private void btnViewStats_Clicked(object sender, EventArgs e)
    {

    }
}