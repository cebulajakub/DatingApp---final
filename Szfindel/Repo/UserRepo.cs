using Microsoft.EntityFrameworkCore;
using Szfindel.Interface;
using Szfindel.Models;

namespace Szfindel.Repo
{
    public class UserRepo : IUser
    {
        /// <summary>
        /// Kontekst bazy danych
        /// </summary>
        private readonly DatabaseContext _dbContext;

        /// <summary>
        /// Połączenie z bazą danych
        /// </summary>
        /// <param name="dbcontext"></param>
        public UserRepo(DatabaseContext dbcontext) { 
            _dbContext = dbcontext;
       
        }


        /// <summary>
        /// Pobranie wszystkich userów
        /// </summary>
        /// <returns></returns>
        public ICollection<User> GetUsers()
        {
            return _dbContext.Users.OrderBy(u => u.UserId).ToList();
        }

        /// <summary>
        /// Tworzenie użytkownika
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _dbContext.Users.Add(user);

            //return Save();
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Pobieranie użytkownika poprzez username i hasło
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User GetUserByCredentials(string username, string password)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        /// <summary>
        /// Pobieranie użytkownika tylko za pomocą username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User GetUserByUsername(string username)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Username == username);
        }

        /// <summary>
        /// Usuwa użytkownika na podstawie jego identyfikatora.
        /// </summary>
        /// <param name="userId">Identyfikator użytkownika do usunięcia</param>
        public void DeleteUserById(int userId)
        {
            var userToDelete = _dbContext.Users.FirstOrDefault(u => u.AccountUserId == userId);

            if (userToDelete != null)
            {
                _dbContext.Users.Remove(userToDelete);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException($"Użytkownik o identyfikatorze {userId} nie został znaleziony.");
            }
        }
    }
}


