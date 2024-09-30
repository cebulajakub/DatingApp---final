using System.ComponentModel.DataAnnotations;

namespace Szfindel.Models
{
    public class Hobby
    {
        
        [Key]
        public int HobbyId { get; set; }
        public string HobbyName { get; set; }
        public ICollection<UserHobby> UserHobbies { get; set; }
    }
}
