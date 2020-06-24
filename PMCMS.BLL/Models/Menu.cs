using System.Collections.Generic;

namespace PMCMS.BLL.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}