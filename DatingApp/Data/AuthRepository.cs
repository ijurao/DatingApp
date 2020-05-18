using DatingApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace DatingApp.Data
{
    public class AuthRepository : IAuthRepository
    {
        private ApplicationDbContext _context;

        public AuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<UserApplication> Login(string userName, string pass)
        {

            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user != null)
            {
                if (!VerifyPassword(pass, user.PasswordHash, user.PasswordSalt))
                {
                    return null;
                }
                else
                {
                    return user;
                }
            }
            else
            {
                return null;
            }

        }

        private bool VerifyPassword(string pass, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(pass));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if(computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;

            }
        }

        public async Task<UserApplication> RegisterUser(UserApplication user, string pass)
        {

            byte[] passHash, passSalt;
            createPassHash(pass,out passHash,out passSalt);
            user.PasswordHash = passHash;
            user.PasswordSalt = passSalt;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
         }

        private void createPassHash(string pass, out byte[] passHash, out byte[] passSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passSalt = hmac.Key;
                passHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(pass));

            }

        }

        public async Task<bool> UserExists(string userName)
        {
            var user = await _context.Users.AnyAsync(x => x.UserName == userName);
            if (await _context.Users.AnyAsync(x => x.UserName == userName))
            {
                return true;
            }
            return  false;
         }
    }
}
