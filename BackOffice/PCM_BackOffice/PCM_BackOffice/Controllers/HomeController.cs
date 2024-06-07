using DAL.Login;
using Microsoft.AspNetCore.Mvc;
using Core.Home;
using Microsoft.AspNetCore.Authorization;
using Core.ChatProcess;
using Dal;
using System.Web;
using Dal.ChatMongoDB;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using PCM_BackOffice.Models;
using System.Security.Cryptography;
using Azure;




namespace PCM_BackOffice.Controllers
{
    public class HomeController : Controller
    {
        IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Emp_Content_Details emp_Content_Details)
        {
             //string pass = Request["Str_Emp_ID"].ToString();
            string Login_ID = emp_Content_Details.Str_Login_ID;
            string password = emp_Content_Details.Str_Password;
            DAL_Login _obj_ = new DAL_Login();
            string user_password = _obj_.ReturnPassword(Login_ID, _configuration);
            if (password.Trim() == user_password.Trim())
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddHours(1);
                Response.Cookies.Append("Emp_ID", Login_ID, option);

                HttpContext.Session.SetString("LoginID", Login_ID);
                var Session_Login_ID  = HttpContext.Session.GetString("LoginID");
                
                chatuserlist();

                return RedirectToAction("home-index");
                //return View("home");


            }

