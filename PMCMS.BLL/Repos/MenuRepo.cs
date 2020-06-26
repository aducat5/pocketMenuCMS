

using PMCMS.DAL;
using System.Collections.Generic;
using System.Linq;

namespace PMCMS.BLL.Repos
{
    public class MenuRepo
    {
        private static PocketMenuDBEntities db = DBTool.GetInstance();

        public List<Menu> GetMenus()
        {
            return db.Menus.ToList();
        }
        public List<Menu> GetMenus(int sellerId)
        {
            return db.Menus.Where(menu => menu.SellerID == sellerId).ToList();
        }
        public List<Menu> GetMenus(int sellerId, bool isDeleted)
        {
            return db.Menus.Where(menu => menu.SellerID == sellerId && menu.IsDeleted == isDeleted).ToList();
        }
    }
}
