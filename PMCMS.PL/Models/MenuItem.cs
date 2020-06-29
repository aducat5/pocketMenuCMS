namespace PMCMS.PL.Models
{
    public class MenuItem
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string ImageUrl { get; set; }
        public bool DetailVisible { get; set; } = false;
    }
}