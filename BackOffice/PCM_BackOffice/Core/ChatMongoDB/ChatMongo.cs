using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ChatMongoDB
{
    public class ChatMongo
    {
        public List<Chat> Chat { get; set; }
    }
    public class Chat
    {

        public string Id { get; set; } 
        public string SenderID { get; set; } 
        public string ReciverID { get; set; } 
        public string SenderName { get; set; } 
        public string ReciverName { get; set; } 
        public string Massage { get; set; } 
        public int Chatpool { get; set; }
        public bool IsRead { get; set; }
        public DateTime Createdatetime { get; set; }
    }
    public class NewChat
    {
        public string SenderID { get; set; } 
        public string ReciverID { get; set; } 
        public string Massage { get; set; } 
        public string SenderName { get; set; } 
        public string ReciverName { get; set; } 
        public int Chatpool { get; set; }

    }
    public class UpdateStatus
    {
        public string SenderID { get; set; } 
        public string ReciverID { get; set; } 


    }
}
