﻿using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BasketballGUI
{
    public partial class LiveEnterStats : ContentPage
    {
        public ObservableCollection<TeamRoster> AwayPlayerList;
        public ObservableCollection<TeamRoster> HomePlayerList;
        public int GameId;
        public Color originalColor = Colors.Red;
        public Color clickColor = Colors.White;

        public LiveEnterStats(int gameId)
        {
            GameId = gameId;
            AwayPlayerList = new ObservableCollection<TeamRoster>();
            HomePlayerList = new ObservableCollection<TeamRoster>();
            InitializeComponent();
        }


        private async Task GetPlayersAsync()
        {
            string apiUrl = "https://localhost:7067/api/TeamRoster/";
            
            using (HttpClient client = new HttpClient())
            {
                Game game = await GetGameAsync(GameId);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl + game.Team1);


                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();

                        List<TeamRoster> roster = JsonConvert.DeserializeObject<List<TeamRoster>>(jsonString);

                        foreach(TeamRoster player in roster)
                        {
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
                    HttpResponseMessage response = await client.GetAsync(apiUrl + game.Team2);


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


        private async Task<Game> GetGameAsync(int gameId)
        {
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

        private void btnPlusOne_Clicked(object sender, EventArgs e)
        {
            
            
        }

        /*private void btnPlusTwo_Clicked(object sender, EventArgs e)
        {
            checkTeam();
            if (awayTeam)
            {
                awayScore += 2;
                lblAwayScore.Text = "" + awayScore;
            }
            else if (homeTeam)
            {
                homeScore += 2;
                lblHomeScore.Text = "" + homeScore;
            }
            clearNames();
        }

        private void btnPlusThree_Clicked(object sender, EventArgs e)
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

        *//*private void btnPeriod_Clicked(object sender, EventArgs e)
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
        }*//*

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

        *//*public void clearNames()
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
        }*//*

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