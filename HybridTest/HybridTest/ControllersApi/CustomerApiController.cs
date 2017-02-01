using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Script.Serialization;

namespace HybridTest.ControllersApi
{

    public class CustomerApiController : ApiController
    {
        private prodigiousEntities db = new prodigiousEntities();
        [HttpGet]
        public List<HybridTest.Models.Customer> Customer()
        {
            List<HybridTest.Models.Customer> oList = new List<HybridTest.Models.Customer>();

            db.Customer.ToList().All
                (x =>
                {
                    oList.Add(new Models.Customer()
                    {
                        CustomerId = x.CustomerID,
                        NameStyle = x.NameStyle,
                        Title = x.Title,
                        FirstName = x.FirstName,
                        MiddleName = x.MiddleName,
                        LastName = x.LastName,
                        Suffix = x.Suffix,
                        CompanyName = x.CompanyName,
                        SalesPerson = x.SalesPerson,                       
                        EmailAddress = x.EmailAddress,
                        Phone = x.Phone,
                        PasswordHash =x.PasswordHash,
                        PasswordSalt =x.PasswordSalt,
                        rowguid = x.rowguid,
                        ModifiedDate = x.ModifiedDate
                    });
                    return true;
                });

            return oList;
        }

        [HttpPost]
        public void UpdateCustomer(string update)
        {
            List<HybridTest.Models.Customer> oCustomer = new JavaScriptSerializer().Deserialize<List<HybridTest.Models.Customer>>(System.Web.HttpContext.Current.Request["models"]);
            oCustomer.All(x =>
            {
                db.Entry(new HybridTest.Customer()
                {
                    CustomerID = x.CustomerId,
                    NameStyle = x.NameStyle,
                    Title = x.Title,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    Suffix = x.Suffix,
                    CompanyName = x.CompanyName,
                    SalesPerson = x.SalesPerson,
                    EmailAddress = x.EmailAddress,
                    Phone = x.Phone,
                    PasswordHash = x.PasswordHash,
                    PasswordSalt = x.PasswordSalt,
                    rowguid = x.rowguid,
                    ModifiedDate = x.ModifiedDate
                }).State = EntityState.Modified;
                return true;
            });            
            db.SaveChanges();            
        }
        [HttpPost]
        public void CreateCustomer(string create)
        {
            List<HybridTest.Models.Customer> oCustomer = new JavaScriptSerializer().Deserialize<List<HybridTest.Models.Customer>>(System.Web.HttpContext.Current.Request["models"]);
            oCustomer.All(x =>
            {
                db.Entry(new HybridTest.Customer()
                {
                    CustomerID = x.CustomerId,
                    NameStyle = x.NameStyle,
                    Title = x.Title,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    Suffix = x.Suffix,
                    CompanyName = x.CompanyName,
                    SalesPerson = x.SalesPerson,
                    EmailAddress = x.EmailAddress,
                    Phone = x.Phone,
                    PasswordHash = x.PasswordHash,
                    PasswordSalt = x.PasswordSalt,
                    rowguid = x.rowguid,
                    ModifiedDate = x.ModifiedDate
                }).State = EntityState.Added;
                return true;
            });
            db.SaveChanges();
        }
    }
   
}
