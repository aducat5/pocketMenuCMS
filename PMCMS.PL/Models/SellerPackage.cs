using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMCMS.PL.Models
{
    public class SellerPackage
    {
        public string SellerName { get; set; }
        public string SellerJSON { get; set; }
        public List<string> MenuJSONs { get; set; }
    }
}