using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMCMS.PL.Models
{
    public class Menu
    {
        public string Title { get; set; }
        public List<MenuItem> Data { get; set; }
    }
}