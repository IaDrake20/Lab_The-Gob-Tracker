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
                await GetScoreStatsAsync();
            }
        }


        private async Task GetScoreStatsAsync()
        {
            string apiUrl = "https://localhost:7067/api/ScoringStat";

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
                            if (stat.GameID == GameId)
                            {
                                if (stat.TeamID == T1Id)
                                {
                                    Debug.WriteLine("Updating home score");
                                    lblHomeScore.Text = stat.TotalPtsMade.ToString();
                                }
                                else if (stat.TeamID == T2Id)
                                {
                                    Debug.WriteLine("Updating away score");
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
                        Debug.WriteLine("success");
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
                        Debug.WriteLine("awsdfasdfasdf with status code:" + response.StatusCode);
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
                        Debug.WriteLine("satus code:" + response.StatusCode);
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
                Stat NewTwoPoint = new Stat();
                NewTwoPoint.StatValue = 1;
                NewTwoPoint.StatTypeId = 1;
                NewTwoPoint.GameId = GameId;
                NewTwoPoint.PlayerTeamId = player.PlayerTeamID;
                await PostStatAsync(NewTwoPoint);
                awayPicker.SelectedItem = null;
                awayPicker.SelectedIndex = -1;
            }
        }

        private async void btnPlusThree_Clicked(object sender, EventArgs e)
        {
            if (homePicker.SelectedIndex == -1 && awayPicker.SelectedIndex == -1)
            {
                return;
            }
            else if (homePicker.SelectedIndex != -1)
            {
                TeamRoster player = homePicker.SelectedItem as TeamRoster;
                Stat NewThreePoint = new Stat();
                NewThreePoint.StatValue = 1;
                NewThreePoint.StatTypeId = 2;
                NewThreePoint.GameId = GameId;
                NewThreePoint.PlayerTeamId = player.PlayerTeamID;
                await PostStatAsync(NewThreePoint);
                homePicker.SelectedItem = null;
                homePicker.SelectedIndex = -1;
            }
            else if (awayPicker.SelectedIndex != -1)
            {
                TeamRoster player = awayPicker.SelectedItem as TeamRoster;
                Stat NewThreePoint = new Stat();
                NewThreePoint.StatValue = 1;
                NewThreePoint.StatTypeId = 2;
                NewThreePoint.GameId = GameId;
                NewThreePoint.PlayerTeamId = player.PlayerTeamID;
                await PostStatAsync(NewThreePoint);
                awayPicker.SelectedItem = null;
                awayPicker.SelectedIndex = -1;
            }
        }

        private async void btnFoul_Clicked(object sender, EventArgs e)
        {
            if (homePicker.SelectedIndex == -1 && awayPicker.SelectedIndex == -1)
            {
                return;
            }
            else if (homePicker.SelectedIndex != -1)
            {
                TeamRoster player = homePicker.SelectedItem as TeamRoster;
                Stat NewFoul = new Stat();
                NewFoul.StatValue = 1;
                NewFoul.StatTypeId = 1;
                NewFoul.GameId = GameId;
                NewFoul.PlayerTeamId = player.PlayerTeamID;
                await PostStatAsync(NewFoul);
                homePicker.SelectedItem = null;
                homePicker.SelectedIndex = -1;
            }
            else if (awayPicker.SelectedIndex != -1)
            {
                TeamRoster player = awayPicker.SelectedItem as TeamRoster;
                Stat NewFoul = new Stat();
                NewFoul.StatValue = 1;
                NewFoul.StatTypeId = 11;
                NewFoul.GameId = GameId;
                NewFoul.PlayerTeamId = player.PlayerTeamID;
                await PostStatAsync(NewFoul);
                awayPicker.SelectedItem = null;
                awayPicker.SelectedIndex = -1;
            }
        }

         private async void btnMissedTwo_Clicked(object sender, EventArgs e)
        {
            if (homePicker.SelectedIndex == -1 && awayPicker.SelectedIndex == -1)
            {
                return;
            }
            else if (homePicker.SelectedIndex != -1)
            {
                TeamRoster player = homePicker.SelectedItem as TeamRoster;
                Stat TwoMissed = new Stat();
                TwoMissed.StatValue = 1;
                TwoMissed.StatTypeId = 3;
                TwoMissed.GameId = GameId;
                TwoMissed.PlayerTeamId = player.PlayerTeamID;
                await PostStatAsync(TwoMissed);
                homePicker.SelectedItem = null;
                homePicker.SelectedIndex = -1;
            }
            else if (awayPicker.SelectedIndex != -1)
            {
                TeamRoster player = awayPicker.SelectedItem as TeamRoster;
                Stat TwoMissed = new Stat();
                TwoMissed.StatValue = 1;
                TwoMissed.StatTypeId = 3;
                TwoMissed.GameId = GameId;
                TwoMissed.PlayerTeamId = player.PlayerTeamID;
                await PostStatAsync(TwoMissed);
                awayPicker.SelectedItem = null;
                awayPicker.SelectedIndex = -1;
            }
        }

        private async void btnMissedThree_Clicked(object sender, EventArgs e)
        {
            if (homePicker.SelectedIndex == -1 && awayPicker.SelectedIndex == -1)
            {
                return;
            }
            else if (homePicker.SelectedIndex != -1)
            {
                TeamRoster player = homePicker.SelectedItem as TeamRoster;
                Stat ThreeMissed = new Stat();
                ThreeMissed.StatValue = 1;
                ThreeMissed.StatTypeId = 4;
                ThreeMissed.GameId = GameId;
                ThreeMissed.PlayerTeamId = player.PlayerTeamID;
                await PostStatAsync(ThreeMissed);
                homePicker.SelectedItem = null;
                homePicker.SelectedIndex = -1;
            }
            else if (awayPicker.SelectedIndex != -1)
            {
                TeamRoster player = awayPicker.SelectedItem as TeamRoster;
                Stat ThreeMissed = new Stat();
                ThreeMissed.StatValue = 1;
                ThreeMissed.StatTypeId = 4;
                ThreeMissed.GameId = GameId;
                ThreeMissed.PlayerTeamId = player.PlayerTeamID;
                await PostStatAsync(ThreeMissed);
                awayPicker.SelectedItem = null;
                awayPicker.SelectedIndex = -1;
            }
        }

        /*private void btnUndo_Clicked(object sender, EventArgs e)
        {
            checkTeam();

            clearNames();
        }*/

        private async void btnAssist_Clicked(object sender, EventArgs e)
        {
            if (homePicker.SelectedIndex == -1 && awayPicker.SelectedIndex == -1)
            {
                return;
            }
            else if (homePicker.SelectedIndex != -1)
            {
                TeamRoster player = homePicker.SelectedItem as TeamRoster;
                Stat Assist = new Stat();
                Assist.StatValue = 1;
                Assist.StatTypeId = 10;
                Assist.GameId = GameId;
                Assist.PlayerTeamId = player.PlayerTeamID;
                await PostStatAsync(Assist);
                homePicker.SelectedItem = null;
                homePicker.SelectedIndex = -1;
            }
            else if (awayPicker.SelectedIndex != -1)
            {
                TeamRoster player = awayPicker.SelectedItem as TeamRoster;
                Stat Assist = new Stat();
                Assist.StatValue = 1;
                Assist.StatTypeId = 10;
                Assist.GameId = GameId;
                Assist.PlayerTeamId = player.PlayerTeamID;
                await PostStatAsync(Assist);
                awayPicker.SelectedItem = null;
                awayPicker.SelectedIndex = -1;
            }
        }

        private async void btnSteal_Clicked(object sender, EventArgs e)
        {
            if (homePicker.SelectedIndex == -1 && awayPicker.SelectedIndex == -1)
            {
                return;
            }
            else if (homePicker.SelectedIndex != -1)
            {
                TeamRoster player = homePicker.SelectedItem as TeamRoster;
                Stat Steal = new Stat();
                Steal.StatValue = 1;
                Steal.StatTypeId = 7;
                Steal.GameId = GameId;
                Steal.PlayerTeamId = player.PlayerTeamID;
                await PostStatAsync(Steal);
                homePicker.SelectedItem = null;
                homePicker.SelectedIndex = -1;
            }
            else if (awayPicker.SelectedIndex != -1)
            {
                TeamRoster player = awayPicker.SelectedItem as TeamRoster;
                Stat Steal = new Stat();
                Steal.StatValue = 1;
                Steal.StatTypeId = 7;
                Steal.GameId = GameId;
                Steal.PlayerTeamId = player.PlayerTeamID;
                await PostStatAsync(Steal);
                awayPicker.SelectedItem = null;
                awayPicker.SelectedIndex = -1;
            }
        }

        private async void btnTurnover_Clicked(object sender, EventArgs e)
        {
            if (homePicker.SelectedIndex == -1 && awayPicker.SelectedIndex == -1)
            {
                return;
            }
            else if (homePicker.SelectedIndex != -1)
            {
                TeamRoster player = homePicker.SelectedItem as TeamRoster;
                Stat to = new Stat();
                to.StatValue = 1;
                to.StatTypeId = 8;
                to.GameId = GameId;
                to.PlayerTeamId = player.PlayerTeamID;
                await PostStatAsync(to);
                homePicker.SelectedItem = null;
                homePicker.SelectedIndex = -1;
            }
            else if (awayPicker.SelectedIndex != -1)
            {
                TeamRoster player = awayPicker.SelectedItem as TeamRoster;
                Stat to = new Stat();
                to.StatValue = 1;
                to.StatTypeId = 8;
                to.GameId = GameId;
                to.PlayerTeamId = player.PlayerTeamID;
                await PostStatAsync(to);
                awayPicker.SelectedItem = null;
                awayPicker.SelectedIndex = -1;
            }
        }

        /*private void btnPeriod_Clicked(object sender, EventArgs e)
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
        }*/

        private async void btnBlock_Clicked(object sender, EventArgs e)
        {
            if (homePicker.SelectedIndex == -1 && awayPicker.SelectedIndex == -1)
            {
                return;
            }
            else if (homePicker.SelectedIndex != -1)
            {
                TeamRoster player = homePicker.SelectedItem as TeamRoster;
                Stat block = new Stat();
                block.StatValue = 1;
                block.StatTypeId = 9;
                block.GameId = GameId;
                block.PlayerTeamId = player.PlayerTeamID;
                await PostStatAsync(block);
                homePicker.SelectedItem = null;
                homePicker.SelectedIndex = -1;
            }
            else if (awayPicker.SelectedIndex != -1)
            {
                TeamRoster player = awayPicker.SelectedItem as TeamRoster;
                Stat block = new Stat();
                block.StatValue = 1;
                block.StatTypeId = 9;
                block.GameId = GameId;
                block.PlayerTeamId = player.PlayerTeamID;
                await PostStatAsync(block);
                awayPicker.SelectedItem = null;
                awayPicker.SelectedIndex = -1;
            }
        }

        private async void btnOffReb_Clicked(object sender, EventArgs e)
        {
            if (homePicker.SelectedIndex == -1 && awayPicker.SelectedIndex == -1)
            {
                return;
            }
            else if (homePicker.SelectedIndex != -1)
            {
                TeamRoster player = homePicker.SelectedItem as TeamRoster;
                Stat oreb = new Stat();
                oreb.StatValue = 1;
                oreb.StatTypeId = 12;
                oreb.GameId = GameId;
                oreb.PlayerTeamId = player.PlayerTeamID;
                await PostStatAsync(oreb);
                homePicker.SelectedItem = null;
                homePicker.SelectedIndex = -1;
            }
            else if (awayPicker.SelectedIndex != -1)
            {
                TeamRoster player = awayPicker.SelectedItem as TeamRoster;
                Stat oreb = new Stat();
                oreb.StatValue = 1;
                oreb.StatTypeId = 12;
                oreb.GameId = GameId;
                oreb.PlayerTeamId = player.PlayerTeamID;
                await PostStatAsync(oreb);
                awayPicker.SelectedItem = null;
                awayPicker.SelectedIndex = -1;
            }
        }

        private async void btnDefReb_Clicked(object sender, EventArgs e)
        {
            if (homePicker.SelectedIndex == -1 && awayPicker.SelectedIndex == -1)
            {
                return;
            }
            else if (homePicker.SelectedIndex != -1)
            {
                TeamRoster player = homePicker.SelectedItem as TeamRoster;
                Stat dreb = new Stat();
                dreb.StatValue = 1;
                dreb.StatTypeId = 12;
                dreb.GameId = GameId;
                dreb.PlayerTeamId = player.PlayerTeamID;
                await PostStatAsync(dreb);
                homePicker.SelectedItem = null;
                homePicker.SelectedIndex = -1;
            }
            else if (awayPicker.SelectedIndex != -1)
            {
                TeamRoster player = awayPicker.SelectedItem as TeamRoster;
                Stat dreb = new Stat();
                dreb.StatValue = 1;
                dreb.StatTypeId = 12;
                dreb.GameId = GameId;
                dreb.PlayerTeamId = player.PlayerTeamID;
                await PostStatAsync(dreb);
                awayPicker.SelectedItem = null;
                awayPicker.SelectedIndex = -1;
            }
        }
    }
}

