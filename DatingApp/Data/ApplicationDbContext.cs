using DatingApp.DTOS;
using DatingApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data
{
    public class ApplicationDbContext:DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

        }

        public DbSet<Value> Values { get; set; }
        public DbSet<UserApplication> Users { get; set; }
        public DbSet<UserPhoto> UsersPhotos { get; set; }
    }
}
