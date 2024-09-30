using Szfindel.Models;
using System.Linq;
using Szfindel.Interface;

namespace Szfindel.Repo
{
    public class MatchRepo : IMatch
    {
        private readonly DatabaseContext _dbContext;

        public MatchRepo(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Match CheckMutualLike(int userId, int matchedUserId)
        {
            // Sprawdź, czy istnieje już wpis w tabeli Matches reprezentujący wzajemne zainteresowanie
            var existingMatch = _dbContext.Matches.FirstOrDefault(m => m.AccountUserId == matchedUserId && m.MatchUserId == userId);

            return existingMatch;
        }
        public bool Ismatch(int userId, int matchedUserId)
        {
            var match = _dbContext.Matches.FirstOrDefault(m => (m.AccountUserId == matchedUserId && m.MatchUserId == userId) || ( m.AccountUserId == userId && m.MatchUserId == matchedUserId));

            if (match != null && match.IsMatch)
            {
                return true;
            }
            else
            {

                return false;
            }
        }

        public Match IsmatchReturnMatch(int userId, int matchedUserId)
        {
            var match = _dbContext.Matches.FirstOrDefault(m => (m.AccountUserId == matchedUserId && m.MatchUserId == userId) || (m.AccountUserId == userId && m.MatchUserId == matchedUserId));
            return match;
        }

        public void AddLike(Match match)
        {
            // Sprawdź, czy istnieje już rekord matcha o podobnych parametrach
            var existingMatch = _dbContext.Matches.FirstOrDefault(m => m.AccountUserId == match.AccountUserId && m.MatchUserId == match.MatchUserId);

            if (existingMatch == null)
            {
                // Jeśli nie istnieje, dodaj nowy rekord do tabeli Matches
                _dbContext.Matches.Add(match);
                _dbContext.SaveChanges();
            }
            else
            {
                
                existingMatch.IsMatch = false;
                _dbContext.SaveChanges();
            }
        }

        public void Delete(Match match)
        {
            var existingMatch = _dbContext.Matches.FirstOrDefault(m => m.AccountUserId == match.AccountUserId && m.MatchUserId == match.MatchUserId);
            try
            {
                _dbContext.Matches.Remove(match);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Błąd usuwanaia matcha", ex);
            }
 
        }

        public ICollection<int> OurMatch(int userId)
        {
            var match = _dbContext.Matches.Where(m => (m.MatchUserId == userId || m.AccountUserId == userId)).ToList();
            ICollection<int> result = new List<int>(); 
            foreach(var matchItem in match)
            {
                if(matchItem.AccountUserId == userId)
                {
                    result.Add(matchItem.MatchUserId);
                }
                else if(matchItem.MatchUserId == userId && matchItem.IsMatch)
                {
                    result.Add(matchItem.AccountUserId);
                }
            }
            return result;
        }
        public void UpdateMatch(int userId, int matchedUserId)
        {
            // Ustaw flagę IsMutual na true dla obu wpisów w tabeli Matches
           // var currentMatch = new Match { AccountUserId = userId, MatchUserId = matchedUserId, IsMatch = true };
            var existingMatch = _dbContext.Matches.FirstOrDefault(m => m.AccountUserId == matchedUserId && m.MatchUserId == userId);
            

            if (existingMatch != null)
            {
                existingMatch.IsMatch = true;
                _dbContext.SaveChanges();
            }
            else
            {
                var newMatch = new Match
                {
                    AccountUserId = userId,
                    MatchUserId = matchedUserId,
                    IsMatch = false
                };
                AddLike(newMatch);
            }

        }

        public void DeleteUsersMatchesByID(int userId)
        {
            // Pobierz matche do usunięcia dla danego użytkownika
            var matches = _dbContext.Matches.Where(h => h.AccountUserId == userId||h.MatchUserId==userId).ToList();

            if (matches.Any())
            {
                // Usuń matche danego użytkownika
                _dbContext.Matches.RemoveRange(matches);

                // Zapisz zmiany w bazie danych
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<Match> GetAllMatch()
        {
            return _dbContext.Matches.ToList();
        }

    }
}
