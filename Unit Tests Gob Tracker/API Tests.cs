
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
            game.Id = 9000;
            game.Team1Id = 3;
            game.Team2Id = 4;
            DateTime curr = DateTime.Now;
            game.DateTimeId = curr;

            Assert.Equal("9000", game.Id.ToString());
            Assert.Equal("3", game.Team1Id.ToString());
            Assert.Equal("4", game.Team2Id.ToString());
            Assert.Equal(curr, game.DateTimeId);

        }

        [Fact]
        public void API_Players_GetsSets_Success()
        {
            var player = new Player();
            player.Id = 9000;
            player.Fname = "Ian";
            player.Lname = "Drake";
            

            Assert.Equal("9000", player.Id.ToString());
            Assert.Equal("Ian", player.Fname);
            Assert.Equal("4", player.Lname);
        }

        [Fact]
        public void API_PlayerTeam_GetsSets_Success()
        {
            var team = new PlayerTeam();
            team.Id = 9000;
            team.PlayerId = 86587;
            team.TeamId = -43;
            

            Assert.Equal("9000", team.Id.ToString());
            Assert.Equal("86587", team.PlayerId.ToString());
            Assert.Equal("-43", team.TeamId.ToString());

        }

        [Fact]
        public void API_SeasonStat_GetsSets_Success()
        {
            var stat = new SeasonStat();
            stat.Id = 9000;
            stat.GameId = 86587;
            stat.Total2PtsMade = -43;
            stat.Total3PtsMade = -43;
            stat.Fname = "hi";
            stat.Lname = "gh";


            Assert.Equal("9000", stat.Id.ToString());
            Assert.Equal("86587", stat.GameId.ToString());
            Assert.Equal("-43", stat.Total2PtsMade.ToString());
            Assert.Equal("-43", stat.Total3PtsMade.ToString());
            Assert.Equal("hi", stat.Fname.ToString());
            Assert.Equal("gh", stat.Lname.ToString());
        }

        [Fact]
        public void API_StatType_GetsSets_Success()
        {
            var statT = new StatType();
            statT.Id = -28378;
            statT.Name = "hi";
            statT.Abrv = "something sporty";



            Assert.Equal("-28378", statT.Id.ToString());
            Assert.Equal("hi", statT.Name.ToString());
            Assert.Equal("something sporty", statT.Abrv.ToString());
        }

        [Fact]
        public void API_Teams_GetsSets_Success()
        {
            //TODO
        }
    }
}