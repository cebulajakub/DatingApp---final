using Microsoft.EntityFrameworkCore;
using Szfindel.Models;
namespace Szfindel.Interface
{
    public interface IAccount
    {

        ICollection<AccountUser> GetAccountUsers();
        AccountUser GetAccountUserByUserId(int id);
        AccountUser GetAccountUserByAccUserId(int id);
        AccountUser AddAccountUserInfo(AccountUser accountUser);

        bool AccountHasUser(AccountUser accountId);
        IEnumerable<AccountUser> GetAllAccountUsers();
        void AddForgeinKeyToUser(AccountUser account);
        void DeleteAccountByID(int id);

        void UpdateAccount(AccountUser updatedAccount);
        void UpdateImage(AccountUser updatedAccount);

        IEnumerable<AccountUser> GetUsersByAgeRange(int? minAge, int? maxAge);
        IEnumerable<AccountUser> GetUsersByCity(string city);
        IEnumerable<Hobby> GetHobbies();




    }
}
