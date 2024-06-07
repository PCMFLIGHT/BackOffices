using DAL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Core.ChatProcess; 
using Core.UserDetails;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;





namespace Dal
{
    public class chatprocess
    {

        public static int SaveChatDetails(ChatProcessList chatProcessList, IConfiguration configuration)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[6];
                string msg= chatProcessList.Massage.Replace("<p><br></p>", "");
                msg= msg.Replace("<p> </p><p>  </p><p>  </p><p>   </p>", "");
                param[0] = new SqlParameter("@SenderID", SqlDbType.VarChar, 100);
                param[0].Value = chatProcessList.SenderID;

                param[1] = new SqlParameter("@ReciverID", SqlDbType.VarChar, 100);
                param[1].Value = chatProcessList.ReciverID;

                param[2] = new SqlParameter("@SenderName", SqlDbType.VarChar, 100);
                param[2].Value = chatProcessList.SenderName;

                param[3] = new SqlParameter("@ReciverName", SqlDbType.VarChar, 100);
                param[3].Value = chatProcessList.ReciverName;

                param[4] = new SqlParameter("@Massage", SqlDbType.VarChar, int.MaxValue);
                param[4].Value = msg;

                param[5] = new SqlParameter("@Chatpool", SqlDbType.Int);
                param[5].Value = chatProcessList.Chatpool;

               

