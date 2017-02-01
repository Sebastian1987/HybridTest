using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HybridTest.Models
{
    public class CustomerAddressModel
    {
        public HybridTest.Models.CustomerModel Customer {get; set;}
        public int AddressId { get; set; }
        public string AddressType { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }                
    }
}