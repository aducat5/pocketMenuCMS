using PMCMS.DAL;
using System.Collections.Generic;
namespace PMCMS.PL.Models
{
    public class SellerPackage
    {
        public string SellerName { get; set; }
        public string SellerJSON { get; set; }
        public List<string> Data { get; set; }
    }
}