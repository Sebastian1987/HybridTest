using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace HybridTest.ControllersApi
{

    public class CustomerApiController : ApiController
    {    
        [HttpGet]
        public List<HybridTest.Models.CustomerModel> Customer(string id)
        {
            List<HybridTest.Models.CustomerModel> oList = new List<HybridTest.Models.CustomerModel>();

            if (!string.IsNullOrEmpty(id))
            {
                using (prodigiousEntities db = new prodigiousEntities())
                {
                    var Customer = db.Customer.ToList().Where
                         (x => x.CustomerID == int.Parse(id)).Select(x => x).FirstOrDefault();

                    if (Customer != null)
                    {
                        oList.Add(new Models.CustomerModel()
                        {
                            CustomerId = Customer.CustomerID,
                            NameStyle = Customer.NameStyle,
                            Title = Customer.Title,
                            FirstName = Customer.FirstName,
                            MiddleName = Customer.MiddleName,
                            LastName = Customer.LastName,
                            Suffix = Customer.Suffix,
                            CompanyName = Customer.CompanyName,
                            SalesPerson = Customer.SalesPerson,
                            EmailAddress = Customer.EmailAddress,
                            Phone = Customer.Phone,
                            PasswordHash = Customer.PasswordHash,
                            PasswordSalt = Customer.PasswordSalt,
                            rowguid = Customer.rowguid,
                            ModifiedDate = Customer.ModifiedDate,
                        });
                    }
                    
                }
            }
            else
            {
                using (prodigiousEntities db = new prodigiousEntities())
                {

                    db.Customer.ToList().All
                        (x =>
                        {
                            oList.Add(new Models.CustomerModel()
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
                                PasswordHash = x.PasswordHash,
                                PasswordSalt = x.PasswordSalt,
                                rowguid = x.rowguid,
                                ModifiedDate = x.ModifiedDate,
                            });
                            return true;
                        });
                }
            }
            return oList.OrderByDescending(x => x.ModifiedDate).ToList();
        }

        [HttpPost]
        public void UpdateCustomer(string update)
        {

            List<HybridTest.Models.CustomerModel> oCustomer = new JavaScriptSerializer().Deserialize<List<HybridTest.Models.CustomerModel>>(System.Web.HttpContext.Current.Request["models"]);
            using (prodigiousEntities db = new prodigiousEntities())
            {
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
        }
        [HttpPost]
        public void CreateCustomer(string create)
        {
            try
            {
                List<HybridTest.Models.CustomerModel> oCustomer = new JavaScriptSerializer().Deserialize<List<HybridTest.Models.CustomerModel>>(System.Web.HttpContext.Current.Request["models"]);
                using (prodigiousEntities db = new prodigiousEntities())
                {
                    var oToInsert = db.Set<Customer>();
                    oCustomer.All(x =>
                    {
                        db.Entry(new HybridTest.Customer()
                        {
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
                            rowguid = Guid.NewGuid(),
                            PasswordHash = HybridTest.Controllers.CustomerController.ProcessPassword(),
                            PasswordSalt = HybridTest.Controllers.CustomerController.ProcessPassword(),
                            ModifiedDate = DateTime.Now
                        }).State = EntityState.Added;
                        return true;
                    });

                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        [HttpPost]
        public void DeleteCustomer(string Delete)
        {
            List<HybridTest.Models.CustomerModel> oCustomer = new JavaScriptSerializer().Deserialize<List<HybridTest.Models.CustomerModel>>(System.Web.HttpContext.Current.Request["models"]);
            using (prodigiousEntities db = new prodigiousEntities())
            {
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
                    }).State = EntityState.Deleted;
                    return true;
                });

                db.SaveChanges();
            }
        }
    }

}
