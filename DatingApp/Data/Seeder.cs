using DatingApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
namespace DatingApp.Data
{
    public static class Seeder
    {
        public static void SeedUser(ApplicationDbContext db)
        {
            if (!db.Users.Any())
            {
                var usersData = System.IO.File.ReadAllText("Data/DataToSed.json");
                var users = JsonConvert.DeserializeObject<List<UserApplication>>(usersData);
                foreach (var usr in users)
                {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash(usr.Password, out passwordHash, out passwordSalt);
                    usr.PasswordHash = passwordHash;
                    usr.PasswordSalt = passwordSalt;
                    usr.UserName = usr.UserName.ToLower();
                    db.Users.Add(usr);
                }

                db.SaveChanges();
            }




        }
        private static void CreatePasswordHash(string pass, out byte[] passHash, out byte[] passSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passSalt = hmac.Key;
                passHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(pass));

            }

        }


    }
}
