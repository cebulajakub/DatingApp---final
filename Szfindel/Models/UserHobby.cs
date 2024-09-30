using System.ComponentModel.DataAnnotations;

namespace Szfindel.Models
{
    public class UserHobby
    {
       
        public int UserId { get; set; }
        public AccountUser User { get; set; }
        public int HobbyId { get; set; }
        public Hobby Hobby { get; set; }


    }
}
