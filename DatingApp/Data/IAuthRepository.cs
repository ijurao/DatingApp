using DatingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace DatingApp.Data
{
    public interface IAuthRepository
    {
        Task<UserApplication> RegisterUser(UserApplication user, string pass);

        Task<UserApplication> Login(string userName, string pass);

        Task<bool> UserExists(string userName);
    }
}
