using Szfindel.Models;

namespace Szfindel.Interface
{
    public interface IUserHobby
    {

        ICollection<Hobby> GetUserHobbyById(int Userid, int Hobbyid);

        void AddUserHobby(int Userid);

        void RemoveUserHobby(int Userid, int Hobbyid);

        
    }
}
