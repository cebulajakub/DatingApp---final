using System.ComponentModel.DataAnnotations;

namespace Szfindel.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        [Required(ErrorMessage = "Treść wiadomości jest wymagana.")]
        public string Text { get; set; }
        public DateTime DateTime { get; set; }


        public int SenderId { get; set; }
        public AccountUser? Sender { get; set; }

        public int ReceiverId { get; set; }
        public AccountUser? Receiver { get; set; }

    }
}
