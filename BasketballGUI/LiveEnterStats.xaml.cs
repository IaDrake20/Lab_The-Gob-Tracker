using API_Gob_Tracker.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace BasketballGUI
{
    public partial class LiveEnterStats : ContentPage
    {
        public ObservableCollection<TeamRoster> AwayPlayerList { get; set; }
        public ObservableCollection<TeamRoster> HomePlayerList { get; set; }
        public int GameId;
        public int T1Id;
        public int T2Id;
        PlayerTeam SelectedPlayer;
        public Color originalColor = Colors.Red;
        public Color clickColor = Colors.White;

        public LiveEnterStats(int gameId)
        {
            GameId = gameId;
            AwayPlayerList = new ObservableCollection<TeamRoster>();
            HomePlayerList = new ObservableCollection<TeamRoster>();
            InitializeComponent();
            BindingContext = this;
            GetPlayersAsync();
            GetTeamsAsync();
            KeepScoreAsync();

        }

        private async Task KeepScoreAsync()
        {
            while (true)
            {
                await Task.Delay(10000);

            }
        }


        private async Task GetScoreStatsAsync()
        {
            string apiUrl = "https://localhost:7067/api/ScoringStats";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);


                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();

                        List<ScoringStat> stats = JsonConvert.DeserializeObject<List<ScoringStat>>(jsonString);


                        foreach (ScoringStat stat in stats)
                        {
                            if(stat.GameID == GameId)
                            {
                                if(stat.TeamID == T1Id)
                                {
                                    lblHomeScore.Text = stat.TotalPtsMade.ToString();
                                }
                                else if(stat.TeamID == T2Id)
                                {
                                    lblAwayScore.Text = stat.TotalPtsMade.ToString();
                                }
                            }
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


        private async Task GetPlayersAsync()
        {
            string apiUrl = "https://localhost:7067/api/TeamRoster/";

            using (HttpClient client = new HttpClient())
            {
                Game game = await GetGameAsync(GameId);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl + game.Team1Id);


                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();

                        List<TeamRoster> roster = JsonConvert.DeserializeObject<List<TeamRoster>>(jsonString);


                        foreach (TeamRoster player in roster)
                        {
                            Debug.WriteLine(player.FullName);
                            HomePlayerList.Add(player);
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


                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl + game.Team2Id);


                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();

                        List<TeamRoster> roster = JsonConvert.DeserializeObject<List<TeamRoster>>(jsonString);

                        foreach (TeamRoster player in roster)
                        {
                            AwayPlayerList.Add(player);
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

        private async Task GetTeamsAsync()
        {




            string apiUrl = "https://localhost:7067/api/Teams/";
            Game game = await GetGameAsync(GameId);
            T1Id = game.Team1Id;
            T2Id = game.Team2Id;
            using (HttpClient client = new HttpClient())
            {

                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl + game.Team1Id);


                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();

                        Team team = JsonConvert.DeserializeObject<Team>(jsonString);
                        lblHomeTeam.Text = team.Name;
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

                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl + game.Team2Id);


                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();

                        Team team = JsonConvert.DeserializeObject<Team>(jsonString);
                        lblAwayTeam.Text = team.Name;
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


        private async Task<Game> GetGameAsync(int gameId)
        {
            Debug.WriteLine(gameId);
            string apiUrl = "https://localhost:7067/api/Games/" + GameId;
            Game game = new Game();
            using (HttpClient client = new HttpClient())
            {

                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);


                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();

                        game = JsonConvert.DeserializeObject<Game>(jsonString);

                        Debug.WriteLine("Team IDs: " + game.Team1Id + "  " + game.Team2Id);
                        return game;

                    }
                    else
                    {
                        Debug.WriteLine("API request failed with status code:" + response.StatusCode);
                        return game;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error: " + ex.Message);
                    return game;
                }
            }
        }

        private async void btnPlusOne_Clicked(object sender, EventArgs e)
        {
            if (homePicker.SelectedIndex == -1 && awayPicker.SelectedIndex == -1)
            {
                return;
            }
            else if (homePicker.SelectedIndex != -1)
            {
                TeamRoster player = homePicker.SelectedItem as TeamRoster;
                Stat NewFreeThrow = new Stat();
                NewFreeThrow.StatValue = 1;
                NewFreeThrow.StatTypeId = 5;
                NewFreeThrow.GameId = GameId;
                NewFreeThrow.PlayerTeamId = player.PlayerTeamID;
                await PostStatAsync(NewFreeThrow);
                homePicker.SelectedItem = null;
                homePicker.SelectedIndex = -1;
            }
            else if(awayPicker.SelectedIndex != -1) 
            {
                TeamRoster player = awayPicker.SelectedItem as TeamRoster;
                Stat NewFreeThrow = new Stat();
                NewFreeThrow.StatValue = 1;
                NewFreeThrow.StatTypeId = 5;
                NewFreeThrow.GameId = GameId;
                NewFreeThrow.PlayerTeamId = player.PlayerTeamID;
                await PostStatAsync(NewFreeThrow);
                awayPicker.SelectedItem = null;
                awayPicker.SelectedIndex = -1;
            }
        }

        private async Task PostStatAsync(Stat stat)
        {
            string apiUrl = "https://localhost:7067/api/Stats";

            using (HttpClient client = new HttpClient())
            {
                try
                {

                    string jsonString = JsonConvert.SerializeObject(stat);
                    HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        Debug.WriteLine("Stat successfully added.");
                    }
                    else
                    {
                        Debug.WriteLine("Failed to add Stat. Status code: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.ToString());
                }
            }

        }

            private async void btnPlusTwo_Clicked(object sender, EventArgs e)
            {
                if (homePicker.SelectedIndex == -1 && awayPicker.SelectedIndex == -1)
                {
                    return;
                }
                else if (homePicker.SelectedIndex != -1)
                {
                    TeamRoster player = homePicker.SelectedItem as TeamRoster;
                    Stat NewTwoPoint = new Stat();
                    NewTwoPoint.StatValue = 1;
                    NewTwoPoint.StatTypeId = 1;
                    NewTwoPoint.GameId = GameId;
                    NewTwoPoint.PlayerTeamId = player.PlayerTeamID;
                    await PostStatAsync(NewTwoPoint);
                    homePicker.SelectedItem = null;
                    homePicker.SelectedIndex = -1;
                }
                else if (awayPicker.SelectedIndex != -1)
                {
                    TeamRoster player = awayPicker.SelectedItem as TeamRoster;
                    Stat NewFreeThrow = new Stat();
                    NewFreeThrow.StatValue = 1;
                    NewFreeThrow.StatTypeId = 5;
                    NewFreeThrow.GameId = GameId;
                    NewFreeThrow.PlayerTeamId = player.PlayerTeamID;
                    await PostStatAsync(NewFreeThrow);
                    awayPicker.SelectedItem = null;
                    awayPicker.SelectedIndex = -1;
                }
            }

            /*private void btnPlusThree_Clicked(object sender, EventArgs e)
            {
                checkTeam();
                if (awayTeam)
                {
                    awayScore += 3;
                    lblAwayScore.Text = "" + awayScore;
                }
                else if (homeTeam)
                {
                    homeScore += 3;
                    lblHomeScore.Text = "" + homeScore;
                }
                clearNames();
            }

            private void btnFoul_Clicked(object sender, EventArgs e)
            {
                checkTeam();

                clearNames();
            }

            private void btnMissedTwo_Clicked(object sender, EventArgs e)
            {
                checkTeam();

                clearNames();
            }

            private void btnMissedThree_Clicked(object sender, EventArgs e)
            {
                checkTeam();

                clearNames();
            }

            private void btnUndo_Clicked(object sender, EventArgs e)
            {
                checkTeam();

                clearNames();
            }

            private void btnAssist_Clicked(object sender, EventArgs e)
            {
                checkTeam();

                clearNames();
            }

            private void btnSteal_Clicked(object sender, EventArgs e)
            {
                checkTeam();

                clearNames();
            }

            private void btnTurnover_Clicked(object sender, EventArgs e)
            {
                checkTeam();

                clearNames();
            }

            private void btnPeriod_Clicked(object sender, EventArgs e)
            {
                checkTeam();
                if (intPeriod == 4)
                {
                    intPeriod = 1;
                }
                else
                {
                    intPeriod++;
                }

                lblPeriod.Text = "Q" + intPeriod;
                clearNames();
            }

            private void btnBlock_Clicked(object sender, EventArgs e)
            {
                checkTeam();

                clearNames();
            }

            private void btnOffReb_Clicked(object sender, EventArgs e)
            {
                checkTeam();

                clearNames();
            }

            private void btnDefReb_Clicked(object sender, EventArgs e)
            {
                checkTeam();

                clearNames();
            }

            public void clearNames()
            {
                homePicker.SelectedIndex = -1;
                awayPicker.SelectedIndex = -1;
                homeTeam = false;
                awayTeam = false;

            }
            public void checkTeam()
            {
                if (homePicker.SelectedIndex > -1)
                {
                    homeTeam = true;
                }

                if (awayPicker.SelectedIndex > -1)
                {
                    awayTeam = true;
                }
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
            }*/

            // private async void selectPlayer()
            // {
            // var playerSelectionPage = new PlayerSelectionPage();
            // await Navigation.PushModalAsync(playerSelectionPage);
            // }
        }
    }

