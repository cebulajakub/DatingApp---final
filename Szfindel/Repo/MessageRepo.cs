using System.Collections.Generic;
using System.Linq;
using Szfindel.Models;
using Szfindel.Interface;
using System;
using Microsoft.EntityFrameworkCore;

namespace Szfindel.Repo
{
    public class MessageRepo : IMessage
    {
        /// <summary>
        /// Kontekst bazy danych
        /// </summary>
        private readonly DatabaseContext _context;

        /// <summary>
        /// Połączenie z bazą dannych
        /// </summary>
        /// <param name="context"></param>
        public MessageRepo(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Pobranie wiadomości między dwoma użytkownikami(odbiorca i nadawca), poprzez ich ID 
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="receiverId"></param>
        /// <returns></returns>
        public IEnumerable<Message> GetMessagesBetweenUsers(int senderId, int receiverId)
        {
            return _context.Messages.Where(m => (m.SenderId == senderId && m.ReceiverId == receiverId) || (m.SenderId == receiverId && m.ReceiverId == senderId));
        }


        /// <summary>
        /// Wysyłanie wiadomości, dodawanie do bazy danych
        /// </summary>
        /// <param name="message"></param>
        /// <param name="userId"></param>
        public void SendMessage(Message message, int userId)
        {
            //message.ReceiverId = message.ReceiverId;
            //  message.SenderId = message.SenderId;

            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            message.SenderId = (int)user.AccountUserId;
            message.DateTime = DateTime.UtcNow;

            _context.Messages.Add(message);
            _context.SaveChanges();
        }

        /// <summary>
        /// Usuwa wiadomości między dwoma użytkownikami na podstawie ich identyfikatorów nadawcy i odbiorcy.
        /// </summary>
        /// <param name="senderId">Identyfikator nadawcy</param>
        /// <param name="receiverId">Identyfikator odbiorcy</param>
        public void DeleteMessagesUsers(int senderId)
        {
            // Znajdź wszystkie wiadomości między danymi użytkownikami
            var messagesToDelete = _context.Messages.Where(m => (m.SenderId == senderId || m.ReceiverId == senderId)).ToList();

            if (messagesToDelete.Any())
            {
                // Usuń znalezione wiadomości
                _context.Messages.RemoveRange(messagesToDelete);
                _context.SaveChanges();
            }
        }
    }


}


