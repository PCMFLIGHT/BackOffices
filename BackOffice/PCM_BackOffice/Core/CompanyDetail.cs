
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Core
{ 
    
    public class CompanyListDetail
    {
        List<CompanyDetail> companylist = new List<CompanyDetail>();
    }

    public class CompanyDetail
    {
       
        public int Id { get; set; }
        public string CompName { get; set; }
        public string ParentCompany { get; set; }
        public string Code { get; set; }
        public string AccountNumber { get; set; }
        public string IATACode { get; set; }
        public string Currency { get; set; }
        public string CurrencyName { get; set; }
        public string Email { get; set; }
        public string Email1 { get; set; }
        public string Phone { get; set; }
        public string Phone1 { get; set; }
        public string Fax { get; set; }
        public int ParentId { get; set; }
        public string PrefixCode { get; set; }
        public string Address { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        [DataType(DataType.PostalCode)]
        public string PostCode { get; set; }
        public string State { get; set; }
        public string Website { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedByName { get; set;}
        public DateTime ModifiedDate { get; set; }
        public string Operation { get; set; }
        public int SN { get; set; }
        public string Action { get; set; }
      
             
    }
}