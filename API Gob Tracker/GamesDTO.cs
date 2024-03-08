namespace API_Gob_Tracker
{
    public class GamesDTO
    {
        public int Team1ID { get; set; } 
        public int Team2ID { get; set; }

        public int Quarter { get; set; }

        public int Half {  get; set; }

        public string date {  get; set; } = string.Empty;
    }
}
