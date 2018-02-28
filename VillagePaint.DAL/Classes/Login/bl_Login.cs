﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Crypto;

namespace VillagePaint.DAL.Classes.Login
{
    public class bl_Login
    {
        public long userID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public bool isActive { get; set; }
        public bool isGuest { get; set; }

        public static bl_Login Get(string email, string password)
        {
            using (var metadata = DataAccess.getDesktopMetadata())
            {
                var q = from rowU in metadata.db_User
                        where rowU.Email == email
                        select rowU;

                var qUser = q.FirstOrDefault();

                //validate email
                if (qUser == null)
                    return null;

                //validate password
                if (!PasswordManager.verify(password, qUser.PasswordHash))
                    return null;

                return new bl_Login
                {
                    userID = qUser.userID,
                    Name = qUser.FirstName,
                    LastName = qUser.LastName,
                    isActive = true,
                    //isGuest = qUser.isGuest,
                };
            }
        }
    }
}
