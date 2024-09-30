namespace Szfindel.Models
{
    public class Match
    {
        public int MatchId { get; set; }
        public int AccountUserId {get; set; }
        public int MatchUserId {  get; set; }
        public bool IsMatch { get; set; }

       public AccountUser? AccountUser { get; set; }
    }
}
