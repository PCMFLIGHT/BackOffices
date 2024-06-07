using DAL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Core.UserDetails;
using System.Runtime.Remoting.Contexts;
using Microsoft.SqlServer.Server;

namespace Dal.UserDetails
{
    public class DAL_UserDetails
    {

        public static List<User_DetailsContent> GetUserDetails(Get_UserDetails userdetails, IConfiguration configuration)
        {
            List<User_DetailsContent> list = new List<User_DetailsContent>();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Emp_ID", SqlDbType.VarChar, 50);
            param[0].Value = userdetails.users.Str_Emp_ID;

            param[1] = new SqlParameter("@Real_Name", SqlDbType.VarChar, 50);
            param[1].Value = userdetails.users.Str_Real_Name;

            param[2] = new SqlParameter("@Pseudo_Name", SqlDbType.VarChar, 50);
            param[2].Value = userdetails.users.Str_Pseudo_Name;

            param[3] = new SqlParameter("@Role_ID", SqlDbType.VarChar, 50);
            param[3].Value = userdetails.users.Str_Designation
                ;

            param[4] = new SqlParameter("@Dept_ID", SqlDbType.VarChar, 50);
            param[4].Value = userdetails.users.Str_Department;

            using (SqlConnection con = GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration))
            {
                SqlDataReader dr = SqlHelper_1.ExecuteReader(con, CommandType.StoredProcedure, "sp_SELECT_EmpList_Details ", param);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        User_DetailsContent content = new User_DetailsContent();
                        content.Int_Id = Convert.ToInt32(dr["id"]);
                        content.Int_This_Status = Convert.ToInt32(dr["This_Status"]);
                        content.Str_Emp_ID = dr["Emp_ID"].ToString();
                        content.Str_Role_ID = dr["Role_ID"].ToString();
                        content.Str_Real_Name = dr["Real_Name"].ToString();
                        content.Str_Pseudo_Name = dr["Pseudo_Name"].ToString();
                        content.Str_Email_ID = dr["Email_ID"].ToString();
                        content.Str_Mobile_Num = dr["Mobile_Num"].ToString();
                        content.Str_Dept_ID = dr["Dept_ID"].ToString();
                        content.Str_Access_Level = dr["Access_Level"].ToString();
                        content.Str_Mng_Emp_ID = dr["Mng_Emp_ID"].ToString();
                        content.Str_TL_Emp_ID = dr["TL_Emp_ID"].ToString();
                        content.Int_Is_Active = Convert.ToInt32(dr["Is_Active"]);
                        content.Int_Created_by = Convert.ToInt32(dr["Created_by"]);
                        content.date_Created_on = Convert.ToDateTime(dr["Created_on"]);
                        list.Add(content);
                    }
                }
            }
            return list;
        }

        public static List<User_DetailsContent> TopOne_GetUserDetails(string id, IConfiguration configuration)
        {
            List<User_DetailsContent> lst = new List<User_DetailsContent>();
            DataSet _ds = new DataSet();
            try
            {
                using (SqlConnection con = GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration))
                {
                    SqlParameter[] param = new SqlParameter[30];
                    param[0] = new SqlParameter("@id", SqlDbType.VarChar, (50));
                    param[0].Value = id;

                    _ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "sp_SELECT_top1_EmpList", param);
                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                        {

                            string DOB = Convert.ToDateTime(_ds.Tables[0].Rows[i]["DOB"].ToString()).ToString("dd-MM-yyyy");
                            User_DetailsContent _obj = new User_DetailsContent();
                            _obj.SR_No = i + 1;
                            //_obj.Int_Id = int.Parse(_ds.Tables[0].Rows[i]["id"].ToString());
                           _obj.Str_EmpCode = _ds.Tables[0].Rows[i]["EmpCode"].ToString();
                           _obj.Str_Login_ID = _ds.Tables[0].Rows[i]["EmpCode"].ToString();
                           _obj.Str_UserName = _ds.Tables[0].Rows[i]["UserName"].ToString();
                           _obj.Str_Password = _ds.Tables[0].Rows[i]["Password"].ToString();
                           _obj.Str_Title = _ds.Tables[0].Rows[i]["Title"].ToString();
                           _obj.Str_FirstName = _ds.Tables[0].Rows[i]["FirstName"].ToString();
                           _obj.Str_MiddleName = _ds.Tables[0].Rows[i]["MiddleName"].ToString();
                           _obj.Str_LastName = _ds.Tables[0].Rows[i]["LastName"].ToString();
                           _obj.Str_Pseudo_Name = _ds.Tables[0].Rows[i]["NickName"].ToString();
                        
                           _obj.Str_Phone = _ds.Tables[0].Rows[i]["Phone"].ToString();
                           _obj.Str_Address = _ds.Tables[0].Rows[i]["Address"].ToString();
                           _obj.Str_Mobile_Num = _ds.Tables[0].Rows[i]["Mobile"].ToString();
                           _obj.Str_Email = _ds.Tables[0].Rows[i]["Email"].ToString();
                           _obj.Str_Email1 = _ds.Tables[0].Rows[i]["Email1"].ToString();
                           _obj.Str_ddlCountry = _ds.Tables[0].Rows[i]["Country"].ToString();
                           _obj.Str_State = _ds.Tables[0].Rows[i]["State"].ToString();
                           _obj.Str_City = _ds.Tables[0].Rows[i]["City"].ToString();
                           _obj.Str_PostCode = _ds.Tables[0].Rows[i]["PostCode"].ToString().Trim();
                           _obj.Str_Nationality = _ds.Tables[0].Rows[i]["Nationality"].ToString();
                           _obj.Int_ReportingTo = Convert.ToInt32(_ds.Tables[0].Rows[i]["ReportingTo"].ToString());
                           _obj.ddlCompany = _ds.Tables[0].Rows[i]["CompId"].ToString();
                           _obj.Str_Department = _ds.Tables[0].Rows[i]["DeptId"].ToString();
                           _obj.Str_Designation = _ds.Tables[0].Rows[i]["DesigId"].ToString();
                            _obj.Str_Access_Level = _ds.Tables[0].Rows[i]["RoleId"].ToString();
                            //_obj.users = _ds.Tables[0].Rows[i]["ModifiedByName"].ToString();
                            //_obj.user.date_ModifiedDate = DateTime.Parse(_ds.Tables[0].Rows[i]["ModifiedDate"].ToString());
                            lst.Add(_obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return lst;
        }

        public string Delete(string Id , IConfiguration configuration)
        {
            int Rval = 0;
            try
            {
                using (SqlConnection con = GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration))
                {
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@Counter", SqlDbType.VarChar, (50));
                    param[0].Value = "delete";
                    param[1] = new SqlParameter("@Id", SqlDbType.Int);
                    param[1].Value = Id;
                    Rval = SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "Usp_Employee_Delete", param);
                    return Rval.ToString();
                }
            }
            catch (Exception ex)
            {
                return Rval.ToString();
            }
        }


        public static List<User_DetailsContent> GetEmployeeDetails(Get_UserDetails userdetails,IConfiguration configuration)
        {
            List<User_DetailsContent> lst = new List<User_DetailsContent>();
            DataSet _ds = new DataSet();
            try
            {
                using (SqlConnection con = GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration))
                {
                    SqlParameter[] param = new SqlParameter[10];
                    param[0] = new SqlParameter("@Counter", SqlDbType.VarChar, (50));
                    param[0].Value = "select";
                    param[1] = new SqlParameter("@UserName", SqlDbType.VarChar, (50));
                    param[1].Value = userdetails.users.Str_UserName;
                    param[2] = new SqlParameter("@FirstName", SqlDbType.VarChar, (50));
                    param[2].Value = userdetails.users.Str_FirstName;

                    _ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "sp_SELECT_EmpList_1", param);
                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                        {

                            string DOB = Convert.ToDateTime(_ds.Tables[0].Rows[i]["DOB"].ToString()).ToString("dd-MM-yyyy");
                            User_DetailsContent _obj = new User_DetailsContent();
                            _obj.SR_No = i + 1;
                            _obj.Int_Id = int.Parse(_ds.Tables[0].Rows[i]["Id"].ToString());
                            _obj.Str_EmpCode = _ds.Tables[0].Rows[i]["EmpCode"].ToString();
                            _obj.Str_Login_ID = _ds.Tables[0].Rows[i]["EmpCode"].ToString();
                            _obj.Str_UserName = _ds.Tables[0].Rows[i]["UserName"].ToString();
                            _obj.Str_Password = _ds.Tables[0].Rows[i]["Password"].ToString();
                            _obj.Str_Title = _ds.Tables[0].Rows[i]["Title"].ToString();
                            _obj.Str_FirstName = _ds.Tables[0].Rows[i]["FirstName"].ToString();
                            _obj.Str_MiddleName = _ds.Tables[0].Rows[i]["MiddleName"].ToString();
                            _obj.Str_LastName = _ds.Tables[0].Rows[i]["LastName"].ToString();
                            _obj.Str_Pseudo_Name = _ds.Tables[0].Rows[i]["NickName"].ToString();
                            _obj.date_DOB = DOB;
                            _obj.Str_Phone = _ds.Tables[0].Rows[i]["Phone"].ToString();
                            _obj.Str_Address = _ds.Tables[0].Rows[i]["Address"].ToString();
                            _obj.Str_Mobile_Num = _ds.Tables[0].Rows[i]["Mobile"].ToString();
                            _obj.Str_Email = _ds.Tables[0].Rows[i]["Email"].ToString();
                            _obj.Str_Email1 = _ds.Tables[0].Rows[i]["Email1"].ToString();
                            _obj.Str_ddlCountry = _ds.Tables[0].Rows[i]["Country"].ToString();
                            _obj.Str_State = _ds.Tables[0].Rows[i]["State"].ToString();
                            _obj.Str_City = _ds.Tables[0].Rows[i]["City"].ToString();
                            _obj.Str_PostCode = _ds.Tables[0].Rows[i]["PostCode"].ToString().Trim();
                            _obj.Str_Nationality = _ds.Tables[0].Rows[i]["Nationality"].ToString();
                            _obj.Int_ReportingTo = Convert.ToInt32(_ds.Tables[0].Rows[i]["ReportingTo"].ToString());
                            _obj.ddlCompany = _ds.Tables[0].Rows[i]["CompName"].ToString();
                            _obj.Str_Department = _ds.Tables[0].Rows[i]["DepName"].ToString();
                            _obj.Str_Designation = _ds.Tables[0].Rows[i]["DesigName"].ToString();
                            _obj.bool_ValidateIP = Convert.ToBoolean(_ds.Tables[0].Rows[i]["ValidateIP"].ToString());
                            _obj.bool_IsManager = Convert.ToBoolean(_ds.Tables[0].Rows[i]["IsManager"].ToString());
                            _obj.bool_IsActive = Convert.ToBoolean(_ds.Tables[0].Rows[i]["IsActive"].ToString());
                            _obj.Int_ModifiedBy = int.Parse(_ds.Tables[0].Rows[i]["ModifiedBy"].ToString());
                            //_obj.users = _ds.Tables[0].Rows[i]["ModifiedByName"].ToString();
                            //_obj.user.date_ModifiedDate = DateTime.Parse(_ds.Tables[0].Rows[i]["ModifiedDate"].ToString());
                            lst.Add(_obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return lst;
        }


        public Get_UserDetails GetEmployee(Get_UserDetails _objemp, IConfiguration configuration)
        {

            List<Get_UserDetails> lst = new List<Get_UserDetails>();
            Get_UserDetails _obj = new Get_UserDetails();
            DataSet _ds = new DataSet();
            try
            {
                using (SqlConnection con = GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration))
                {
                    SqlParameter[] param = new SqlParameter[3];
                    param[0] = new SqlParameter("@Counter", SqlDbType.VarChar, (50));
                    param[0].Value = "selected";
                    param[1] = new SqlParameter("@Id", SqlDbType.Int);
                    param[1].Value = _objemp.users.Int_Id;

                    _ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "Usp_EmployeeDetails", param);
                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < 1; i++)
                        {

                            _obj.users.date_DOB = Convert.ToDateTime(_ds.Tables[0].Rows[i]["DOB"].ToString()).ToString("dd-MM-yyyy");
                            _obj.users.SR_No = i + 1;
                            _obj.users.Int_Id = int.Parse(_ds.Tables[0].Rows[i]["Id"].ToString());
                            _obj.users.Str_EmpCode = _ds.Tables[0].Rows[i]["EmpCode"].ToString();
                            _obj.users.Str_Login_ID = _ds.Tables[0].Rows[i]["EmpCode"].ToString();
                            _obj.users.Str_UserName = _ds.Tables[0].Rows[i]["UserName"].ToString();
                            _obj.users.Str_Password = _ds.Tables[0].Rows[i]["Password"].ToString();
                            _obj.users.Str_Title = _ds.Tables[0].Rows[i]["TitleName"].ToString();
                            _obj.users.Str_FirstName = _ds.Tables[0].Rows[i]["FirstName"].ToString();
                            _obj.users.Str_MiddleName = _ds.Tables[0].Rows[i]["MiddleName"].ToString();
                            _obj.users.Str_LastName = _ds.Tables[0].Rows[i]["LastName"].ToString();
                            _obj.users.Str_Pseudo_Name = _ds.Tables[0].Rows[i]["NickName"].ToString();
                            _obj.users.Str_Phone = _ds.Tables[0].Rows[i]["Phone"].ToString();
                            _obj.users.Str_Address = _ds.Tables[0].Rows[i]["Address"].ToString();
                            _obj.users.Str_Mobile_Num = _ds.Tables[0].Rows[i]["Mobile"].ToString();
                            _obj.users.Str_Email = _ds.Tables[0].Rows[i]["Email"].ToString();
                            _obj.users.Str_Email1 = _ds.Tables[0].Rows[i]["Email1"].ToString();
                            _obj.users.Str_ddlCountry = _ds.Tables[0].Rows[i]["Country"].ToString();
                            _obj.users.Str_State = _ds.Tables[0].Rows[i]["State"].ToString();
                            _obj.users.Str_City = _ds.Tables[0].Rows[i]["City"].ToString();
                            _obj.users.Str_PostCode = _ds.Tables[0].Rows[i]["PostCode"].ToString().Trim();
                            _obj.users.Str_Nationality = _ds.Tables[0].Rows[i]["Nationality"].ToString();
                            _obj.users.Int_ReportingTo = Convert.ToInt32(_ds.Tables[0].Rows[i]["ReportingTo"].ToString());
                            _obj.users.ddlCompany = _ds.Tables[0].Rows[i]["CompName"].ToString();
                            _obj.users.Str_Department = _ds.Tables[0].Rows[i]["DepName"].ToString();
                            _obj.users.Str_Designation = _ds.Tables[0].Rows[i]["DesigName"].ToString();
                            _obj.users.bool_ValidateIP = Convert.ToBoolean(_ds.Tables[0].Rows[i]["ValidateIP"].ToString());
                            _obj.users.bool_IsManager = Convert.ToBoolean(_ds.Tables[0].Rows[i]["IsManager"].ToString());
                            _obj.users.bool_IsActive = Convert.ToBoolean(_ds.Tables[0].Rows[i]["IsActive"].ToString());
                            _obj.users.Int_ModifiedBy = int.Parse(_ds.Tables[0].Rows[i]["ModifiedBy"].ToString());
                            _obj.users.date_ModifiedDate = DateTime.Parse(_ds.Tables[0].Rows[i]["ModifiedDate"].ToString());
                            lst.Add(_obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return _obj;
        }

     


        public static Get_UserDetails GetSetUserDetails(string id, IConfiguration configuration)
        {
            Get_UserDetails gettheme = new Get_UserDetails();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@EmpCode", SqlDbType.VarChar,50);
            param[0].Value = id;

            using (SqlConnection con = GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration))
            {
                DataSet _ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "sp_SELECT_Emp_List_Details", param);

                if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {

                    for(int i = 0; i < 1; i++)
                    {
                        gettheme.users = new User_DetailsContent();

                        gettheme.users.date_DOB = Convert.ToDateTime(_ds.Tables[0].Rows[i]["DOB"].ToString()).ToString("dd-MM-yyyy");
                        gettheme.users.Int_Id = int.Parse(_ds.Tables[0].Rows[i]["Id"].ToString());
                        gettheme.users.Str_EmpCode = _ds.Tables[0].Rows[i]["EmpCode"].ToString();
                        gettheme.users.Str_Login_ID = _ds.Tables[0].Rows[i]["EmpCode"].ToString();
                        gettheme.users.Str_UserName = _ds.Tables[0].Rows[i]["UserName"].ToString();
                        gettheme.users.Str_Password = _ds.Tables[0].Rows[i]["Password"].ToString();
                        gettheme.users.Str_Title = _ds.Tables[0].Rows[i]["Title"].ToString();
                        gettheme.users.Str_FirstName = _ds.Tables[0].Rows[i]["FirstName"].ToString();
                        gettheme.users.Str_MiddleName = _ds.Tables[0].Rows[i]["MiddleName"].ToString();
                        gettheme.users.Str_LastName = _ds.Tables[0].Rows[i]["LastName"].ToString();
                        gettheme.users.Str_Pseudo_Name = _ds.Tables[0].Rows[i]["NickName"].ToString();
                    
                        gettheme.users.Str_Phone = _ds.Tables[0].Rows[i]["Phone"].ToString();
                        gettheme.users.Str_Address = _ds.Tables[0].Rows[i]["Address"].ToString();
                        gettheme.users.Str_Mobile_Num = _ds.Tables[0].Rows[i]["Mobile"].ToString();
                        gettheme.users.Str_Email = _ds.Tables[0].Rows[i]["Email"].ToString();
                        gettheme.users.Str_Email1 = _ds.Tables[0].Rows[i]["Email1"].ToString();
                        gettheme.users.Str_ddlCountry = _ds.Tables[0].Rows[i]["Country"].ToString();
                        gettheme.users.Str_State = _ds.Tables[0].Rows[i]["State"].ToString();
                        gettheme.users.Str_City = _ds.Tables[0].Rows[i]["City"].ToString();
                        gettheme.users.Str_PostCode = _ds.Tables[0].Rows[i]["PostCode"].ToString().Trim();
                        gettheme.users.Str_Nationality = _ds.Tables[0].Rows[i]["Nationality"].ToString();
                        gettheme.users.Int_ReportingTo = Convert.ToInt32(_ds.Tables[0].Rows[i]["ReportingTo"].ToString());
                        gettheme.users.ddlCompany = _ds.Tables[0].Rows[i]["CompId"].ToString();
                        gettheme.users.Str_Department = _ds.Tables[0].Rows[i]["DeptId"].ToString();
                        gettheme.users.Str_Designation = _ds.Tables[0].Rows[i]["DesigId"].ToString();
                        gettheme.users.Str_Access_Level = _ds.Tables[0].Rows[i]["RoleId"].ToString();
                        
                        //gettheme.users.bool_ValidateIP = Convert.ToBoolean(_ds.Tables[0].Rows[i]["ValidateIP"].ToString());
                        //gettheme.users.bool_IsManager = Convert.ToBoolean(_ds.Tables[0].Rows[i]["IsManager"].ToString());
                        gettheme.users.bool_IsActive = Convert.ToBoolean(_ds.Tables[0].Rows[i]["IsActive"].ToString());
                        gettheme.users.Int_ModifiedBy = int.Parse(_ds.Tables[0].Rows[i]["ModifiedBy"].ToString());
                        gettheme.users.date_ModifiedDate = DateTime.Parse(_ds.Tables[0].Rows[i]["ModifiedDate"].ToString());

                    }
                }
            }
            return gettheme;

        }

       
        public static int SaveUserDetails(Get_UserDetails frm, IConfiguration configuration)
        {
            SqlParameter[] param = new SqlParameter[32];

            param[0] = new SqlParameter("@Id", SqlDbType.Int);
            param[0].Value = frm.users.Int_Id;
            if (!string.IsNullOrEmpty(frm.users.Str_EmpCode))
            {
                param[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar, (50));
                param[1].Value = frm.users.Str_EmpCode;
            }
            if (!string.IsNullOrEmpty(frm.users.Str_UserName))
            {
                param[2] = new SqlParameter("@UserName", SqlDbType.VarChar, (50));
                param[2].Value = frm.users.Str_UserName;
            }
            if (!string.IsNullOrEmpty(frm.users.Str_Password))
            {
                param[3] = new SqlParameter("@Password", SqlDbType.VarChar, (50));
                param[3].Value = frm.users.Str_Password;
            }

            param[4] = new SqlParameter("@Title", SqlDbType.Int);
            param[4].Value = Convert.ToInt32(frm.users.Str_Title);


            if (!string.IsNullOrEmpty(frm.users.Str_FirstName))
            {
                param[5] = new SqlParameter("@FirstName", SqlDbType.VarChar, (50));
                param[5].Value = frm.users.Str_FirstName;
            }
            if (!string.IsNullOrEmpty(frm.users.Str_MiddleName))
            {
                param[6] = new SqlParameter("@MiddleName", SqlDbType.VarChar, (50));
                param[6].Value = frm.users.Str_MiddleName;
            }
            if (!string.IsNullOrEmpty(frm.users.Str_LastName))
            {
                param[7] = new SqlParameter("@LastName", SqlDbType.VarChar, (50));
                param[7].Value = frm.users.Str_LastName;
            }
            if (!string.IsNullOrEmpty(frm.users.Str_Pseudo_Name))
            {
                param[8] = new SqlParameter("@NickName", SqlDbType.VarChar, (50));
                param[8].Value = frm.users.Str_Pseudo_Name;
            }
            //if (!string.IsNullOrEmpty(frm.users.date_DOB)))
            //{
                param[9] = new SqlParameter("@DOB", SqlDbType.DateTime);
                param[9].Value = Convert.ToDateTime(frm.users.date_DOB);
            //}
            if (!string.IsNullOrEmpty(frm.users.Str_Phone))
            {
                param[10] = new SqlParameter("@Phone", SqlDbType.VarChar, (50));
                param[10].Value = frm.users.Str_Phone;
            }
            if (!string.IsNullOrEmpty(frm.users.Str_Mobile_Num))
            {
                param[11] = new SqlParameter("@Mobile", SqlDbType.VarChar, (50));
                param[11].Value = frm.users.Str_Mobile_Num;
            }
            if (!string.IsNullOrEmpty(frm.users.Str_Email))
            {
                param[12] = new SqlParameter("@Email", SqlDbType.VarChar, (50));
                param[12].Value = frm.users.Str_Email;
            }

            if (!string.IsNullOrEmpty(frm.users.Str_Nationality))
            {
                param[13] = new SqlParameter("@Nationality", SqlDbType.VarChar, (50));
                param[13].Value = frm.users.Str_Nationality;
            }
            if (!string.IsNullOrEmpty(frm.users.Str_Address))
            {
                param[14] = new SqlParameter("@Address", SqlDbType.VarChar, (200));
                param[14].Value = frm.users.Str_Address;
            }
            if (!string.IsNullOrEmpty(frm.users.Str_ddlCountry))
            {
                param[15] = new SqlParameter("@Country", SqlDbType.VarChar, (200));
                param[15].Value = frm.users.Str_ddlCountry;
            }
            if (!string.IsNullOrEmpty(frm.users.Str_City))
            {
                param[16] = new SqlParameter("@City", SqlDbType.VarChar, (50));
                param[16].Value = frm.users.Str_City;
            }
            if (!string.IsNullOrEmpty(frm.users.Str_PostCode))
            {
                param[17] = new SqlParameter("@PostCode", SqlDbType.VarChar, (50));
                param[17].Value = frm.users.Str_PostCode;
            }
            if (!string.IsNullOrEmpty(frm.users.Str_State))
            {
                param[18] = new SqlParameter("@State", SqlDbType.VarChar, (50));
                param[18].Value = frm.users.Str_State;
            }
            if (frm.users.Int_ReportingTo > 0)
            {
                param[19] = new SqlParameter("@ReportingTo", SqlDbType.Int);
                param[19].Value = frm.users.Int_ReportingTo;
            }
            else
            {
                param[19] = new SqlParameter("@ReportingTo", SqlDbType.Int);
                param[19].Value = -1;
            }
            if (frm.users.Int_CompId > 0)
            {
                param[20] = new SqlParameter("@CompId", SqlDbType.Int);
                param[20].Value = frm.users.Int_CompId;
            }
            else
            {
                param[20] = new SqlParameter("@CompId", SqlDbType.Int);
                param[20].Value = -1;
            }
            param[21] = new SqlParameter("@DeptId", SqlDbType.Int);
            param[21].Value = frm.users.Str_Department;
            param[22] = new SqlParameter("@DesigId", SqlDbType.Int);
            param[22].Value = frm.users.Str_Designation;
            param[23] = new SqlParameter("@UserTypeId", SqlDbType.Int);
            param[23].Value = 0;
            param[24] = new SqlParameter("@ValidateIP", SqlDbType.Bit);
            param[24].Value = frm.users.bool_ValidateIP;
            param[25] = new SqlParameter("@IsManager", SqlDbType.Bit);
            param[25].Value = frm.users.bool_IsManager;

            param[26] = new SqlParameter("@IsActive", SqlDbType.Bit);
            param[26].Value = 1;
            param[27] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
            param[27].Value = 0;
            param[28] = new SqlParameter("@Counter", SqlDbType.VarChar, 50);
            param[28].Value = "insert";
            param[29] = new SqlParameter("@RoleId", SqlDbType.Int);
            param[29].Value = frm.users.Str_Access_Level;
            // 
            param[30] = new SqlParameter("@CompIds", SqlDbType.NVarChar, 250);
            param[30].Value = "-1";
            if (!string.IsNullOrEmpty(frm.users.Str_Email1))
            {
                param[31] = new SqlParameter("@Email1", SqlDbType.VarChar, (50));
                param[31].Value = frm.users.Str_Email1;
            }
            using (SqlConnection con = GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration))
            {
                return SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "Usp_EmployeeDetails", param);
            }

        } 
        public static int UpdateUserDetails(Get_UserDetails frm, IConfiguration configuration)
        {
            SqlParameter[] param = new SqlParameter[32];

            param[0] = new SqlParameter("@Id", SqlDbType.Int);
            param[0].Value = frm.users.Int_Id;
            if (!string.IsNullOrEmpty(frm.users.Str_EmpCode))
            {
                param[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar, (50));
                param[1].Value = frm.users.Str_EmpCode;
            }
            if (!string.IsNullOrEmpty(frm.users.Str_UserName))
            {
                param[2] = new SqlParameter("@UserName", SqlDbType.VarChar, (50));
                param[2].Value = frm.users.Str_UserName;
            }
            if (!string.IsNullOrEmpty(frm.users.Str_Password))
            {
                param[3] = new SqlParameter("@Password", SqlDbType.VarChar, (50));
                param[3].Value = frm.users.Str_Password;
            }

            param[4] = new SqlParameter("@Title", SqlDbType.Int);
            param[4].Value = Convert.ToInt32(frm.users.Str_Title);


            if (!string.IsNullOrEmpty(frm.users.Str_FirstName))
            {
                param[5] = new SqlParameter("@FirstName", SqlDbType.VarChar, (50));
                param[5].Value = frm.users.Str_FirstName;
            }
            if (!string.IsNullOrEmpty(frm.users.Str_MiddleName))
            {
                param[6] = new SqlParameter("@MiddleName", SqlDbType.VarChar, (50));
                param[6].Value = frm.users.Str_MiddleName;
            }
            if (!string.IsNullOrEmpty(frm.users.Str_LastName))
            {
                param[7] = new SqlParameter("@LastName", SqlDbType.VarChar, (50));
                param[7].Value = frm.users.Str_LastName;
            }
            if (!string.IsNullOrEmpty(frm.users.Str_Pseudo_Name))
            {
                param[8] = new SqlParameter("@NickName", SqlDbType.VarChar, (50));
                param[8].Value = frm.users.Str_Pseudo_Name;
            }
            //if (!string.IsNullOrEmpty(frm.users.date_DOB)))
            //{
            param[9] = new SqlParameter("@DOB", SqlDbType.VarChar ,50);
            param[9].Value = Convert.ToDateTime(frm.users.date_DOB);
            //}
            if (!string.IsNullOrEmpty(frm.users.Str_Phone))
            {
                param[10] = new SqlParameter("@Phone", SqlDbType.VarChar, (50));
                param[10].Value = frm.users.Str_Phone;
            }
            if (!string.IsNullOrEmpty(frm.users.Str_Mobile_Num))
            {
                param[11] = new SqlParameter("@Mobile", SqlDbType.VarChar, (50));
                param[11].Value = frm.users.Str_Mobile_Num;
            }
            if (!string.IsNullOrEmpty(frm.users.Str_Email))
            {
                param[12] = new SqlParameter("@Email", SqlDbType.VarChar, (50));
                param[12].Value = frm.users.Str_Email;
            }

            if (!string.IsNullOrEmpty(frm.users.Str_Nationality))
            {
                param[13] = new SqlParameter("@Nationality", SqlDbType.VarChar, (50));
                param[13].Value = frm.users.Str_Nationality;
            }
            if (!string.IsNullOrEmpty(frm.users.Str_Address))
            {
                param[14] = new SqlParameter("@Address", SqlDbType.VarChar, (200));
                param[14].Value = frm.users.Str_Address;
            }
            if (!string.IsNullOrEmpty(frm.users.Str_ddlCountry))
            {
                param[15] = new SqlParameter("@Country", SqlDbType.VarChar, (200));
                param[15].Value = frm.users.Str_ddlCountry;
            }
            if (!string.IsNullOrEmpty(frm.users.Str_City))
            {
                param[16] = new SqlParameter("@City", SqlDbType.VarChar, (50));
                param[16].Value = frm.users.Str_City;
            }
            if (!string.IsNullOrEmpty(frm.users.Str_PostCode))
            {
                param[17] = new SqlParameter("@PostCode", SqlDbType.VarChar, (50));
                param[17].Value = frm.users.Str_PostCode;
            }
            if (!string.IsNullOrEmpty(frm.users.Str_State))
            {
                param[18] = new SqlParameter("@State", SqlDbType.VarChar, (50));
                param[18].Value = frm.users.Str_State;
            }
            if (frm.users.Int_ReportingTo > 0)
            {
                param[19] = new SqlParameter("@ReportingTo", SqlDbType.Int);
                param[19].Value = frm.users.Int_ReportingTo;
            }
            else
            {
                param[19] = new SqlParameter("@ReportingTo", SqlDbType.Int);
                param[19].Value = -1;
            }
            if (frm.users.Int_CompId > 0)
            {
                param[20] = new SqlParameter("@CompId", SqlDbType.Int);
                param[20].Value = frm.users.Int_CompId;
            }
            else
            {
                param[20] = new SqlParameter("@CompId", SqlDbType.Int);
                param[20].Value = -1;
            }
            param[21] = new SqlParameter("@DeptId", SqlDbType.Int);
            param[21].Value = frm.users.Str_Department;
            param[22] = new SqlParameter("@DesigId", SqlDbType.Int);
            param[22].Value = frm.users.Str_Designation;
            param[23] = new SqlParameter("@UserTypeId", SqlDbType.Int);
            param[23].Value = 0;
            param[24] = new SqlParameter("@ValidateIP", SqlDbType.Bit);
            param[24].Value = frm.users.bool_ValidateIP;
            param[25] = new SqlParameter("@IsManager", SqlDbType.Bit);
            param[25].Value = frm.users.bool_IsManager;

            param[26] = new SqlParameter("@IsActive", SqlDbType.Bit);
            param[26].Value = 1;
            param[27] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
            param[27].Value = 0;
            param[28] = new SqlParameter("@Counter", SqlDbType.VarChar, 50);
            param[28].Value = "insert";
            param[29] = new SqlParameter("@RoleId", SqlDbType.Int);
            param[29].Value = frm.users.Str_Access_Level;
            // 
            param[30] = new SqlParameter("@CompIds", SqlDbType.NVarChar, 250);
            param[30].Value = "-1";
            if (!string.IsNullOrEmpty(frm.users.Str_Email1))
            {
                param[31] = new SqlParameter("@Email1", SqlDbType.VarChar, (50));
                param[31].Value = frm.users.Str_Email1;
            }
            using (SqlConnection con = GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration))
            {
                return SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "SP_UPDATE_User_Details", param);
            }
        }
        public string GetEmployeeID(string Emp_ID, IConfiguration configuration)
        {
            string Employeee_ID = "";
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Emp_ID", SqlDbType.VarChar, 200);
                param[0].Value = Emp_ID;
                using (SqlConnection con = GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration))
                {
                    SqlDataReader dr = SqlHelper_1.ExecuteReader(con, CommandType.StoredProcedure, "sp_SELECT_Get_Emp_ID", param);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Employeee_ID = dr["Emp_ID"].ToString();
                        }
                    }
                }
                return Employeee_ID;
            }
            catch (Exception ex)
            {
                return Employeee_ID;
            }

        }
        public string GetloginID(string loginid, IConfiguration configuration)
        {
            string Login_ID = "";
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Login_ID", SqlDbType.VarChar, 200);
                param[0].Value = loginid;
                using (SqlConnection con = GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration))
                {
                    SqlDataReader dr = SqlHelper_1.ExecuteReader(con, CommandType.StoredProcedure, "sp_SELECT_Get_login_ID", param);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Login_ID = dr["Login_ID"].ToString();
                        }
                    }
                }
                return Login_ID;
            }
            catch (Exception ex)
            {
                return Login_ID;

            }

        }
        public string GetExistUsers(string Emp_ID, int is_active , int this_status,  IConfiguration configuration)
        {
            string Employeee_ID = "";
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Emp_ID", SqlDbType.VarChar, 200);
                param[0].Value = Emp_ID;
                param[0] = new SqlParameter("@Is_Active", SqlDbType.Int);
                param[0].Value = is_active;
                param[0] = new SqlParameter("@This_Status", SqlDbType.Int);
                param[0].Value = this_status;
                using (SqlConnection con = GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration))
                {
                    SqlDataReader dr = SqlHelper_1.ExecuteReader(con, CommandType.StoredProcedure, "sp_SELECT_Get_ExistEmpID", param);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Employeee_ID = dr["Emp_ID"].ToString();
                        }
                    }
                }
                return Employeee_ID;
            }
            catch (Exception ex)
            {
                return Employeee_ID;
            }

        }

    }
}
