using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BasketballGUI;

public partial class ViewGame : ContentPage
{
    public ObservableCollection<Schedule> MasterList { get; set; }
    public ViewGame()
	{
		InitializeComponent();
        MasterList = new ObservableCollection<Schedule>();
        GetGamesAsync();
        BindingContext = this;
	}
   /* private async void btnViewGame_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LiveGame());
    }*/


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
                    var games = JsonConvert.DeserializeObject<List<Schedule>>(jsonString);

                    MasterList.Clear();
                    foreach (var game in games)
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
}