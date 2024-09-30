using System.Collections.Generic;
using Szfindel.Models;

namespace Szfindel.Interface
{
    public interface IMessage
    {
        IEnumerable<Message> GetMessagesBetweenUsers(int senderId, int receiverId);
        void DeleteMessagesUsers(int senderId);

        void SendMessage(Message message,int userId);
    }
}
