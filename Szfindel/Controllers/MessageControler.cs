using Microsoft.AspNetCore.Mvc;
using Szfindel.Interface;
using Szfindel.Models;
using System.Collections.Generic;
using Szfindel.Repo;

namespace Szfindel.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : Controller
    {

        private readonly IMessage _messageRepository;
        //private readonly IAccount _accountRepository;


        /// <summary>
        /// Łączenie interfejsu z kontrolerem
        /// </summary>
        /// <param name="messageRepository"></param>
        /// <param name="accountRepository"></param>
        public MessageController(IMessage messageRepository,IAccount accountRepository)
        {
            _messageRepository = messageRepository;
           // _accountRepository = accountRepository;
        }

        /// <summary>
        /// Pobieranie informacji z bazy danych o wiadomościach między uczestnikami
        /// </summary>
        /// <param name="receiverId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SendMessage(int receiverId)
        {
          
            var accId = Request.Cookies["AccountUserId"];
            int.TryParse(accId, out int accIdint);

            IEnumerable<Message> messages = _messageRepository.GetMessagesBetweenUsers(accIdint, receiverId);
            ViewBag.Messages = messages;
            ViewBag.account = accIdint;

            return View("~/Views/Message/Index.cshtml");
        }


        /// <summary>
        /// Tworzenie nowych wiadomości, informacje do kogo i od kogo
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult sendmessage([FromForm] Message message)
        {
            var IDre = Request.Cookies["AccountUserId"];//mozna zamienic ten kod ponizej idUs == IDre
            var IDrequest = Request.Cookies["UserId"];
            int idUs;
            if (IDrequest != null)
            {
                int.TryParse(IDrequest, out int userIdint);
                idUs = userIdint;
                _messageRepository.SendMessage(message, idUs);
            }

           // ViewBag.message = message;
            
            return RedirectToAction("AccountProfil", "Account", new { id = message.ReceiverId });
        }


    }
}
