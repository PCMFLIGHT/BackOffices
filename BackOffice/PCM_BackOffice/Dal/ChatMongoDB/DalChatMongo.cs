using Newtonsoft.Json;
using System;
using System.Net;
using Core.ChatMongoDB;
 

namespace Dal.ChatMongoDB
{
    public class DalChatMongo
    {
        static string APIurl = "https://localhost:7120/api/Chat/";
        public string Get_ChatList(string SenderID, string ReciverID)
        {
            string result = "";
            try
            {
                WebClient client = new WebClient();
                var url = APIurl + "messagelist?SenderID=" + SenderID + "&ReciverID="+ ReciverID;
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                System.DateTime dt = DateTime.Now;
                result = client.DownloadString(url);
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string Get_LastChat(string SenderID, string ReciverID)
        {
            string result = "";
            try
            {
                WebClient client = new WebClient();
                var url = APIurl + "recievedmessage?SenderID=" + SenderID + "&ReciverID=" + ReciverID;
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                System.DateTime dt = DateTime.Now;
                result = client.DownloadString(url);
                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public string Get_Notification(string SenderID)
        {
            string result = "";
            try
            {
                WebClient client = new WebClient();
                var url = APIurl + "notification?ReciverID=" + SenderID;
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                System.DateTime dt = DateTime.Now;
                result = client.DownloadString(url);
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string Update_ChatRead(UpdateStatus updateChat)
        {
            string result = "";
            try
            {
                WebClient client = new WebClient();
                var url = APIurl + "updatestatus";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";               
                string serialisedData = JsonConvert.SerializeObject(updateChat);
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                System.DateTime dt = DateTime.Now;
                var kk = client.UploadString(url, "PUT", serialisedData);
                result = "Ok";
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string Send_Chat(NewChat newchat)
        {
            string result = "";
            try
            {
                WebClient client = new WebClient();
                var url = APIurl;
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                string serialisedData = JsonConvert.SerializeObject(newchat);
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                System.DateTime dt = DateTime.Now;
                var kk = client.UploadString(url, serialisedData);
                result = "Ok";
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
   


}
