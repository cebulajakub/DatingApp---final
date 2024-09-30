using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Szfindel.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Podaj Nickname")]
        

      //  [BindProperty]
        public string Username { get; set; }
        [Required(ErrorMessage = "Podaj hasło")]

        [MinLength(8, ErrorMessage = "Hasło jest za krótkie")]

        //[BindProperty]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int? AccountUserId { get; set; }
        public AccountUser? AccountUser { get; set; }

    }
}
