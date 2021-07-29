using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Data.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public virtual IList<Hotel> Hotels { get; set; }
    }

    public class ConfigurationCountry : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
                new Country { Id = 1, Name = "Iran", ShortName = "ir" },
                new Country { Id = 2, Name = "Soryie", ShortName = "so" },
                new Country { Id = 3, Name = "iraq", ShortName = "aq" },
                new Country { Id = 4, Name = "Brazel", ShortName = "br" }
                );
        }
    }
}