            return View();
        }

        [AllowAnonymous]
        [ActionName("home-index")]
        public IActionResult home()
        {
            return View("home-index");
        }


        [HttpGet]
        public IActionResult chatprocess()
        {
            HttpContext.Session.SetString("ActiveUser", "");
            chatuserlist();
            return View("chatprocess");
        }
        public void chatuserlist()
        {
            chatprocess chatprocess = new chatprocess();
            string Emp_id = "";
            if (Request.Cookies["Emp_ID"] != null)
            { Emp_id = HttpContext.Session.GetString("LoginID").ToString(); }
            var uList = chatprocess.GetActiveUserDetails(Emp_id, _configuration);
            ViewBag.UserList = uList;
            DAL_Login _obj_ = new DAL_Login();
            ViewBag.SenderIDs = Emp_id;
            ViewBag.SenderName = _obj_.ReturnNickName(Emp_id, _configuration);

        }

        [HttpPost]
        public async Task<IActionResult> sendmsg(ChatProcessList chatProcessList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("false");
            }
            else
            {
                string time = DateTime.Now.ToShortTimeString();
                string msg = chatProcessList.Massage.Replace("<p><br></p>", "");
                msg = msg.Replace("<p> </p><p>  </p><p>  </p><p>   </p>", "");
                var currentmsg = "<li class='chat-item-end'>                                                                     " +
                        "    <div class='chat-list-inner'>                                                              " +
                        "        <div class='me-3'>                                                                     " +
                        "            <span class='chatting-user-info d-inline-flex align-items-center'>                 " +
                        "                <span class='msg-sent-time'>                                                   " +
                        "                    <span class='chat-read-mark align-middle'>                                 " +
                        "                        <i class='ri-check-double-line'></i>                                   " +
                        "                    </span>" + time + "                                                             " +
                        "                </span> You                                                                    " +
                        "            </span>                                                                            " +
                        "            <div class='main-chat-msg'>                                                        " +
                        "                <div>                                                                          " +
                        "                    <p class='mb-0'>       " + msg + "                                     " +

                        "                    </p>                                                                       " +
                        "                </div>                                                                         " +
                        "            </div>                                                                             " +
                        "        </div>                                                                                 " +
                        "                                                                                               " +
                        "    </div>                                                                                     " +
                        "</li>";

                chatprocess chatprocess = new chatprocess();
                int saverow = chatprocess.SaveChatDetails(chatProcessList, _configuration);
                return this.Ok(currentmsg);
            }
        }

        [HttpPost]
        public async Task<IActionResult> recievrdAll(ChatProcessList chatProcessList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("false");
            }
            else
            {
                chatprocess chatprocess = new chatprocess();
                var result = chatprocess.chatdata(chatProcessList, _configuration);
                return this.Ok(result.Replace(',', ' '));
            }
        }

        [HttpPost]
        public async Task<IActionResult> SetrecievrdAll(ChatProcessList chatProcessList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("false");
            }
            else
            {
                chatprocess chatprocess = new chatprocess();
                var result = chatprocess.chatReceivedata(chatProcessList, _configuration);
                return this.Ok(result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SetNotification(string Notification)
        {
            DalChatMongo DCM = new DalChatMongo();
            string Emp_id = "";
            if (HttpContext.Session.GetString("LoginID") != null)
            { Emp_id = HttpContext.Session.GetString("LoginID").ToString(); }
            chatprocess chatprocess = new chatprocess();
            var result = chatprocess.NotificationReceivedata(Emp_id, _configuration);

            return this.Ok(result);


        }
        [HttpPost]
        public async Task<IActionResult> SetNotificationcount(string Notification)
        {
            DalChatMongo DCM = new DalChatMongo();
            string Emp_id = "";
            if (HttpContext.Session.GetString("LoginID") != null)
            { Emp_id = HttpContext.Session.GetString("LoginID").ToString(); }
            chatprocess chatprocess = new chatprocess();
            var result = chatprocess.NotificationReceivecount(Emp_id, _configuration);

            return this.Ok(result);


        }
        [HttpPost]
        public async Task<IActionResult> ActiveUserLists(string Notification)
        {
            string ActiveUser = HttpContext.Session.GetString("ActiveUser").ToString();
            DalChatMongo DCM = new DalChatMongo();
            string Emp_id = "";
            if (HttpContext.Session.GetString("LoginID") != null)
            { Emp_id = HttpContext.Session.GetString("LoginID").ToString(); }
            chatprocess chatprocess = new chatprocess();
            string result = chatprocess.ActiveUserList(Emp_id, _configuration);
            var fResult = "";
            if (ActiveUser == result)
            {
                fResult = "";
            }
            else
            {
                string result2 = chatprocess.NeweUserList(Emp_id, _configuration);
                fResult = " <ul class='list-unstyled mb-0 mt-2 chat-users-tab'>" + result + result2 + " </ul>";
                HttpContext.Session.SetString("ActiveUser", result);
            }
            return this.Ok(fResult);


        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles()
        {
            var folderName = Path.Combine("wwwroot", "chatfile");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var filess = Request.Form.Files;
            string type = Request.Form.Where(x => x.Key == "Type").FirstOrDefault().Value;
            string id = Request.Form.Where(x => x.Key == "Id").FirstOrDefault().Value;
            string result = string.Empty;
            foreach (IFormFile source in filess)
            {
                string strmsg = "";
                //string datetime = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();               
                var fileName = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.Trim('"');
                string fileextention = "." + fileName.Split('.').Last();
                string renameFile = Convert.ToString(Guid.NewGuid()) + "." + fileName.Split('.').Last();

                var fullPath = Path.Combine(pathToSave, renameFile);
                var dbPath = Path.Combine(folderName, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    source.CopyTo(stream);
                }
                if (fileextention == ".jpg" || fileextention == ".jpeg" || fileextention == ".png" || fileextention == ".gif")
                {
                    strmsg = "<img src='../chatfile/" + renameFile + "' style='max-width:250px;' />";
                }
                else
                {
                    strmsg = fileName + "<a href='../chatfile/" + renameFile + "' class='btn btn-icon btn-sm btn-success-transparent rounded-pill'><i class='ri-download-2-line'></i></a>";
                }
                string time = DateTime.Now.ToShortTimeString();



                result += "<li class='chat-item-end'>                                                                     " +
                        "    <div class='chat-list-inner'>                                                              " +
                        "        <div class='me-3'>                                                                     " +
                        "            <span class='chatting-user-info d-inline-flex align-items-center'>                 " +
                        "                <span class='msg-sent-time'>                                                   " +
                        "                    <span class='chat-read-mark align-middle'>                                 " +
                        "                        <i class='ri-check-double-line'></i>                                   " +
                        "                    </span>" + time + "                                                            " +
                        "                </span> You                                                                    " +
                        "            </span>                                                                            " +
                        "            <div class='main-chat-msg'>                                                        " +
                        "                <div>                                                                          " +
                        "                    <p class='mb-0'>       " + strmsg + "                                     " +

                        "                    </p>                                                                       " +
                        "                </div>                                                                         " +
                        "            </div>                                                                             " +
                        "        </div>                                                                                 " +
                        "                                                                                               " +
                        "    </div>                                                                                     " +
                        "</li>";


                ChatProcessList chatProcessList = new ChatProcessList();
                chatprocess chatprocess = new chatprocess();
                DAL_Login _obj_ = new DAL_Login();
                string SenderID = "";
                if (HttpContext.Session.GetString("LoginID") != null)
                { SenderID = HttpContext.Session.GetString("LoginID").ToString(); }
                chatProcessList.Massage = strmsg;
                chatProcessList.SenderID = SenderID;
                chatProcessList.SenderName = _obj_.ReturnNickName(SenderID, _configuration);
                chatProcessList.ReciverID = id;
                chatProcessList.ReciverName = type;
                int saverow = chatprocess.SaveChatDetails(chatProcessList, _configuration);

            }
            return this.Ok(result);
        }

    }
}
