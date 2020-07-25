using PMCMS.DAL;
using System;
using System.Linq;

namespace PMCMS.BLL.Repos
{
    public class UserRepo
    {
        private static readonly PocketMenuDBEntities db = DBTool.GetInstance();
        public bool CheckUser(int id)
        {
            return db.Users.Any(u => u.UserID == id);
        }

        public bool CheckUser(string email)
        {
            return db.Users.Any(u => u.Email == email);
        }

        public User GetUser(int id)
        {
            User user = null;
            if (CheckUser(id)) user = db.Users.Find(id);
            return user;
        }
        public User GetUser(string email, string password)
        {
            User user = db.Users.Where(u => u.Email == email).FirstOrDefault();

            if (BCrypt.Net.BCrypt.Verify(password, user.Password))
                return user;
            else
                return null;
        }
        public bool InsertUser(User newUser, out string result)
        {
            newUser.CreateDate = DateTime.Now;
            newUser.UpdateDate = DateTime.Now;

            try
            {
                if (CheckUser(newUser.Email))
                {
                    result = "Bu adres zaten kullanımda.";
                    return false;
                }
                else
                {
                    newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
                    db.Users.Add(newUser);
                    if (db.SaveChanges() > 0)
                    {
                        result = "Insert is successful.";
                        return true;
                    }
                    else
                    {
                        result = "Bazı şeyler istediğimiz gibi gitmedi, lütfen destek ekibi ile iletişime geçiniz.";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                result = "Bilinmeyen Hata: " + ex.Message;
                return false;
            }
        }

        public bool UpdatePass(string newPass, int userID, out string result)
        {
            if (!CheckUser(userID))
            {
                result = "Güncellemeye çalıştığınız kullanıcı bulunamadı";
                return false;
            }
            else
            {
                User userToUpdate = GetUser(userID);
                userToUpdate.Password = BCrypt.Net.BCrypt.HashPassword(newPass);
                userToUpdate.UpdateDate = DateTime.Now;
                bool sonuc = db.SaveChanges() > 0;
                if (sonuc)
                {
                    result = "Parola başarıyla güncellendi";
                    return sonuc;
                }
                else
                {
                    result = "Güncelleme sırasında bir sorun oluştu, lütfen destek ekibi ile iletişime geçin.";
                    return sonuc;
                }
            }
        }

        public bool DeleteUser(int id, out string result)
        {
            bool sonuc = false;
            if (CheckUser(id))
            {
                User user = GetUser(id);
                user.IsDeleted = true;
                try
                {
                    using (var db = DBTool.GetNewInstance())
                    {
                        sonuc = db.SaveChanges() > 0;
                    }

                    if (sonuc) 
                        result = "Silme işlemi başarılı.";
                    else
                        result = "Bazı şeyler istediğimiz gibi gitmedi, lütfen destek ekibi ile iletişime geçiniz.";
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }
            }
            else result = "Silmeye çalıştığınız kullanıcı bulunamadı.";
            return sonuc;
        }
        public bool UpdateUser(User user, out string result)
        {
            bool sonuc = false;
            if (user != null)
            {
                if (CheckUser(user.UserID))
                {
                    User userToUpdate = GetUser(user.UserID);
                    
                    userToUpdate.UpdateDate = DateTime.Now;
                    userToUpdate.FirstName = user.FirstName;
                    userToUpdate.LastName = user.LastName;
                    userToUpdate.Email = user.Email;

                    db.Entry(GetUser(user.UserID)).CurrentValues.SetValues(userToUpdate);

                    if (db.SaveChanges() > 0)
                        result = "Güncelleme başarılı.";
                    else
                        result = "Bazı şeyler istediğimiz gibi gitmedi, lütfen destek ekibi ile iletişime geçiniz.";
                }
                else 
                    result = "Kullanıcı Bulunamadı.";
            }
            else 
                result = "Kullanıcı nesnesi boş olamaz";
            return sonuc;
        }

        //TODO: AuthorizeUser with permission level
        //public static bool IsUserAllowed(int userID, int contentID, PermissionTypes permissionType, ContentTypes contentType)
        //{
        //    int permissionTypeID = permissionType.ToInt();
        //    int contentTypeID = contentType.ToInt();

        //    return db.Permissions.Any(
        //        p =>
        //        p.PermissionTypeID >= permissionTypeID &&
        //        p.ContentTypeID == contentTypeID &&
        //        p.ContentID == contentID &&
        //        p.GrantedUserID == userID
        //    );
        //}
    }
}
