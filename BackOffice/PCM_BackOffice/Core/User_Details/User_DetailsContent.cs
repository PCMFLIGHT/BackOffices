using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UserDetails
{

    public class Get_UserDetails
    {
        public List<User_DetailsContent> user { get; set; }
        public User_DetailsContent users { get; set; }


    }
   
    public class User_DetailsContent
    {
        public int Int_Id { get; set; }
        public int SR_No { get; set; }
        public string Str_OldEmp_ID { get; set; }
        public string Str_Designation { get; set; }
        public string Str_Department { get; set; }
        public int Int_This_Status {  get; set; }
        public string Str_Emp_ID { get; set; }
        public string Str_Login_ID { get; set; }
        //public string Str_Password { get; set; }
        public string Str_Role_ID { get; set; }
        public string Str_Real_Name { get; set; }
        public string Str_Pseudo_Name { get; set; }
        public string Str_Email_ID { get; set; }
        public string Str_Mobile_Num { get; set; }
        public string Str_Dept_ID { get; set; }
        public string Str_Access_Level { get; set; }
        public string Str_Mng_Emp_ID { get; set; }
        public string Str_TL_Emp_ID { get; set; }
        public int Int_Is_Active { get; set; }
        public int Int_IP_Check { get; set; }
        public int Int_Created_by { get; set; }
        public DateTime date_Created_on { get; set; } = DateTime.Now;
        public int Int_Modified_by { get; set;}
        public DateTime date_Modified_on { get; set;}

//----------------------------------------------------------- new-----------------------------------------------------
     public string Str_EmpCode    { get; set; }
        public string ddlCompany {  get; set; }
     //public string Str_OldEmp_ID { get; set; }
     public string Str_UserName   { get; set; }
     public string Str_Password   { get; set; }
     public string Str_Title      { get; set; }
     public string Str_FirstName  { get; set; }
     public string Str_MiddleName { get; set; }
     public string Str_LastName { get; set; }
     public string Str_NickName { get; set; }
     public string date_DOB { get; set; }
     public string Str_Phone { get; set; }
     public string Str_Mobile { get; set; }
     public string Str_Email { get; set; }
     public string Str_Nationality{ get; set; }
     public string Str_Address{ get; set; }
     public string Str_Country { get; set; }
     public string Str_State { get; set; }
     public string Str_City { get; set; }
     public string Str_PostCode   { get; set; }
     public int Int_ReportingTo{ get; set; }
     public int Int_CompId { get; set; }
     public int Int_UserTypeId { get; set; }
     public int Int_DeptId { get; set; }
     public int Int_DesigId { get; set; }
     public bool bool_ValidateIP { get; set; }
     public bool bool_IsManager  { get; set; }
     public bool bool_IsActive   { get; set; }
     public bool bool_IsDeleted  { get; set; }
     public DateTime date_ModifiedDate { get; set; }
     public int Int_ModifiedBy { get; set; }
     public int Int_RoleId { get; set; }
     public string Str_Email1 { get; set; }
     public string Str_ddlTitle { get; set; }
     public string Str_ddlCountry { get; set; }





    }
}
