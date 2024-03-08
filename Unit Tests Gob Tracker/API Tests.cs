
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
    }
}