
using API_Gob_Tracker;
using API_Gob_Tracker.Models;
namespace Unit_Tests_Gob_Tracker
{
    public class API_Tests
    {
        [Fact]
        public void API_Game_GetsSets_Success()
        {
            var game = new Game();
            game.Team1Id = 3;
            game.Team2Id = 4;
            DateTime curr = DateTime.Now;
            game.DateTimeId = curr;
            game.Quarter = 3;
            game.Half = 1;

            Assert.Equal("3", game.Team1Id.ToString());
            Assert.Equal("4", game.Team2Id.ToString());
            Assert.Equal(curr, game.DateTimeId);
            Assert.Equal("3", game.Quarter.ToString());
            Assert.Equal("1", game.Half.ToString());

        }

        [Fact]
        public void API_Players_GetsSets_Success()
        {
            var player = new Player();
            player.Fname = "Ian";
            player.Lname = "Drake";
            

            Assert.Equal("Ian", player.Fname);
            Assert.Equal("4", player.Lname);
        }

        [Fact]
        public void API_PlayerTeam_GetsSets_Success()
        {
            var team = new PlayerTeam();
            team.PlayerId = 86587;
            team.TeamId = -43;
            

            Assert.Equal("86587", team.PlayerId.ToString());
            Assert.Equal("-43", team.TeamId.ToString());

        }

        [Fact]
        public void API_SeasonStat_GetsSets_Success()
        {
            var stat = new SeasonStat();
            stat.GameId = 86587;
            stat.Total2PtsMade = -43;
            stat.Total3PtsMade = -43;
            stat.Fname = "hi";
            stat.Lname = "gh";


            Assert.Equal("86587", stat.GameId.ToString());
            Assert.Equal("-43", stat.Total2PtsMade.ToString());
            Assert.Equal("-43", stat.Total3PtsMade.ToString());
            Assert.Equal("hi", stat.Fname.ToString());
            Assert.Equal("gh", stat.Lname.ToString());
        }

        [Fact]
        public void API_StatType_GetsSets_Success()
        {
            var statT = new StatType
            {
                Name = "hi",
                Abrv = "something sporty"
            };

            Assert.Equal("hi", statT.Name.ToString());
            Assert.Equal("something sporty", statT.Abrv.ToString());
        }

        [Fact]
        public void API_Teams_GetsSets_Success()
        {
            var team = new Team();

            team.Name = "Penguins";
            team.Ranking = 1;

            Assert.Equal("Penguins", team.Name);
            Assert.Equal("1", team.Ranking.ToString());
        }
    }
}