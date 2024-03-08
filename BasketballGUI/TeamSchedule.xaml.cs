using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BasketballGUI;

public partial class TeamSchedule : ContentPage
{
    public ObservableCollection<Schedule> MasterList { get; set; }
    Schedule SelectedGame;
    public TeamSchedule(int id)
	{
		InitializeComponent();
        MasterList = new ObservableCollection<Schedule>();
        GetGamesAsync();
        BindingContext = this;
    }

    async private Task GetGamesAsync()
    {
        MasterList = new ObservableCollection<Schedule>();
        string apiUrl = "https://localhost:7067/api/Schedule";

        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);


                if (response.IsSuccessStatusCode)
                {
                    string jsonString = await response.Content.ReadAsStringAsync();

                    List<Schedule> games = JsonConvert.DeserializeObject<List<Schedule>>(jsonString);

                    foreach (Schedule game in games)
                    {
                           MasterList.Add(game);
                    }

                }
                else
                {
                    Debug.WriteLine("API request failed with status code:" + response.StatusCode);

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);

            }
        }
    }


    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

        if (e.SelectedItem == null)
        {
            return;
        }

        SelectedGame = e.SelectedItem as Schedule;

        await Navigation.PushAsync(new TeamChoice(SelectedGame.Id));
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NewGame());
    }
}