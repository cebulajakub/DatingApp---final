using Szfindel.Models;


namespace Szfindel.Interface
{
    public interface IMatch
    {
        Match CheckMutualLike(int userId, int matchedUserId);
        void AddLike(Match match);
        void UpdateMatch(int userId, int matchedUserId);
        void DeleteUsersMatchesByID(int userId);
        IEnumerable<Match> GetAllMatch();
        bool Ismatch(int userId, int matchedUserId);
        void Delete(Match match);
        ICollection<int> OurMatch(int userId);
        Match IsmatchReturnMatch(int userId, int matchedUserId);
    }
}
