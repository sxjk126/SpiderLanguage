using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpiderLanguage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SpiderLanguage.Data
{
    public class SpiderLanguageContext : IdentityDbContext<User>
    {
        public SpiderLanguageContext(DbContextOptions<SpiderLanguageContext> options)
              : base(options)
        {
        }

        public DbSet<User> SpiderLanguageUsers { get; set; }
        public DbSet<Chapter> Chapters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Chapter>().HasData(
               new Chapter
               {
                   Id = 1,
                   Name = "Chapter One",
                   Available = true,
                   ImageMimeType = "image/jpeg",
                   ImageName = "Country.jpg",
                   Recommended = true,
                   DatePublished = new DateTime(2019, 6, 20).ToShortDateString()
               },
               new Chapter
               {
                   Id = 2,
                   Name = "Chapter Two",
                   Available = true,
                   ImageMimeType = "image/jpeg",
                   ImageName = "Disciplines.jpg",
                   Recommended = true,
                   DatePublished = new DateTime(2019, 8, 12).ToShortDateString()

               }, new Chapter
               {
                   Id = 3,
                   Name = "Chapter Three",
                   Available = true,
                   ImageMimeType = "image/jpeg",
                   ImageName = "Cellphones.jpg",
                   Recommended = true,
                   DatePublished = new DateTime(2019, 2, 9).ToShortDateString()
               });
        }
    }
}
