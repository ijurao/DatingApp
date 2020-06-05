﻿using DatingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data
{
    public interface IDatingRepository
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
        Task<IEnumerable<UserApplication>> GetUsers();
        Task<UserApplication> GetUser(int id);

        Task<UserPhoto> GetPhoto(int id);

        Task<UserPhoto> GetMainPhotoForUser(int userId);

    }
}
