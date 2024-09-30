using Szfindel.Models;

namespace Szfindel.Interface
{
    public interface IHobby
    {
        IEnumerable<Hobby> GetAllHobby();

        bool IfUserGetHobby( int userId, int hobbyId );
        void DeleteUsersHobbiesByID(int userId);


        ICollection<Hobby> GetUserHobbyById(int AccountUserid);

        //void AddUserHobby(int Userid);
        void AddUserHobby(int Userid, int Hobbyid);

        void RemoveUserHobby(int Userid, int Hobbyid);
    }
}
