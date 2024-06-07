using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ChatProcess
{
    public class UserList
    {
        public string Emp_ID { get; set; }
        public string Pseudo_Name { get; set; }
        public string Time { get; set; }
        public string Massage { get; set; }
        public int countmessage { get; set; }
    }
    public class ChatProcessListDetail
    {
        public List<ChatProcessList> ChatList { get; set; }
    }

    public class ChatProcessList
    {
        public string SenderID { get; set; }
        public string ReciverID { get; set; }
        public string SenderName { get; set; }
        public string ReciverName { get; set; }
        public string Massage { get; set; }
        public int Chatpool { get; set; }
        public int Newmessage { get; set; }
        public string File1 { get; set; }
    }

}
