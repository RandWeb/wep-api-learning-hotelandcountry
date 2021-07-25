using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Data.Entities
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; }
        public Country Country { get; set; }
        public int CountryId { get; set; }
    }

    public class ConfigurationHotel : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(c => c.Country).WithMany(h => h.Hotels).HasForeignKey(p => p.CountryId);
        }
    }
}
