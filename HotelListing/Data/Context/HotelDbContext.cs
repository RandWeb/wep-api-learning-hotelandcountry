using HotelListing.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration<Hotel>(new ConfigurationHotel());
            modelBuilder.Entity<Country>().HasData(
                new Country { Id=1,Name="Iran",ShortName="ir"},
                new Country { Id=2,Name="Soryie",ShortName="so"},
                new Country { Id=3,Name="iraq",ShortName="aq"},
                new Country { Id=4,Name="Brazel",ShortName="br"}
                );

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel { Id=1,Name="Pars",Address="bolvar k",CountryId=1,Rating=3.2},
                new Hotel { Id=2,Name="nor afshar",Address="darband",CountryId=1,Rating=4.2},
                new Hotel { Id=3,Name="baghdad",Address="sabz k",CountryId=3,Rating=2.1},
                new Hotel { Id=4,Name="saires",Address="santigao k",CountryId=4,Rating=1.2}
                );
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
    }
}
