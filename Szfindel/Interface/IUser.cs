using Szfindel.Models;

namespace Szfindel.Interface
{
    public interface IUser
    {
        ICollection<User> GetUsers();

        User GetUserByCredentials(string username, string password);
        void CreateUser(User user);
        void DeleteUserById(int userId);
        User GetUserByUsername(string username);
        
    }
}
