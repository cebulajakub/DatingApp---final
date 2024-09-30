using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Szfindel.Models
{
   
    public class AccountUser 
    {
        [Key]
        public int AccountUserId { get; set; }


         [Required(ErrorMessage = "Imię jest wymagane.")]
        
        [StringLength(30, ErrorMessage = "Twoje imie jest za długie")]
        [MinLength(4, ErrorMessage = "Imię musi mieć conajmniej 4 litery")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Imię nie może zawierac znaków")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Podaj Nazwisko")]
        [StringLength(50, ErrorMessage = "Twoje nazwisko jest za długie")]
        [MinLength(4, ErrorMessage = "Twoje Nazwisko musi mieć conajmniej 4 litery")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Nazwisko nie może zawierac znaków")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Twój wiek jest wymagany")]
        [Range(18,120 ,ErrorMessage = "Musisz mieć conajmniej 18 lat!!!")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Podaj Miasto")]
        public string City { get; set; }
        public int? Height { get; set; }

        public string? Image {  get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }

        public ICollection<Message>? SentMessages { get; set; }
        public ICollection<Message>? ReceivedMessages { get; set; }
        public ICollection<UserHobby>? UserHobbies { get; set; }
        public ICollection<Match>? Matches { get; set; }


    }
}

