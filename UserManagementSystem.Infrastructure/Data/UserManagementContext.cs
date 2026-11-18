using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UserManagementSystem.Domain.Entities;

namespace UserManagementSystem.Infrastructure.Data
{
    public class UserManagementContext:DbContext
    {
        public UserManagementContext(DbContextOptions<UserManagementContext> options):base(options)
        {
                
        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().Property(x => x.Name).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.Family).HasMaxLength(70).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.NationalCode).HasMaxLength(10).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.BirthDate).IsRequired();


            base.OnModelCreating(modelBuilder);
        }
    }
}
