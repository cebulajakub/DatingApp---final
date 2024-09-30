
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Szfindel.Interface;
using Szfindel.Models;
using IAccount = Szfindel.Interface.IAccount;

namespace Szfindel.Repo
{
    public class AccountRepo : IAccount
    {

        /// <summary>
        /// Konetkst bazy danych
        /// </summary>
        private readonly DatabaseContext _dbContext;


        /// <summary>
        /// Połączenie z bazą dancyh
        /// </summary>
        /// <param name="dbcontext"></param>
        public AccountRepo(DatabaseContext dbcontext)
        {
            _dbContext = dbcontext;

        }

        /// <summary>
        /// Wyświetlanie uzytkowników za pomoca ID
        /// </summary>
        /// <returns></returns>
        public ICollection<AccountUser> GetAccountUsers()
        {
            return _dbContext.AccountUsers.OrderBy(u => u.AccountUserId).ToList();
        }


        /// <summary>
        /// Dowdawanie danych konta uzytkownika
        /// </summary>
        /// <param name="accountUser"></param>
        /// <returns></returns>
        public AccountUser AddAccountUserInfo(AccountUser accountUser)
        {

           
            _dbContext.AccountUsers.Add(accountUser);
            _dbContext.SaveChanges();

            return accountUser;
        }
        //Zwracanie AccountUser za pomoca UserId -- kazdy Account ma usera
        //User nie musi miec accounta <- przypadek gdy osoba zalogowana nie wypelni informacji o sobie


        /// <summary>
        /// Pobieranie AccountUser poprzez UserId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AccountUser GetAccountUserByUserId(int id)
        {
            return _dbContext.AccountUsers.FirstOrDefault(u => u.UserId == id);
        }


        public AccountUser GetAccountUserByAccUserId(int id)
        {
            return _dbContext.AccountUsers.FirstOrDefault(a => a.AccountUserId == id);
        }

        /// <summary>
        ///  Pobranie wszytskich użytkowników z bazy danych 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AccountUser> GetAllAccountUsers()
        {
            // Pobierz wszystkie konta użytkowników z bazy danych
            return _dbContext.AccountUsers.ToList();
        }


        /// <summary>
        /// Przypisywanie AccountUser do Uesr-a
        /// </summary>
        /// <param name="account"></param>
        /// <exception cref="Exception"></exception>
        public void AddForgeinKeyToUser(AccountUser account)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UserId == account.UserId);
            if (user != null)
            {
                
                user.AccountUserId = account.AccountUserId;
                _dbContext.SaveChanges();

            }
            else
            {
                // Jeśli użytkownik nie istnieje, możesz podjąć odpowiednie działania, na przykład rzucić wyjątek
                throw new Exception("Użytkownik o podanym Id nie istnieje.");
            }
        }

        /// <summary>
        /// Sprawdzanie czy Uzytkownik wypełnił dane o sobie
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public bool AccountHasUser(AccountUser accountId)
        {
            var account = _dbContext.AccountUsers.FirstOrDefault(a => a.UserId == accountId.UserId);


            
            if (account != null && account.UserId != null)
            {
                return true; 
            }
            else
            {
                
                return false; 
            }
        }


        /// <summary>
        /// Edycja danych po tym jak użytkownik ma już wypełnione informacja o sobie(Przypisany Account)
        /// </summary>
        /// <param name="updatedAccount"></param>
        public void UpdateAccount(AccountUser updatedAccount)
        {
            var existingAccount = _dbContext.AccountUsers.FirstOrDefault(a => a.AccountUserId == updatedAccount.AccountUserId);
            if (existingAccount != null)
            {
                
                existingAccount.Name = updatedAccount.Name;
                existingAccount.Surname = updatedAccount.Surname;
                existingAccount.Age = updatedAccount.Age;
                existingAccount.Height = updatedAccount.Height;
              //  existingAccount.Image = updatedAccount.Image;
                existingAccount.City = updatedAccount.City;

                _dbContext.SaveChanges();
            }
           
        }
        /// <summary>
        /// Możliwość dodania zdjęcia
        /// </summary>
        /// <param name="updatedAccount"></param>
        public void UpdateImage(AccountUser updatedAccount)
        {
            var existingAccount = _dbContext.AccountUsers.FirstOrDefault(a => a.AccountUserId == updatedAccount.AccountUserId);
            existingAccount.Image = updatedAccount.Image;
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Wyświetlanie Hobby
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Hobby> GetHobbies()
        {
            return _dbContext.Hobbies.ToList();
        }
        /// <summary>
        /// Usuwanie konta uzytkownika
        /// </summary>
        /// <param name="id"></param>
        public void DeleteAccountByID(int id)
        {
            // Pobierz konto do usunięcia
            AccountUser account = _dbContext.AccountUsers.FirstOrDefault(a => a.AccountUserId == id);

            if (account != null)
            {
                // Oznacz konto do usunięcia
                _dbContext.AccountUsers.Remove(account);

                // Zapisz zmiany w bazie danych
                _dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Funkcja do filtrowania użytkowników z bazy danych na podstawie wieku
        /// </summary>
        /// <param name="minAge"></param>
        /// <param name="maxAge"></param>
        /// <returns></returns>
        public IEnumerable<AccountUser> GetUsersByAgeRange(int? minAge, int? maxAge)
        {
            IQueryable<AccountUser> query = _dbContext.AccountUsers;

            if (minAge != null)
            {
                query = query.Where(u => u.Age >= minAge);
            }

            if (maxAge != null)
            {
                query = query.Where(u => u.Age <= maxAge);
            }

            return query.ToList();
        }
        /// <summary>
        /// Pobranie użytkowników z bazy danych na podstawie miasta
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>

        public IEnumerable<AccountUser> GetUsersByCity(string city)
        {
            return _dbContext.AccountUsers.Where(u => u.City == city).ToList();
        }

        


    }

}

