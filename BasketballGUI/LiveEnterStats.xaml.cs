namespace BasketballGUI
{
    public partial class LiveEnterStats : ContentPage
    {
        public int awayScore = 0;
        public int homeScore = 0;

        public bool awayTeam = false;
        public bool homeTeam = false;

        public int intPeriod = 1;

        public Color originalColor = Colors.Red;
        public Color clickColor = Colors.White;

        public LiveEnterStats()
        {
            InitializeComponent();
        }

        private void btnPlusOne_Clicked(object sender, EventArgs e)
        {
            selectPlayer();
            if (awayTeam) 
            {
                awayScore++;
                lblAwayScore.Text = "" + awayScore;
            }
            else if (homeTeam)
            {
                homeScore++;
                lblHomeScore.Text = "" + homeScore;
            }
            clearNames();
        }

        private void btnPlusTwo_Clicked(object sender, EventArgs e)
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
                awayScore+= 3;
                lblAwayScore.Text = "" + awayScore;
            }
            else if (homeTeam)
            {
                homeScore+= 3;
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
        }

        private async void selectPlayer()
        {
            var playerSelectionPage = new PlayerSelectionPage();
            await Navigation.PushModalAsync(playerSelectionPage);
        }
    }
}
