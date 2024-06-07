
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System;


namespace DAL.Login
{
    public class DAL_Login
    {   
        public string ReturnPassword(string Login_ID, IConfiguration configuration)
        {
            string password = "";
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Login_ID", SqlDbType.VarChar, 200);
                param[0].Value = Login_ID;
                using (SqlConnection con = GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration))
                {   
                    SqlDataReader dr = SqlHelper_1.ExecuteReader(con, CommandType.StoredProcedure, "sp_SELECT_Emp_Credential", param);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            password = dr["Password"].ToString();
                        }
                    }

                }
                return password;
            }
            catch (Exception ex)
            {
                return password;
            }

        }


        public string ReturnNickName(string Login_ID, IConfiguration configuration)
        {
            string password = "";
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Login_ID", SqlDbType.VarChar, 200);
                param[0].Value = Login_ID;
                using (SqlConnection con = GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration))
                {
                    SqlDataReader dr = SqlHelper_1.ExecuteReader(con, CommandType.StoredProcedure, "sp_SELECT_Emp_Credential", param);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            password = dr["NickName"].ToString();
                        }
                    }

                }
                return password;
            }
            catch (Exception ex)
            {
                return password;
            }

        }


    }
}
