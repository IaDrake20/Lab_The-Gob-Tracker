namespace BasketballGUI
{
    public partial class LiveEnterStats : ContentPage
    {
        public int awayScore = 0;
        public int homeScore = 0;
        public bool awayTeam = false;
        public bool homeTeam = false;

        public LiveEnterStats()
        {
            InitializeComponent();
        }

        private void btnPlusOne_Clicked(object sender, EventArgs e)
        {
            checkTeam();
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

        }

        private void btnMissedTwo_Clicked(object sender, EventArgs e)
        {

        }

        private void btnMissedThree_Clicked(object sender, EventArgs e)
        {

        }

        private void btnUndo_Clicked(object sender, EventArgs e)
        {

        }

        private void btnAssist_Clicked(object sender, EventArgs e)
        {

        }

        private void btnSteal_Clicked(object sender, EventArgs e)
        {

        }

        private void btnTurnover_Clicked(object sender, EventArgs e)
        {

        }

        private void btnPeriod_Clicked(object sender, EventArgs e)
        {

        }

        private void btnBlock_Clicked(object sender, EventArgs e)
        {

        }

        private void btnOffReb_Clicked(object sender, EventArgs e)
        {

        }

        private void btnDefReb_Clicked(object sender, EventArgs e)
        {

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
    }
}
