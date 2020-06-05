using DatingApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly ApplicationDbContext _context;

        public DatingRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {

            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<UserApplication> GetUser(int id)
        {
            var user = await this._context.Users.Include(x => x.Photos).FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task<IEnumerable<UserApplication>> GetUsers()
        {
           

            var list = await this._context.Users.Include(x => x.Photos).ToListAsync();
            return list;
        }

        public async Task<bool> SaveAll()
        {
            return await this._context.SaveChangesAsync() > 0;
        }

        public async Task<UserPhoto> GetPhoto(int id)
        {
            return await this._context.UsersPhotos.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<UserPhoto> GetMainPhotoForUser(int userId)
        {
            var photo =   await this._context.UsersPhotos.Where(x => x.UserApplicationId == userId)
                .FirstOrDefaultAsync(p => p.IsMain);

            return photo;
        }
    }
}
