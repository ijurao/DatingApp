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
            modelBuilder.Entity<Value>().HasData(new Value { Id = 1,Name = "Value101" });
            modelBuilder.Entity<Value>().HasData(new Value { Id = 2,Name = "Value102" });
            modelBuilder.Entity<Value>().HasData(new Value { Id = 3,Name = "Value103" });

        }

        public DbSet<Value> Values { get; set; }
        public DbSet<UserApplication> Users { get; set; }
    }
}
