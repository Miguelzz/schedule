using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Data
{
    public class UserContext: DbContext
    {
        public UserContext(DbContextOptions options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Relations> Relations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Relations>()
                .HasKey(bc => new { bc.UserId, bc.User2Id });

            //modelBuilder.Entity<Relations>()
            //    .HasOne(bc => bc.User2)
            //    .WithMany(b => b.Relations)
            //    .HasForeignKey(bc => bc.User2Id);

            modelBuilder.Entity<Relations>()
                .HasOne(bc => bc.User)
                .WithMany(c => c.Relations)
                .HasForeignKey(bc => bc.UserId);
        }

    }
}
