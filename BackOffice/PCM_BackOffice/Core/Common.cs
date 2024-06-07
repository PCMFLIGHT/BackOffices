using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;



namespace Core
{
    public class DesignationContent
    {
        public int Int_ID {  get; set; }
        public string Str_Login_ID { get; set; }
        public string Str_Designation { get;set; }
        public string Str_Created_by {  get; set; } 
        public DateTime Date_Created_on { get;set; }
        public string Str_Modified_by { get; set; }
        public DateTime Date_Modified_on { get;set; }

        public void Insert(int v)
        {
            throw new NotImplementedException();
        }

        public void Insert(int v, DesignationContent designationContent)
        {
            throw new NotImplementedException();
        }

        public static implicit operator DesignationContent(List<DesignationContent> v)
        {
            throw new NotImplementedException();
        }
    }
    public class Designation_Details
    {
        public DesignationContent designation { get; set; }=null;
    }
    public class Country
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class Continent
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class RoleManagement
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
    }

    public class Currency
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string IsActive { get; set; }
        public string IsDeleted { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public class Status
    {
        public int Id { set; get; }
        public string Name { get; set; }
    }
    public class Title
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string IsActive { get; set; }
        public string IsDeleted { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

}
