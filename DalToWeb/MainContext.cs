using DalToWeb.Configurations;
using DalToWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace DalToWeb.Repositories
{
    public class MainContext : DbContext
    {
        public MainContext()
            : base("DefaultConnection")
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Edition> Editions { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CityConfiguration());
            modelBuilder.Configurations.Add(new HouseConfiguration());
            modelBuilder.Configurations.Add(new LanguageConfiguration());
            modelBuilder.Configurations.Add(new EditionConfiguration());
            modelBuilder.Configurations.Add(new GenreConfiguration());
            modelBuilder.Configurations.Add(new AuthorConfiguration());
        }
    }
}