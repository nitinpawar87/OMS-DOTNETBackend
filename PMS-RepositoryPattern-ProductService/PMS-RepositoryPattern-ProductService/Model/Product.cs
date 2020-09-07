using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_RepositoryPattern.Model
{
    public class Product
    {
        public int  Id{ get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public string ProductFamily { get; set; }
        public bool ProductAvaiability{ get; set; }
        public string ProductVersion{ get; set; }
    }
}
