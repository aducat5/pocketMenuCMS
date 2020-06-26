using PMCMS.DAL;
using System;
using System.Linq;

namespace PMCMS.BLL.Repos
{
    public class SellerRepo
    {
        private static PocketMenuDBEntities db = DBTool.GetInstance();
        public bool CheckSeller(int id) => db.Sellers.Any(seller => seller.SellerID == id);
        public bool CheckSeller(string name) => db.Sellers.Any(seller => seller.SellerName == name);
        public Seller GetSeller(int id)
        {
            Seller seller = null;
            if (CheckSeller(id)) 
                seller = db.Sellers.Find(id);
            return seller;
        }
        public bool InsertSeller(Seller newSeller, out string result)
        {
            newSeller.CreateDate = DateTime.Now;
            newSeller.UpdateDate = DateTime.Now;

            try
            {
                if (CheckSeller(newSeller.SellerName))
                {
                    result = "This name is already in use.";
                    return false;
                }
                else
                {
                    db.Sellers.Add(newSeller);
                    if (db.SaveChanges() > 0)
                    {
                        result = "A new restaurant created.";
                        return true;
                    }
                    else
                    {
                        result = "There is a problem with the save process.";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                result = "there is an unknown error" + ex.Message;
                return false;
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
        public bool UpdateSeller(Seller seller, out string result)
        {
            bool sonuc = false;
            if (seller != null)
            {
                if (CheckSeller(seller.SellerID) && !CheckSeller(seller.SellerName))
                {
                    seller.UpdateDate = DateTime.Now;
                    db.Entry(GetSeller(seller.SellerID)).CurrentValues.SetValues(seller);
                    if (db.SaveChanges() > 0) 
                        result = "Update succeeded.";
                    else 
                        result = "There is an unknown exception";
                }
                else result = "Seller does not exists.";
            }
            else result = "Seller object cannot be null";
            return sonuc;
        }
        
    }
}
