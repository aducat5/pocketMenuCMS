using System.Collections.Generic;

namespace PMCMS.BLL.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public List<Menu> Menus { get; set; }
    }
}