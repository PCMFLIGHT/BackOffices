using System;
namespace Core.Home
{
    public class Emp_Content_Details
    {
        public int Int_ID { get; set; }
        public string Str_Emp_ID { get; set; } 
        public string Str_Password { get; set; } = string.Empty;
        public string Str_Login_ID { get; set; } = string.Empty;
        Caching _objCaching = new Caching();
        public DesignationContent designation { get; set; }
        //DAL_Common _obj = new DAL_Common();

        //lstCompany = _objCaching.GetCompanyDetails();
        //    lstTitle = _objCaching.GetTitleList();
        //    lstStatus = _objCaching.GetStatusList();
        //    lstCountry = _objCaching.GetCountryList();
        //    lstReportingTo = _objCaching.GetReportingToList();
        //    lstDept = _objdb.GetDepartmentList();
        //    lstDesig = _objdb.GetDesignationList();

    }
}
