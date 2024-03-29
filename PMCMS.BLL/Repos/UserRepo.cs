﻿using PMCMS.DAL;
using System;
using System.Linq;

namespace PMCMS.BLL.Repos
{
    public class UserRepo
    {
        private static readonly PocketMenuDBEntities db = DBTool.GetInstance();
        public bool CheckUser(int id)
        {
            using (db)
            {
                return db.Users.Any(u => u.UserID == id);
            }
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
                    result = "This email is already in use.";
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
                        result = "Something went wrong, please call the support team.";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                result = "Unhandled exception: " + ex.Message;
                return false;
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
                        result = "Delete is successful.";
                    else
                        result = "Something went wrong, please call the support team.";
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }
            }
            else result = "The user you are trying to delete could not be found.";
            return sonuc;
        }
        public bool UpdateUser(User user, out string result)
        {
            bool sonuc = false;
            if (user != null)
            {
                if (CheckUser(user.UserID))
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                    user.UpdateDate = DateTime.Now;

                    db.Entry(GetUser(user.UserID)).CurrentValues.SetValues(user);
                    if (db.SaveChanges() > 0)
                        result = "Update is successful";
                    else
                        result = "Something went wrong, please call the support team.";
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
