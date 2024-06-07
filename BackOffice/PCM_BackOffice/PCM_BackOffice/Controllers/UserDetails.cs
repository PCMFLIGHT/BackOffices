using Dal.UserDetails;
using Microsoft.AspNetCore.Mvc;
using Core.UserDetails;
using Core;
using Dal;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Newtonsoft.Json;

namespace PCM_BackOffice.Controllers
{
    public class UserDetails : Controller
    {
        IConfiguration _configuration;
        public UserDetails(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //------------------------------------------------------- Add User Registration-------------
        [HttpGet]
        [AllowAnonymous]
        [ActionName("Add-User-Details")]
        public IActionResult index(Get_UserDetails content)
        
        {
            if (HttpContext.Session.GetString("LoginID") != null)
            {
                Get_Set_Value();
                Get_UserDetails get_UserDetails = new Get_UserDetails();
                //List<User_DetailsContent> user_details_content = DAL_UserDetails.GetEmployeeDetails(content, _configuration);
                //get_UserDetails.user = user_details_content;
                ViewBag.none = "none";
                ViewBag.Search_divhide = "none";
                ViewBag.update_divhide= "none";
                return View("search_user_details", get_UserDetails);
            }
            else
            {
                Redirect("/");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ActionName("Add-User-Details")]
        public ActionResult Index(Get_UserDetails userdetails)
        {
            if (HttpContext.Session.GetString("LoginID") != null)
            {
                DAL_UserDetails dAL_UserDetails = new DAL_UserDetails();
                int userDetails = DAL_UserDetails.SaveUserDetails(userdetails, _configuration);
                if (userDetails > 0)
                {
                    var Login_ID = HttpContext.Session.GetString("Str_Emp_ID");
                    Get_UserDetails get_UserDetails = new Get_UserDetails();
                    //List<User_DetailsContent> user_details_content = DAL_UserDetails.TopOne_GetUserDetails(userdetails, _configuration);
                    //get_UserDetails.user = user_details_content;
                    //User_DetailsContent content = new User_DetailsContent();
                    Get_Set_Value();
                    @ViewBag.message = "Data Is Saved Successfully !!";
                    ViewBag.Search_divhide = "none";
                    ViewBag.update_divhide = "none";
                    return View("search_user_details", get_UserDetails);
                }
                else
                {
                    @ViewBag.message = "Data Is Not Saved Successfully !!";
                    Get_Set_Value();
                    return View();
                }
            }
            else
            {
                return RedirectToRoute("home");
            }
        }
        //------------------------------------------------------- End-------------------------------
        //------------------------------------------------------- Search User Registration-----------
        [HttpPost]
        public IActionResult Deletefun(Get_UserDetails content)
        {
            var id = Request.Query["Id"].ToString();

            DAL_UserDetails _obj = new DAL_UserDetails();
            string data = _obj.Delete(id, _configuration);
            return Json(data);
        }

        [HttpGet]
        public IActionResult search_user_details(User_DetailsContent user_content)
        {
            if (HttpContext.Session.GetString("LoginID") != null)
            {
                Get_UserDetails get_UserDetails = new Get_UserDetails();
                Get_Set_Value();
                ViewBag.add_divhide = "none";
                ViewBag.update_divhide = "none";
                return View("search_user_details", get_UserDetails);
            }
            else
            {
            return View();
            }
        }
        [HttpPost]
        public IActionResult search_user(Get_UserDetails userdetails)
        {
            if (HttpContext.Session.GetString("LoginID") != null)
            {
                Get_UserDetails get_UserDetails = new Get_UserDetails();
                List<User_DetailsContent> user_details_content = DAL_UserDetails.GetEmployeeDetails(userdetails, _configuration);
                get_UserDetails.user = user_details_content;
                User_DetailsContent content = new User_DetailsContent();
                Get_Set_Value();
                ViewBag.add_divhide = "none";
                ViewBag.update_divhide = "none";
                return View("search_user_details", get_UserDetails);
            }
            else
            {
                return View();
            }
        }
        //------------------------------------------------------- End-------------------------------
        //------------------------------------------------------- Update User Registration---------
        [HttpGet]
        public IActionResult UpdateUserDetails()
        {
            string id = Request.Query["id"].ToString();
            string repo = Request.Query["repo"].ToString();
            Get_UserDetails user_details_content = DAL_UserDetails.GetSetUserDetails(id, _configuration);
            Get_Set_Value();
            ViewBag.none = "none";
            ViewBag.add_divhide = "none";
            ViewBag.Search_divhide = "none";
            ViewBag.Search_divhide = "none";  
            return View("search_user_details", user_details_content);
        }
        [HttpPost]
        public IActionResult UpdateUserDetails(Get_UserDetails getuserdetails)
        {
            if (HttpContext.Session.GetString("LoginID") != null)
            {
                string id= getuserdetails.users.Int_Id.ToString();
                HttpContext.Session.SetString("Int_Id", id);
                DAL_UserDetails dAL_UserDetails = new DAL_UserDetails();
                    int userDetails = DAL_UserDetails.UpdateUserDetails(getuserdetails, _configuration);
                    if (userDetails > 0)
                    {
                        @ViewBag.message = "Data Is Update Successfully !!";
                        var Int_ID = HttpContext.Session.GetString("Int_Id");
                        Get_UserDetails get_UserDetails = new Get_UserDetails();
                        List<User_DetailsContent> user_details_content = DAL_UserDetails.TopOne_GetUserDetails(Int_ID, _configuration);
                        get_UserDetails.user = user_details_content;
                        Get_Set_Value();
                        ViewBag.none = "block";
                        ViewBag.add_divhide = "none";
                        ViewBag.uptbutton = "none;";
                        ViewBag.Search_divhide = "none";
                    return View("search_user_details", get_UserDetails);
                    }
                    else
                    {
                        @ViewBag.message = "Data Is Not Saved Successfully !!";
                        return View();
                    }
            }
            else
            {
                return RedirectToRoute("home");
            }
        }


        //public IActionResult UpdateUserDetails(Get_UserDetails getuserdetails)
        //{
        //    if (HttpContext.Session.GetString("LoginID") != null)
        //    {
        //        string Emp_id = getuserdetails.users.Str_Emp_ID;
        //        string old_emp_id = getuserdetails.users.Str_OldEmp_ID;
        //        int Is_Active = getuserdetails.users.Int_Is_Active;
        //        int This_Status = getuserdetails.users.Int_This_Status;
        //        //if(getuserdetails.users.Str_Emp_ID != getuserdetails.users.Str_OldEmp_ID)
        //        //{
        //        //    DAL_UserDetails _obj_ = new DAL_UserDetails();
        //        //    string empid = _obj_.GetEmployeeID(Emp_id, _configuration);
        //        //    if (empid == "")
        //        //    {
        //        //        DAL_UserDetails dAL_UserDetails = new DAL_UserDetails();
        //        //        int userDetails = DAL_UserDetails.UpdateUserDetails(getuserdetails, _configuration);
        //        //        if (userDetails > 1)
        //        //        {
        //        //            @ViewBag.message = "Data Is Update Successfully !!";
        //        //            Get_Set_Value();
        //        //            ViewBag.none = "block";
        //        //            return View();
        //        //        }
        //        //        else
        //        //        {
        //        //            @ViewBag.message = "Data Is Not Saved Successfully !!";
        //        //            return View();
        //        //        }
        //        //    }
        //        //    else
        //        //    {
        //        //        @ViewBag.message = "This Employee ID Is already Exist !! Please Enter The Unique Employee ID !!";
        //        //        Get_Set_Value();

        //        //        @ViewBag.message = "block";
        //        //        return View();

        //        //    }

        //        //}
        //        //else
        //        //{
        //        DAL_UserDetails dAL_UserDetails = new DAL_UserDetails();
        //        int userDetails = DAL_UserDetails.UpdateUserDetails(getuserdetails, _configuration);
        //        if (userDetails > 1)
        //        {
        //            @ViewBag.message = "Data Is Update Successfully !!";
        //            Get_Set_Value();
        //            ViewBag.none = "block";
        //            return View();
        //        }
        //        else
        //        {
        //            @ViewBag.message = "Data Is Not Saved Successfully !!";
        //            return View();
        //        }

        //        //}
        //    }
        //    else
        //    {
        //        return RedirectToRoute("home");
        //    }
        //}


        //------------------------------------------------------- End-------------------------------

        //------------------------------------------- SEARCH EMPLOYEE ID METHOD----------------------
        [HttpPost]
        public IActionResult searchempid(User_DetailsContent content)
        {
            if (HttpContext.Session.GetString("LoginID") != null)
            {
                string Emp_id = content.Str_Emp_ID;
                DAL_UserDetails _obj_ = new DAL_UserDetails();
                string data = _obj_.GetEmployeeID(Emp_id, _configuration);
                return Json(data);
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpPost]
        public IActionResult searchloginid(User_DetailsContent content)
        {
            if (HttpContext.Session.GetString("LoginID") != null)
            {
                string login_id = content.Str_Login_ID;
                DAL_UserDetails _obj_ = new DAL_UserDetails();
                string data = _obj_.GetloginID(login_id, _configuration);
                return Json(data);
            }
            else
            {
                return Redirect("/");
            }
        }


        //public string GetReportingList(string Id)
        //{
        //    string Rval = "";
        //    Get_UserDetails _obj = new Get_UserDetails();
        //    DataSet _ds = _obj.GetreportingList(Convert.ToInt32(Id));


        //    return JsonConvert.SerializeObject(_ds, Formatting.Indented);
        //}

        //--------------------------------------------end-----------------------------------------

        //----------------GET dESIGNATION,dEPARTMENT AND ACCESS-LEVEL VALUE USING DROPDOWN FIELD -----
        public IActionResult Get_Set_Value()
        {
            string login_ID = "";
            int loginID = 1;
            Designation_Details details = new Designation_Details();
            List<User_DetailsContent> designationContents = new List<User_DetailsContent>();
            designationContents = DAL_Common.GetDesignation(loginID, _configuration);
            designationContents.Insert(0, new User_DetailsContent { Int_Id = 0, Str_Designation = "---Select Designation---" });
            ViewBag.designation = designationContents;

            List<User_DetailsContent> departmentContents = new List<User_DetailsContent>();
            departmentContents = DAL_Common.GetDepartment(loginID, _configuration);
            departmentContents.Insert(0, new User_DetailsContent { Int_Id = 0, Str_Department = "-----Select Depatment---" });
            ViewBag.department = departmentContents;

            List<User_DetailsContent> access_level = new List<User_DetailsContent>();
            access_level = DAL_Common.GetAccessLevel(loginID, _configuration);
            access_level.Insert(0, new User_DetailsContent { Int_Id = 0, Str_Access_Level = "----- Select Access Level ---" });
            ViewBag.accesslevel = access_level;

            List<CompanyDetail> companydetails = new List<CompanyDetail>();
            companydetails = DAL_Common.GetCompanyDetails(login_ID , _configuration);
            companydetails.Insert(0, new CompanyDetail { Id = 0, CompName = "----- Choose Company Name ---" });
            ViewBag.companydetails = companydetails;

             List<Country> country=new List<Country>();
            country = DAL_Common.GetCountryList();
            country.Insert(0, new Country { Id = "", Name = "----- Select Country List ---" });
            ViewBag.country = country;

            List<Title> title = new List<Title>();
            title = DAL_Common.GetTitleList();
            title.Insert(0, new Title { Id = 0, Name = "----- Select Title ---" });
            ViewBag.Title = title;
            
            return View();
          



        }

        //------------------------------------------------- END--------------------------------------

        [HttpPost]
        public string ReportingList(User_DetailsContent content)
        {
            if(content.Int_ReportingTo != null)
            {
                int login_id = content.Int_ReportingTo;
                DAL_Common _obj = new DAL_Common();

                DataSet _ds = _obj.GetreportingList(Convert.ToInt32(login_id), _configuration);
                return JsonConvert.SerializeObject(_ds, Formatting.Indented);
            }
            else
            {
                int login_id = 0;
                DAL_Common _obj = new DAL_Common();
                DataSet _ds = _obj.GetreportingList(Convert.ToInt32(login_id), _configuration);
                return JsonConvert.SerializeObject(_ds, Formatting.Indented);
            }
           
        }

    }
}
