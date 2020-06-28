using DatingApp.Helpers;
using DatingApp.Migrations;
using DatingApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        private async Task<IEnumerable<int>> GetUserLikes(int id, bool likers)
        {
            var user = await this._context.Users.Include(x => x.Likees).Include(x => x.Likers).FirstOrDefaultAsync(u => u.Id == id);
            if (likers)
            {
                return user.Likers.Where(u => u.LikeeId == id).Select(i => i.LikerId);
            }
            else
            {
                return user.Likees.Where(u => u.LikerId == id).Select(i => i.LikeeId);

            }

        }
        public async Task<PagedList<UserApplication>> GetUsers(UserParams parameters)
        {
           

            var list =  this._context.Users.Include(x => x.Photos).AsQueryable();
            var minDateOfBirth = DateTime.Today.AddYears(-parameters.MaxAge + 1);
            var maxDateOfBirth = DateTime.Today.AddYears(-parameters.MinAge + 1);
            list = list.Where(x => x.Id != parameters.UserApplicationId)
                        .Where(x => x.Gender == parameters.Gender)
                        .Where(x => x.DateOfBirth >= minDateOfBirth && x.DateOfBirth <= maxDateOfBirth);

            if (parameters.Likers)
            {
                var userLikers = await GetUserLikes(parameters.UserApplicationId, parameters.Likers);
                list = list.Where(u => userLikers.Contains(u.Id));
            }

            if (parameters.Likees)
            {
                var userLikees = await GetUserLikes(parameters.UserApplicationId, parameters.Likers);
                list = list.Where(u => userLikees.Contains(u.Id));


            }
            if (!String.IsNullOrEmpty(parameters.OrderBy))
            {
                GenericSorter<UserApplication> genericSorter = new GenericSorter<UserApplication>();
                list = genericSorter.Sort(list, parameters.OrderBy, "desc");
            }
        





            return await PagedList<UserApplication>.CreateSync(list, parameters.PageNumber, parameters.PageSize);
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

        public async Task<Like> GetLike(int liker, int likee)
        {
            return await this._context.Likes.FirstOrDefaultAsync(u => u.LikerId == liker && u.LikeeId == likee);
        }

    }



}
