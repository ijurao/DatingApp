using DatingApp.DTOS;
using DatingApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

            // n to n relaationship 

            modelBuilder.Entity<Like>().
                HasKey(k => new { k.LikerId, k.LikeeId });

            modelBuilder.Entity<Like>().
                HasOne(u => u.Likee).
                WithMany(u => u.Likers).
                HasForeignKey(u => u.LikeeId).
                OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Like>().
               HasOne(u => u.Liker).
               WithMany(u => u.Likees).
               HasForeignKey(u => u.LikerId).
               OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>().
                HasOne(u => u.Sender).WithMany(u => u.MessagesSents).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>().
               HasOne(u => u.Recipient).WithMany(u => u.MessagesReceived).OnDelete(DeleteBehavior.Restrict);

        }

        public DbSet<Value> Values { get; set; }
        public DbSet<UserApplication> Users { get; set; }
        public DbSet<UserPhoto> UsersPhotos { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
