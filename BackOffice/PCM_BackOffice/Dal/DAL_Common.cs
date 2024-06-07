using Core.UserDetails;
using DAL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Dal
{
    public class DAL_Common
    { 
    
    
       // ---------------------------------------------- Designation -----------------------------------------------------
        public static List<User_DetailsContent> GetDesignation(int loginid, IConfiguration configuration)
        {
            List<User_DetailsContent> list = new List<User_DetailsContent>();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@id", SqlDbType.VarChar, 50);
            param[0].Value = loginid;

            using (SqlConnection con = GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration))
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(con, CommandType.StoredProcedure, "sp_SELECT_Emp_Designation ", param);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        User_DetailsContent content = new User_DetailsContent();
                        content.Int_Id = Convert.ToInt32(dr["id"]);
                        content.Str_Designation = dr["Designation"].ToString();
                        list.Add(content);
                    }
                }
            }
            return list;
        }
         //--------------------------------------------- End---------------------------------------------------------
       // -------------------------------------------------- Department-----------------------------------------------
        public static List<User_DetailsContent> GetDepartment(int loginid, IConfiguration configuration)
        {
            List<User_DetailsContent> list = new List<User_DetailsContent>();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ID", SqlDbType.VarChar, 50);
            param[0].Value = loginid;

            using (SqlConnection con = GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration))
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(con, CommandType.StoredProcedure, "sp_SELECT_Emp_Department ", param);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        User_DetailsContent content = new User_DetailsContent();
                        content.Int_Id = Convert.ToInt32(dr["id"]);
                        content.Str_Department = dr["Department_name"].ToString();
                        list.Add(content);
                    }
                }
            }
            return list;
        }
         //--------------------------------------------- End------------------------------------------------------------
        //---------------------------------------------- Access-Level -----------------------------------------------------
        public static List<User_DetailsContent> GetAccessLevel(int loginid, IConfiguration configuration)
        {
            List<User_DetailsContent> list = new List<User_DetailsContent>();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Login_ID", SqlDbType.VarChar, 50);
            param[0].Value = loginid;

            using (SqlConnection con = GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration))
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(con, CommandType.StoredProcedure, "sp_SELECT_Accesslevel ", param);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        User_DetailsContent content = new User_DetailsContent();
                        content.Int_Id = Convert.ToInt32(dr["id"]);
                        content.Str_Access_Level = dr["Access_level"].ToString();
                        list.Add(content);
                    }
                }
            }
            return list;
        }
         //--------------------------------------------- End---------------------------------------------------------
        public DataSet GetreportingList(string loginid, IConfiguration configuration)
        {
            DataSet Ds = new DataSet();
            try
            {
                using (SqlConnection con = GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration))
                {
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@Id", SqlDbType.VarChar, (50));
                    param[0].Value = loginid;

                    Ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "sp_SELECT_Accesslevel", param);

                    return Ds;
                }
            }
            catch (Exception ex)
            {
                return Ds;
            }
        }

     

        public static List<CompanyDetail> GetCompanyDetails(string login_id, IConfiguration configuration)
        {
            List<CompanyDetail> lst = new List<CompanyDetail>();
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Login_ID", SqlDbType.VarChar, 50);
            param[0].Value = login_id;
            dt = SqlHelper.ExecuteSPReturnDT(new object[] { "Usp_CompanyDetails", "@Counter", "select" }, GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration));
            foreach (DataRow dr in dt.Rows)
            {
                CompanyDetail _objComp = new CompanyDetail();
                _objComp.Id = Convert.ToInt32(dr["Id"]);
                _objComp.CompName = Convert.ToString(dr["Name"]);
                _objComp.Code = Convert.ToString(dr["Code"]);
                _objComp.Currency = Convert.ToString(dr["Currency"]);
                _objComp.Email = Convert.ToString(dr["Email"]);
                _objComp.Email1 = Convert.ToString(dr["Email1"]);
                _objComp.Phone = Convert.ToString(dr["Phone"]);
                _objComp.Phone1 = Convert.ToString(dr["Phone1"]);
                _objComp.ParentId = Convert.ToInt32(dr["ParentId"]);
                _objComp.PrefixCode = Convert.ToString(dr["PrefixCode"]);
                _objComp.Address = Convert.ToString(dr["Address"]);
                _objComp.Address1 = Convert.ToString(dr["Address1"]);
                _objComp.City = Convert.ToString(dr["City"]);
                _objComp.PostCode = Convert.ToString(dr["PostCode"]);
                _objComp.State = Convert.ToString(dr["State"]);
                _objComp.Website = Convert.ToString(dr["Website"]);
                _objComp.IsActive = Convert.ToBoolean((dr["IsActive"] == DBNull.Value ? true : dr["IsActive"]));
                _objComp.ModifiedBy = Convert.ToInt32(dr["ModifiedBy"]);
                _objComp.ModifiedByName = Convert.ToString(dr["ModifiedByName"]);

                lst.Add(_objComp);
            }
            return lst;
        }
        //public List<RoleManagement> GetUserRoleList(IConfiguration configuration)
        //{
        //    List<RoleManagement> lst = new List<RoleManagement>();
        //    DataTable dt = new DataTable();
        //    dt = SqlHelper.ExecuteSPReturnDT(new object[] { "Usp_GetuserRoleList", "@Counter", "select" }, GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration));

        //    lst.Add(new UserType { Id = "ANY", Name = "ANY" });
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        RoleManagement _objPro = new RoleManagement();
        //        _objPro.RoleId = Convert.ToInt32(dr["RoleId"]);
        //        _objPro.RoleName = Convert.ToString(dr["RoleName"]);
        //        lst.Add(_objPro);
        //    }
        //    return lst;

        //}
        public static List<Country> GetCountryList()
        {
            List<Country> _country = new List<Country>();
            _country.Add(new Country { Id = "ANY", Name = "ANY" });
            _country.Add(new Country { Id = "AF", Name = "Afghanistan" });
            _country.Add(new Country { Id = "AX", Name = "Aland Islands" });
            _country.Add(new Country { Id = "AL", Name = "Albania" });
            _country.Add(new Country { Id = "DZ", Name = "Algeria" });
            _country.Add(new Country { Id = "AS", Name = "American Samoa" });
            _country.Add(new Country { Id = "AD", Name = "Andorra" });
            _country.Add(new Country { Id = "AO", Name = "Angola" });
            _country.Add(new Country { Id = "AI", Name = "Anguilla" });
            _country.Add(new Country { Id = "AQ", Name = "Antarctica" });
            _country.Add(new Country { Id = "AG", Name = "Antigua and Barbuda" });
            _country.Add(new Country { Id = "AR", Name = "Argentina" });
            _country.Add(new Country { Id = "AM", Name = "Armenia" });
            _country.Add(new Country { Id = "AW", Name = "Aruba" });
            _country.Add(new Country { Id = "AU", Name = "Australia" });
            _country.Add(new Country { Id = "AT", Name = "Austria" });
            _country.Add(new Country { Id = "AZ", Name = "Azerbaijan" });
            _country.Add(new Country { Id = "BS", Name = "Bahamas" });
            _country.Add(new Country { Id = "BH", Name = "Bahrain" });
            _country.Add(new Country { Id = "BD", Name = "Bangladesh" });
            _country.Add(new Country { Id = "BB", Name = "Barbados" });
            _country.Add(new Country { Id = "BY", Name = "Belarus" });
            _country.Add(new Country { Id = "BE", Name = "Belgium" });
            _country.Add(new Country { Id = "BZ", Name = "Belize" });
            _country.Add(new Country { Id = "BJ", Name = "Benin" });
            _country.Add(new Country { Id = "BM", Name = "Bermuda" });
            _country.Add(new Country { Id = "BT", Name = "Bhutan" });
            _country.Add(new Country { Id = "BO", Name = "Bolivia" });
            _country.Add(new Country { Id = "BA", Name = "Bosnia and Herzegovina" });
            _country.Add(new Country { Id = "BW", Name = "Botswana" });
            _country.Add(new Country { Id = "BV", Name = "Bouvet Island" });
            _country.Add(new Country { Id = "BR", Name = "Brazil" });
            _country.Add(new Country { Id = "IO", Name = "British Indian Ocean Territory" });
            _country.Add(new Country { Id = "BN", Name = "Brunei Darussalam" });
            _country.Add(new Country { Id = "BG", Name = "Bulgaria" });
            _country.Add(new Country { Id = "BF", Name = "Burkina Faso" });
            _country.Add(new Country { Id = "BI", Name = "Burundi" });
            _country.Add(new Country { Id = "KH", Name = "Cambodia" });
            _country.Add(new Country { Id = "CM", Name = "Cameroon" });
            _country.Add(new Country { Id = "CA", Name = "Canada" });
            _country.Add(new Country { Id = "CV", Name = "Cape Verde" });
            _country.Add(new Country { Id = "KY", Name = "Cayman Islands" });
            _country.Add(new Country { Id = "CF", Name = "Central African Republic" });
            _country.Add(new Country { Id = "TD", Name = "Chad" });
            _country.Add(new Country { Id = "CL", Name = "Chile" });
            _country.Add(new Country { Id = "CN", Name = "China" });
            _country.Add(new Country { Id = "CX", Name = "Christmas Island" });
            _country.Add(new Country { Id = "CC", Name = "Cocos (Keeling) Islands" });
            _country.Add(new Country { Id = "CO", Name = "Colombia" });
            _country.Add(new Country { Id = "KM", Name = "Comoros" });
            _country.Add(new Country { Id = "CG", Name = "Congo" });
            _country.Add(new Country { Id = "CD", Name = "Congo The Democratic Republic of the" });
            _country.Add(new Country { Id = "CK", Name = "Cook Islands" });
            _country.Add(new Country { Id = "CR", Name = "Costa Rica" });
            _country.Add(new Country { Id = "CI", Name = "Côte d'Ivoire" });
            _country.Add(new Country { Id = "HR", Name = "Croatia" });
            _country.Add(new Country { Id = "CU", Name = "Cuba" });
            _country.Add(new Country { Id = "CY", Name = "Cyprus" });
            _country.Add(new Country { Id = "CZ", Name = "Czech Republic" });
            _country.Add(new Country { Id = "DK", Name = "Denmark" });
            _country.Add(new Country { Id = "DJ", Name = "Djibouti" });
            _country.Add(new Country { Id = "DM", Name = "Dominica" });
            _country.Add(new Country { Id = "DO", Name = "Dominican Republic" });
            _country.Add(new Country { Id = "EC", Name = "Ecuador" });
            _country.Add(new Country { Id = "EG", Name = "Egypt" });
            _country.Add(new Country { Id = "SV", Name = "El Salvador" });
            _country.Add(new Country { Id = "GQ", Name = "Equatorial Guinea" });
            _country.Add(new Country { Id = "ER", Name = "Eritrea" });
            _country.Add(new Country { Id = "EE", Name = "Estonia" });
            _country.Add(new Country { Id = "ET", Name = "Ethiopia" });
            _country.Add(new Country { Id = "FK", Name = "Falkland Islands (Malvinas)" });
            _country.Add(new Country { Id = "FO", Name = "Faroe Islands" });
            _country.Add(new Country { Id = "FJ", Name = "Fiji" });
            _country.Add(new Country { Id = "FI", Name = "Finland" });
            _country.Add(new Country { Id = "FR", Name = "France" });
            _country.Add(new Country { Id = "GF", Name = "French Guiana" });
            _country.Add(new Country { Id = "PF", Name = "French Polynesia" });
            _country.Add(new Country { Id = "TF", Name = "French Southern Territories" });
            _country.Add(new Country { Id = "GA", Name = "Gabon" });
            _country.Add(new Country { Id = "GM", Name = "Gambia" });
            _country.Add(new Country { Id = "GE", Name = "Georgia" });
            _country.Add(new Country { Id = "DE", Name = "Germany" });
            _country.Add(new Country { Id = "GH", Name = "Ghana" });
            _country.Add(new Country { Id = "GI", Name = "Gibraltar" });
            _country.Add(new Country { Id = "GR", Name = "Greece" });
            _country.Add(new Country { Id = "GL", Name = "Greenland" });
            _country.Add(new Country { Id = "GD", Name = "Grenada" });
            _country.Add(new Country { Id = "GP", Name = "Guadeloupe" });
            _country.Add(new Country { Id = "GU", Name = "Guam" });
            _country.Add(new Country { Id = "GT", Name = "Guatemala" });
            _country.Add(new Country { Id = "GG", Name = "Guernsey" });
            _country.Add(new Country { Id = "GN", Name = "Guinea" });
            _country.Add(new Country { Id = "GW", Name = "Guinea-Bissau" });
            _country.Add(new Country { Id = "GY", Name = "Guyana" });
            _country.Add(new Country { Id = "HT", Name = "Haiti" });
            _country.Add(new Country { Id = "HM", Name = "Heard Island and McDonald Islands" });
            _country.Add(new Country { Id = "VA", Name = "Holy See (Vatican City State)" });
            _country.Add(new Country { Id = "HN", Name = "Honduras" });
            _country.Add(new Country { Id = "HK", Name = "Hong Kong" });
            _country.Add(new Country { Id = "HU", Name = "Hungary" });
            _country.Add(new Country { Id = "IS", Name = "Iceland" });
            _country.Add(new Country { Id = "IN", Name = "India" });
            _country.Add(new Country { Id = "ID", Name = "Indonesia" });
            _country.Add(new Country { Id = "IR", Name = "Iran Islamic Republic of" });
            _country.Add(new Country { Id = "IQ", Name = "Iraq" });
            _country.Add(new Country { Id = "IE", Name = "Ireland" });
            _country.Add(new Country { Id = "IM", Name = "Isle of Man" });
            _country.Add(new Country { Id = "IL", Name = "Israel" });
            _country.Add(new Country { Id = "IT", Name = "Italy" });
            _country.Add(new Country { Id = "JM", Name = "Jamaica" });
            _country.Add(new Country { Id = "JP", Name = "Japan" });
            _country.Add(new Country { Id = "JE", Name = "Jersey" });
            _country.Add(new Country { Id = "JO", Name = "Jordan" });
            _country.Add(new Country { Id = "KZ", Name = "Kazakhstan" });
            _country.Add(new Country { Id = "KE", Name = "Kenya" });
            _country.Add(new Country { Id = "KI", Name = "Kiribati" });
            _country.Add(new Country { Id = "KP", Name = "Korea Democratic People's Republic of" });
            _country.Add(new Country { Id = "KR", Name = "Korea Republic of" });
            _country.Add(new Country { Id = "KW", Name = "Kuwait" });
            _country.Add(new Country { Id = "KG", Name = "Kyrgyzstan" });
            _country.Add(new Country { Id = "LA", Name = "Lao People's Democratic Republic" });
            _country.Add(new Country { Id = "LV", Name = "Latvia" });
            _country.Add(new Country { Id = "LB", Name = "Lebanon" });
            _country.Add(new Country { Id = "LS", Name = "Lesotho" });
            _country.Add(new Country { Id = "LR", Name = "Liberia" });
            _country.Add(new Country { Id = "LY", Name = "Libyan Arab Jamahiriya" });
            _country.Add(new Country { Id = "LI", Name = "Liechtenstein" });
            _country.Add(new Country { Id = "LT", Name = "Lithuania" });
            _country.Add(new Country { Id = "LU", Name = "Luxembourg" });
            _country.Add(new Country { Id = "MO", Name = "Macao" });
            _country.Add(new Country { Id = "MK", Name = "Macedonia The Former Yugoslav Republic of" });
            _country.Add(new Country { Id = "MG", Name = "Madagascar" });
            _country.Add(new Country { Id = "MW", Name = "Malawi" });
            _country.Add(new Country { Id = "MY", Name = "Malaysia" });
            _country.Add(new Country { Id = "MV", Name = "Maldives" });
            _country.Add(new Country { Id = "ML", Name = "Mali" });
            _country.Add(new Country { Id = "MT", Name = "Malta" });
            _country.Add(new Country { Id = "MH", Name = "Marshall Islands" });
            _country.Add(new Country { Id = "MQ", Name = "Martinique" });
            _country.Add(new Country { Id = "MR", Name = "Mauritania" });
            _country.Add(new Country { Id = "MU", Name = "Mauritius" });
            _country.Add(new Country { Id = "MT", Name = "Mayotte" });
            _country.Add(new Country { Id = "MX", Name = "Mexico" });
            _country.Add(new Country { Id = "FM", Name = "Microneia Federated States of" });
            _country.Add(new Country { Id = "MD", Name = "Moldova" });
            _country.Add(new Country { Id = "MC", Name = "Monaco" });
            _country.Add(new Country { Id = "MN", Name = "Mongolia" });
            _country.Add(new Country { Id = "ME", Name = "Montenegro" });
            _country.Add(new Country { Id = "MS", Name = "Montserrat" });
            _country.Add(new Country { Id = "MA", Name = "Morocco" });
            _country.Add(new Country { Id = "MZ", Name = "Mozambique" });
            _country.Add(new Country { Id = "MM", Name = "Myanmar" });
            _country.Add(new Country { Id = "NA", Name = "Namibia" });
            _country.Add(new Country { Id = "NR", Name = "Nauru" });
            _country.Add(new Country { Id = "NP", Name = "Nepal" });
            _country.Add(new Country { Id = "NL", Name = "Netherlands" });
            _country.Add(new Country { Id = "AN", Name = "Netherlands Antilles" });
            _country.Add(new Country { Id = "NC", Name = "New Caledonia" });
            _country.Add(new Country { Id = "NZ", Name = "New Zealand" });
            _country.Add(new Country { Id = "NI", Name = "Nicaragua" });
            _country.Add(new Country { Id = "NE", Name = "Niger" });
            _country.Add(new Country { Id = "NG", Name = "Nigeria" });
            _country.Add(new Country { Id = "NU", Name = "Niue" });
            _country.Add(new Country { Id = "NF", Name = "Norfolk Island" });
            _country.Add(new Country { Id = "MP", Name = "Northern Mariana Islands" });
            _country.Add(new Country { Id = "NO", Name = "Norway" });
            _country.Add(new Country { Id = "OM", Name = "Oman" });
            _country.Add(new Country { Id = "PK", Name = "Pakistan" });
            _country.Add(new Country { Id = "PW", Name = "Palau" });
            _country.Add(new Country { Id = "PS", Name = "Palestinian Territory Occupied" });
            _country.Add(new Country { Id = "PA", Name = "Panama" });
            _country.Add(new Country { Id = "PG", Name = "Papua New Guinea" });
            _country.Add(new Country { Id = "PY", Name = "Paraguay" });
            _country.Add(new Country { Id = "PE", Name = "Peru" });
            _country.Add(new Country { Id = "PH", Name = "Philippines" });
            _country.Add(new Country { Id = "PN", Name = "Pitcairn" });
            _country.Add(new Country { Id = "PL", Name = "Poland" });
            _country.Add(new Country { Id = "PT", Name = "Portugal" });
            _country.Add(new Country { Id = "PR", Name = "Puerto Rico" });
            _country.Add(new Country { Id = "QA", Name = "Qatar" });
            _country.Add(new Country { Id = "RE", Name = "Réunion" });
            _country.Add(new Country { Id = "RO", Name = "Romania" });
            _country.Add(new Country { Id = "RU", Name = "Russian Federation" });
            _country.Add(new Country { Id = "RW", Name = "Rwanda" });
            _country.Add(new Country { Id = "BL", Name = "Saint Barthélemy" });
            _country.Add(new Country { Id = "SH", Name = "Saint Helena" });
            _country.Add(new Country { Id = "KN", Name = "Saint Kitts and Nevis" });
            _country.Add(new Country { Id = "LC", Name = "Saint Lucia" });
            _country.Add(new Country { Id = "MF", Name = "Saint Martin" });
            _country.Add(new Country { Id = "PM", Name = "Saint Pierre and Miquelon" });
            _country.Add(new Country { Id = "VC", Name = "Saint Vincent and the Grenadines" });
            _country.Add(new Country { Id = "WS", Name = "Samoa" });
            _country.Add(new Country { Id = "SM", Name = "San Marino" });
            _country.Add(new Country { Id = "ST", Name = "Sao Tome and Principe" });
            _country.Add(new Country { Id = "SA", Name = "Saudi Arabia" });
            _country.Add(new Country { Id = "SN", Name = "Senegal" });
            _country.Add(new Country { Id = "RS", Name = "Serbia" });
            _country.Add(new Country { Id = "SC", Name = "Seychelles" });
            _country.Add(new Country { Id = "SL", Name = "Sierra Leone" });
            _country.Add(new Country { Id = "SG", Name = "Singapore" });
            _country.Add(new Country { Id = "SK", Name = "Slovakia" });
            _country.Add(new Country { Id = "SI", Name = "Slovenia" });
            _country.Add(new Country { Id = "SB", Name = "Solomon Islands" });
            _country.Add(new Country { Id = "SO", Name = "Somalia" });
            _country.Add(new Country { Id = "ZA", Name = "South Africa" });
            _country.Add(new Country { Id = "GS", Name = "South Georgia and the South Sandwich Islands" });
            _country.Add(new Country { Id = "ES", Name = "Spain" });
            _country.Add(new Country { Id = "LK", Name = "Sri Lanka" });
            _country.Add(new Country { Id = "SD", Name = "Sudan" });
            _country.Add(new Country { Id = "SR", Name = "Suriname" });
            _country.Add(new Country { Id = "SJ", Name = "Svalbard and Jan Mayen" });
            _country.Add(new Country { Id = "SZ", Name = "Swaziland" });
            _country.Add(new Country { Id = "SE", Name = "Sweden" });
            _country.Add(new Country { Id = "CH", Name = "Switzerland" });
            _country.Add(new Country { Id = "SY", Name = "Syrian Arab Republic" });
            _country.Add(new Country { Id = "TW", Name = "Taiwan Province of China" });
            _country.Add(new Country { Id = "TJ", Name = "Tajikistan" });
            _country.Add(new Country { Id = "TZ", Name = "Tanzania United Republic of" });
            _country.Add(new Country { Id = "TH", Name = "Thailand" });
            _country.Add(new Country { Id = "TL", Name = "Timor-Leste" });
            _country.Add(new Country { Id = "TG", Name = "Togo" });
            _country.Add(new Country { Id = "TK", Name = "Tokelau" });
            _country.Add(new Country { Id = "TO", Name = "Tonga" });
            _country.Add(new Country { Id = "TT", Name = "Trinidad and Tobago" });
            _country.Add(new Country { Id = "TN", Name = "Tunisia" });
            _country.Add(new Country { Id = "TR", Name = "Turkey" });
            _country.Add(new Country { Id = "TM", Name = "Turkmenistan" });
            _country.Add(new Country { Id = "TC", Name = "Turks and Caicos Islands" });
            _country.Add(new Country { Id = "TV", Name = "Tuvalu" });
            _country.Add(new Country { Id = "UG", Name = "Uganda" });
            _country.Add(new Country { Id = "UA", Name = "Ukraine" });
            _country.Add(new Country { Id = "AE", Name = "United Arab Emirates" });
            _country.Add(new Country { Id = "GB", Name = "United Kingdom" });
            _country.Add(new Country { Id = "US", Name = "United States" });
            _country.Add(new Country { Id = "UM", Name = "United States Minor Outlying Islands" });
            _country.Add(new Country { Id = "UY", Name = "Uruguay" });
            _country.Add(new Country { Id = "UZ", Name = "Uzbekistan" });
            _country.Add(new Country { Id = "VU", Name = "Vanuatu" });
            _country.Add(new Country { Id = "VE", Name = "Venezuela" });
            _country.Add(new Country { Id = "VN", Name = "Viet Nam" });
            _country.Add(new Country { Id = "VG", Name = "Virgin Islands British" });
            _country.Add(new Country { Id = "VI", Name = "Virgin Islands U.S." });
            _country.Add(new Country { Id = "WF", Name = "Wallis and Futuna" });
            _country.Add(new Country { Id = "EH", Name = "Western Sahara" });
            _country.Add(new Country { Id = "YE", Name = "Yemen" });
            _country.Add(new Country { Id = "ZM", Name = "Zambia" });
            _country.Add(new Country { Id = "ZW", Name = "Zimbabwe" });
            return _country;
        }
        public  List<Continent> GetContinentList()
        {
            List<Continent> _continent = new List<Continent>();
            _continent.Add(new Continent { Id = "ANY", Name = "ANY" });
            _continent.Add(new Continent { Id = "AFR", Name = "AFRICA" });
            _continent.Add(new Continent { Id = "ASI", Name = "ASIA" });
            _continent.Add(new Continent { Id = "AUS", Name = "Australia / OCEANIA" });
            _continent.Add(new Continent { Id = "CAR", Name = "Caribbean" });
            _continent.Add(new Continent { Id = "CHN", Name = "China" });
            _continent.Add(new Continent { Id = "EUR", Name = "EUROPE" });
            _continent.Add(new Continent { Id = "FRE", Name = "Far East" });
            _continent.Add(new Continent { Id = "MEA", Name = "M.East" });
            _continent.Add(new Continent { Id = "NAM", Name = "N.AMERICA" });
            _continent.Add(new Continent { Id = "SAM", Name = "S.AMERICA" });

            return _continent;
        }

        public static List<Title> GetTitleList()
        {
            List<Title> title = new List<Title>();
            title.Add(new Title { Id = 0, Name = "select Title" });
            title.Add(new Title { Id = 1, Name = "Mr" });
            title.Add(new Title { Id = 2, Name = "Mrs" });
            title.Add(new Title { Id = 3, Name = "Miss" });
            
            return title;
        }
        public List<Currency> GetCurrencyList(IConfiguration configuration)
        {
            List<Currency> lst = new List<Currency>();
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteSPReturnDT(new object[] { "Usp_GetCurrency", "@Counter", "select" }, GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration));
            foreach (DataRow dr in dt.Rows)
            {
                Currency _objPro = new Currency();
                _objPro.Id = Convert.ToString(dr["Id"]);
                _objPro.Code = Convert.ToString(dr["Code"]);
                _objPro.Name = Convert.ToString(dr["Name"]) + "(" + Convert.ToString(dr["Symbol"]) + ")";
                _objPro.Symbol = Convert.ToString(dr["Symbol"]);
                lst.Add(_objPro);
            }
            return lst;

        }

        public List<Status> GetStatusList()
        {
            List<Status> _JourneyType = new List<Status>();
            _JourneyType.Add(new Status { Id = 1, Name = "Active" });
            _JourneyType.Add(new Status { Id = 0, Name = "InActive" });
            return _JourneyType;
        }
        public List<Status> GetTypeList()
        {
            List<Status> _JourneyType = new List<Status>();
            _JourneyType.Add(new Status { Id = 1, Name = "Offline" });
            _JourneyType.Add(new Status { Id = 2, Name = "Online" });
            return _JourneyType;
        }

        public static List<Title> GetTitleList(string loginid , IConfiguration configuration)
        {
            List<Title> lst = new List<Title>();
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteSPReturnDT(new object[] { "sp_SELECT_Emp_Title", "@Counter", "select" }, GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration));
            foreach (DataRow dr in dt.Rows)
            {
                Title _objPro = new Title();
                _objPro.Id = Convert.ToInt32(dr["Id"]);
                _objPro.Code = Convert.ToString(dr["Code"]);
                _objPro.Name = Convert.ToString(dr["Name"]);
                lst.Add(_objPro);
            }
            return lst;

        }
        public string GetMessage(string Operation, int Rval)
        {
            string Msg = "";
            switch (Operation)
            {
                case "insert":
                    if (Rval >= 1)
                        Msg = "Data inserted successfully!";
                    else if (Rval == -1)
                        Msg = "Data already exists!";
                    else
                        Msg = "Some error occured!";
                    break;
                case "update":
                    if (Rval >= 1)
                        Msg = "Data updated successfully!";
                    else if (Rval == -1)
                        Msg = "Data already exists!";
                    else
                        Msg = "Some error occured!";
                    break;
                case "updateselected":
                    if (Rval >= 1)
                        Msg = "Data updated successfully!";
                    else if (Rval == -1)
                        Msg = "Data already exists!";
                    else
                        Msg = "Some error occured!";
                    break;

            }
            return Msg;
        }

        public DataSet GetreportingList(int Id , IConfiguration configuration)
        {
            DataSet Ds = new DataSet();
            try
            {
                using (SqlConnection con = GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration))
                {
                    SqlParameter[] param = new SqlParameter[2];

                    param[0] = new SqlParameter("@Counter", SqlDbType.VarChar, (50));
                    param[0].Value = "Select";

                    param[1] = new SqlParameter("@Id", SqlDbType.Int);
                    param[1].Value = Id;
                    Ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "Usp_ReportingList", param);

                    return Ds;
                }
            }
            catch (Exception ex)
            {
                return Ds;
            }
        }

        public DataSet GetreportingList_1(string desig, int Id, IConfiguration configuration)
        {
            DataSet Ds = new DataSet();
            try
            {
                using (SqlConnection con = GetDbConnections.FnGetSqlConnection_DbPcmBackOffice(configuration))
                {
                    SqlParameter[] param = new SqlParameter[2];

                    param[0] = new SqlParameter("@Counter", SqlDbType.VarChar, (50));
                    param[0].Value = "Select";

                    param[1] = new SqlParameter("@Id", SqlDbType.Int);
                    param[1].Value = Id;

                    param[1] = new SqlParameter("@Reporting_To_Id", SqlDbType.Int);
                    param[1].Value = desig;
                    Ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "Usp_ReportingList_1", param);

                    return Ds;
                }
            }
            catch (Exception ex)
            {
                return Ds;
            }
        }

    }
}