                using (SqlConnection con = GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration))
                {
                    return SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "SP_INSERT_Chatdetails", param);
                }
            }
            catch
            {
                return 0;
            } 
        }
        public List<UserList> GetUserDetails(string Str_Emp_ID, IConfiguration configuration)
        {
            List<UserList> list = new List<UserList>();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Emp_ID", SqlDbType.VarChar, 50);
            param[0].Value = Str_Emp_ID;
            using (SqlConnection con = GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration))
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(con, CommandType.StoredProcedure, "SP_EMP_LIST ", param);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        UserList content = new UserList();
                        content.Emp_ID = dr["Emp_ID"].ToString();
                        content.Pseudo_Name = dr["Pseudo_Name"].ToString();
                        content.countmessage = Convert.ToInt32(dr["countmessage"]);
                        list.Add(content);
                    }
                }
            }
            return list;
        } 
        public string chatdata(ChatProcessList chatProcessList, IConfiguration configuration)
        {
            string jsondata=string.Empty;
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@SenderID", SqlDbType.VarChar, 100);
                param[0].Value = chatProcessList.SenderID;
                param[1] = new SqlParameter("@ReciverID", SqlDbType.VarChar, 100);
                param[1].Value = chatProcessList.ReciverID;
                using (SqlConnection con = GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration))
                {
                    DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "SP_SingleUser_Chatdetails", param);
                    User_DetailsContent content = new User_DetailsContent();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        jsondata = ds.Tables[0].Rows[0][0].ToString();
                        return jsondata;
                    }
                    else
                    {
                        return jsondata;
                    }
                }
            }
            catch
            {
                return "";
            }
        }
        public string chatReceivedata(ChatProcessList chatProcessList, IConfiguration configuration)
        {
            string jsondata = string.Empty;
            try
            {
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@SenderID", SqlDbType.VarChar, 100);
                param[0].Value = chatProcessList.SenderID;
                param[1] = new SqlParameter("@ReciverID", SqlDbType.VarChar, 100);
                param[1].Value = chatProcessList.ReciverID;
                using (SqlConnection con = GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration))
                {
                    DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "SP_SingleUser_ChatReceive", param);
                    User_DetailsContent content = new User_DetailsContent();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        jsondata = ds.Tables[0].Rows[0][0].ToString();
                        return jsondata;
                    }
                    else
                    {
                        return jsondata;
                    }
                }
            }
            catch
            {
                return "";
            }
        }
        public string NotificationReceivedata(string SenderID, IConfiguration configuration)
        {
            string jsondata = string.Empty;
            try
            {
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@ReciverID", SqlDbType.VarChar, 100);
                param[0].Value = SenderID;
                using (SqlConnection con = GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration))
                {
                    DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "SP_Chat_Notification", param);
                    User_DetailsContent content = new User_DetailsContent();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        List<string> userlist = new List<string>();
                        string firstsender = "";
                        string Nextsender = "";
                        string userexit = "";
                        for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string count = "";
                            Nextsender = ds.Tables[0].Rows[i]["SenderID"].ToString();
                          
                            for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                            {
                                if (Nextsender == ds.Tables[1].Rows[j]["SenderID"].ToString())
                                {
                                    count = ds.Tables[1].Rows[j]["MScount"].ToString();
                                    break;
                                } 
                            }
                            if (Nextsender != firstsender)
                            {
                               
                                foreach (string value in userlist)
                                {
                                    if (value == Nextsender)
                                    {
                                        userexit = "Yes";
                                        break;
                                    }
                                }
                                if (userexit == "")
                                {
                                    jsondata += "<li class='dropdown-item'> " +
                                                    "    <div class='d-flex align-items-start'>" +
                                                     
                                                    "        <div class='w-100'>" +
                                                    "            <div class='flex-grow-1 d-flex align-items-centermy-auto'>" +
                                                    "                <div>" +
                                                    "                    <h6 class='mb-0 fw-semibold fs-14'><a href='chatprocess'>" + ds.Tables[0].Rows[i]["SenderName"] + "  <span class='badge bg-secondary-transparent'>" + count + "</span></a></h6>" +
                                                    "                </div>" +
                                                    "                <div class='ms-auto text-end'>" +
                                                    "                    <p class='text-muted mb-0'> " + ds.Tables[0].Rows[i]["times"] + " </p>" +
                                                    "                </div>" +
                                                    "            </div>" +
                                                    "            <div class='flex-grow-1 d-flex align-items-centermy-auto'>" +
                                                    "                <div>" +
                                                    "                    <span class='text-muted fw-normal fs-12'>" + ds.Tables[0].Rows[i]["Massage"] + "....</span>" +
                                                    "                </div>" +
                                                    "            </div>" +
                                                    "        </div>" +
                                                    "    </div>" +
                                                    "</li>";
                                    userlist.Add(Nextsender);
                                }
                                userexit = "";
                               
                                firstsender = Nextsender;
                            }
                        }
                        

                        return jsondata;
                    }
                    else
                    {
                        return jsondata;
                    }
                }
            }
            catch
            {
                return "";
            }
        }

        public string NotificationReceivecount(string SenderID, IConfiguration configuration)
        {
            string jsondata = string.Empty;
            try
            {
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@ReciverID", SqlDbType.VarChar, 100);
                param[0].Value = SenderID;
                using (SqlConnection con = GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration))
                {
                    DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "SP_Chat_NotificationCount", param);
                    User_DetailsContent content = new User_DetailsContent();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        jsondata = ds.Tables[0].Rows[0][0].ToString();
                        return jsondata;
                    }
                    else
                    {
                        return jsondata;
                    }
                }
            }
            catch
            {
                return "";
            }
        }

        public string ActiveUserList(string SenderID, IConfiguration configuration)
        {
            string jsondata = "<li class='pb-0'><p class='text-muted fs-11 fw-semibold mb-2 op-7'>ACTIVE CHATS</p></li>";
            try
            {
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@ReciverID", SqlDbType.VarChar, 100);
                param[0].Value = SenderID;
                using (SqlConnection con = GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration))
                {
                    DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "SP_Chat_ActiveUser", param);
                    User_DetailsContent content = new User_DetailsContent();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        List<string> userlist = new List<string>();
                        string firstsender = "";
                        string Nextsender = "";
                        string userexit = "";
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string count = "";
                            Nextsender = ds.Tables[0].Rows[i]["SenderID"].ToString();
                            
                            for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                            {
                                if (Nextsender == ds.Tables[1].Rows[j]["SenderID"].ToString())
                                {
                                    count = ds.Tables[1].Rows[j]["MScount"].ToString();
                                    break;
                                }
                            }
                            if (Nextsender != firstsender)
                            {

                                foreach (string value in userlist)
                                {
                                    if (value == Nextsender)
                                    {
                                        userexit = "Yes";
                                        break;
                                    }
                                }
                                if (userexit == "")
                                {

                                    string msgdata = ds.Tables[0].Rows[i]["Massage"].ToString().Replace("<p>", "");
                                    if (msgdata.Length > 10 && msgdata.Substring(0, 4).Trim() == "<img")
                                    {
                                        msgdata = "<img src='../images/messageicon.png' style='max-width:30px;' />";
                                    }
                                    else
                                    {
                                        msgdata = Regex.Replace(msgdata, "<.*?>", String.Empty);
                                        if (msgdata.Length > 22)
                                        {
                                            msgdata = msgdata.Substring(0, 20) + "...";
                                        }
                                    }
                                    jsondata += "<li class='checkforactive' id="+ ds.Tables[0].Rows[i]["SenderID"].ToString() + " onclick=\"return setselectstatus('" + ds.Tables[0].Rows[i]["SenderID"].ToString() + "')\" style=\" border-bottom:#C0C0C0 solid 1px; \">                                                                            " +
                            "    <a onclick=\"return setuser('" + ds.Tables[0].Rows[i]["SenderID"].ToString() + "','" + ds.Tables[0].Rows[i]["SenderName"].ToString() + "')\">       " +
                            "                                                                                                       " +
                            "        <div class='d-flex align-items-top'>                                                           " +
                            //"            <div class='me-1 lh-1'>                                                                    " +
                            //"                <span class='avatar avatar-md online me-2 avatar-rounded'>                             " +
                            //"                    <img src='../assets/images/faces/5.jpg' alt='img'>                                 " +
                            //"                </span>                                                                                " +
                            //"            </div>                                                                                     " +
                            "            <div class='flex-fill' id="+"P"+ ds.Tables[0].Rows[i]["SenderID"].ToString() + ">                                                                    " +
                            "                <p class='mb-0 fw-semibold'>                                                           " +
                            "                    " + ds.Tables[0].Rows[i]["SenderName"] + "                                                                 " +
                            "                    <span class='badge bg-secondary-transparent'>" + count + "</span>                              " +
                            "                    <span class='float-end text-muted fw-normal fs-11'>" + ds.Tables[0].Rows[i]["times"] + "</span>                   " +
                            "                </p>                                                                                   " +
                            "                <p class='fs-12 mb-0'>  " + msgdata + "</p>" +
                            
                            "            </div>                                                                                     " +
                            "        </div>                                                                                         " +
                            "    </a>                                                                                               " +
                            "</li> ";
                                    userlist.Add(Nextsender);
                                }
                                userexit = "";

                                firstsender = Nextsender;
                            }
                        }


                        return jsondata;
                    }
                    else
                    {
                        return jsondata;
                    }
                }
            }
            catch
            {
                return "";
            }
        }
        public string NeweUserList(string SenderID, IConfiguration configuration)
        {
            string jsondata = "<li class='pb-0'><p class='text-muted fs-11 fw-semibold mb-2 op-7'>NEW CHATS</p></li>";
            try
            {
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@Emp_ID", SqlDbType.VarChar, 100);
                param[0].Value = SenderID;
                using (SqlConnection con = GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration))
                {
                    DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "SP_EMP_LIST", param); 
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                       
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        { 
                            jsondata += "<li class='checkforactive'>                                                                            " +
                            "    <a  onclick=\"return setuser(\'" + ds.Tables[0].Rows[i]["EmpCode"] + "\',\'" + ds.Tables[0].Rows[i]["NickName"] +"\')\">       " +
                            "                                                                                                       " +
                            "        <div class='d-flex align-items-top'>                                                           " +
                            //"            <div class='me-1 lh-1'>                                                                    " +
                            //"                <span class='avatar avatar-md online me-2 avatar-rounded'>                             " +
                            //"                    <img src='../assets/images/faces/5.jpg' alt='img'>                                 " +
                            //"                </span>                                                                                " +
                            //"            </div>                                                                                     " +
                            "            <div class='flex-fill'>                                                                    " +
                            "                <p class='mb-0 fw-semibold'>                                                           " +
                            "                    " + ds.Tables[0].Rows[i]["NickName"] + "                                                                 " +
                            "                    <span class='badge bg-secondary-transparent'></span>                              " +
                            "                    <span class='float-end text-muted fw-normal fs-11'></span>                   " +
                            "                </p>                                                                                   " +
                            "                <p class='fs-12 mb-0'>                                                                 " +
                            "                    <span class='chat-msg text-truncate'></span>                  " +
                            "                    <span class='chat-read-icon float-end align-middle'>                               " +
                            //"                        <i class='ri-check-double-fill'></i>                                           " +
                            "                    </span>                                                                            " +
                            "                </p>                                                                                   " +
                            "            </div>                                                                                     " +
                            "        </div>                                                                                         " +
                            "    </a>                                                                                               " +
                            "</li> ";
                                     
                        }


                        return jsondata;
                    }
                    else
                    {
                        return jsondata;
                    }
                }
            }
            catch
            {
                return "";
            }
        }

        public List<UserList> GetUserDetails2(string Str_Emp_ID, IConfiguration configuration)
        {
            List<UserList> list = new List<UserList>();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Emp_ID", SqlDbType.VarChar, 50);
            param[0].Value = Str_Emp_ID;
            using (SqlConnection con = GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration))
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(con, CommandType.StoredProcedure, "SP_EMP_LIST ", param);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        UserList content = new UserList();
                        content.Emp_ID = dr["Emp_ID"].ToString();
                        content.Pseudo_Name = dr["Pseudo_Name"].ToString();
                        content.countmessage = Convert.ToInt32(dr["countmessage"]);
                        list.Add(content);
                    }
                }
            }


            return list;
        }

        public List<UserList> GetActiveUserDetails(string SenderID, IConfiguration configuration)
        {
            List<UserList> list = new List<UserList>();
            try
            {
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@ReciverID", SqlDbType.VarChar, 100);
                param[0].Value = SenderID;
                using (SqlConnection con = GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration))
                {
                    DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "SP_Chat_Notification", param);
                     
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        List<string> Puserlist = new List<string>();
                        string firstsender = "";
                        string Nextsender = "";
                        string userexit = "";
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            
                            string count = "";
                            Nextsender = ds.Tables[0].Rows[i]["SenderID"].ToString();

                            for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                            {
                                if (Nextsender == ds.Tables[1].Rows[j]["SenderID"].ToString())
                                {
                                    count = ds.Tables[1].Rows[j]["MScount"].ToString();
                                    break;
                                }
                            }
                            if (Nextsender != firstsender)
                            {

                                foreach (string value in Puserlist)
                                {
                                    if (value == Nextsender)
                                    {
                                        userexit = "Yes";
                                        break;
                                    }
                                }
                                if (userexit == "")
                                {

                                    UserList content = new UserList();
                                    content.Emp_ID = ds.Tables[0].Rows[i]["SenderID"].ToString();
                                    content.Pseudo_Name = ds.Tables[0].Rows[i]["SenderName"].ToString();
                                    content.countmessage = Convert.ToInt32(count);
                                    content.Time = ds.Tables[0].Rows[i]["times"].ToString();
                                    content.Massage = ds.Tables[0].Rows[i]["Massage"].ToString();
                                    list.Add(content);
                                    Puserlist.Add(Nextsender);
                                }
                                userexit = "";

                                firstsender = Nextsender;
                            }
                        }


                        return list;
                    }
                    else
                    {
                        return list;
                    }
                }
            }
            catch
            {
                return list;
            }
        }
    }
}
 