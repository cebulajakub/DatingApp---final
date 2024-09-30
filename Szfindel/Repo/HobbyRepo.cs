using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Szfindel.Interface;
using Szfindel.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Szfindel.Repo
{
    public class HobbyRepo : IHobby
    {
        /// <summary>
        /// Kontekst bazy danych
        /// </summary>
        private readonly DatabaseContext _dbContext;

        /// <summary>
        /// Połączenie z bazą danych
        /// </summary>
        /// <param name="dbcontext"></param>
        public HobbyRepo(DatabaseContext dbcontext)
        {
            _dbContext = dbcontext;

        }

        /// <summary>
        /// Pobranie wszystkich hobby z bazy danych
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Hobby> GetAllHobby()
        {
            return _dbContext.Hobbies.ToList();
        }

        /// <summary>
        /// Dodawanie hobby do danego usera
        /// </summary>
        /// <param name="Userid"></param>
        /// <param name="Hobbyid"></param>
        public void AddUserHobby(int Userid, int Hobbyid)
        {

            var hobby = new UserHobby();
            
            hobby.UserId = Userid;
            hobby.HobbyId = Hobbyid;

            _dbContext.UserHobby.Add(hobby);
            _dbContext.SaveChanges();

        }

        /// <summary>
        /// Pobieranie hobby użytkownika przez jego ID
        /// </summary>
        /// <param name="AccountUserid"></param>
        /// <returns></returns>
        public ICollection<Hobby> GetUserHobbyById(int AccountUserid)
        {

            var userHobbies = _dbContext.UserHobby
                                 .Where(uh => uh.UserId == AccountUserid)
                                 .Select(uh => uh.Hobby)
                                 .ToList();

            return userHobbies;

        }
        /// <summary>
        /// Usuwanie hobby użytkownika z bazy danych
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="hobbyId"></param>
        /// <exception cref="Exception"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public void RemoveUserHobby(int userId, int hobbyId)
        {
            var userHobby = _dbContext.UserHobby.FirstOrDefault(uh => uh.UserId == userId &&
                                                                uh.HobbyId == hobbyId);  //szukaie hobby danego użytkownika

        
        if (userHobby != null)
        {
            try
            {
                _dbContext.UserHobby.Remove(userHobby); //jeśli jest to usuwamy, a jeśli nie to obsługujemy wyjątek
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                    throw new Exception("Błąd usuwania hobby", ex);
            }
        }
        else
        {
            throw new InvalidOperationException("Użytkownik nie ma takiego hobby");
        }


        }




    public void DeleteUsersHobbiesByID(int userId)
        {
            // Pobierz hobby do usunięcia dla danego użytkownika
            var hobbies = _dbContext.UserHobby.Where(h => h.UserId == userId).ToList();

            if (hobbies.Any())
            {
                // Usuń hobby danego użytkownika
                _dbContext.UserHobby.RemoveRange(hobbies);

                // Zapisz zmiany w bazie danych
                _dbContext.SaveChanges();
            }
        }


    /// <summary>
    /// Sprawdzanie czy user ma to hobby jako swoje, jest wykorzystane do dodawania/usuwania
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="hobbyId"></param>
    /// <returns></returns>
    public bool IfUserGetHobby(int userId, int hobbyId)
        {
            return _dbContext.UserHobby.Any(uh => uh.UserId == userId && uh.HobbyId == hobbyId);
        }
    }



 }
