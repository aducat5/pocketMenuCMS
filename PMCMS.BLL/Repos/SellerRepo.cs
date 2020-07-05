using PMCMS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PMCMS.BLL.Repos
{
    public class SellerRepo
    {
        private static PocketMenuDBEntities db = DBTool.GetInstance();
        public bool CheckSeller(int id)
        {
            using (var db = DBTool.GetNewInstance())
            {
                return db.Sellers.Any(seller => seller.SellerID == id); 
            }
        }

        public bool CheckSeller(string name)
        {
            return db.Sellers.Any(seller => seller.SellerName == name);
        }

        public Seller GetSeller(int id)
        {
            using (var db = DBTool.GetNewInstance())
            {
                Seller seller = null;
                if (CheckSeller(id))
                {
                    seller = db.Sellers.Find(id);
                    seller.Menus = db.Menus.Where(menu => menu.SellerID == seller.SellerID).ToList();
                }

                return seller; 
            }
        }
        public Seller InsertSeller(Seller newSeller, out string result)
        {
            newSeller.CreateDate = DateTime.Now;
            newSeller.UpdateDate = DateTime.Now;

            try
            {
                if (CheckSeller(newSeller.SellerName))
                {
                    result = "This name is already in use.";
                    return null;
                }
                else
                {
                    db.Sellers.Add(newSeller);
                    if (db.SaveChanges() > 0)
                    {
                        result = "A new restaurant created.";
                        return newSeller;
                    }
                    else
                    {
                        result = "There is a problem with the save process.";
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                result = "there is an unknown error" + ex.Message;
                return null;
            }
        }
        public bool DeleteSeller(int id, out string result)
        {
            bool sonuc = false;
            if (CheckSeller(id))
            {
                Seller seller = GetSeller(id);
                seller.IsDeleted = true;
                try
                {
                    sonuc = db.SaveChanges() > 0;
                    if (sonuc) result = "Seller deleted.";
                    else result = "There is an unknown exception.";
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }
            }
            else result = "The seller does not exists.";
            return sonuc;
        }
        public Seller UpdateSeller(Seller seller, out string result)
        {
            if (seller != null)
            {
                if (CheckSeller(seller.SellerID) && !CheckSeller(seller.SellerName))
                {
                    seller.UpdateDate = DateTime.Now;
                    //db.Entry(GetSeller(seller.SellerID)).CurrentValues.SetValues(seller);
                    if (db.SaveChanges() > 0)
                    {
                        result = "Update succeeded.";
                        return seller;
                    }
                    else
                    {
                        result = "There is an unknown exception";
                        return seller;
                    }
                }
                else
                {
                    result = "Seller does not exists.";
                    return null;
                }
            }
            else
            {
                result = "Seller object cannot be null";
                return null;
            }
        }
        public List<Seller> GetSellersOfUser(int userId)
        {
            return (from s in db.Sellers
                    join e in db.Employees on s.SellerID equals e.SellerID
                    join u in db.Users on e.UserID equals u.UserID
                    where u.UserID == userId
                    orderby s.CreateDate descending
                    select s).ToList();
        }
    }
}
