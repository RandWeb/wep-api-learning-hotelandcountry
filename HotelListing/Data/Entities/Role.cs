using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Data.Entities
{
    public class Role : IdentityRole
    {
    }

    public class ConfigurationRole : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role
                {
                    Name = "User",
                    NormalizedName = "USER"
                }, new Role
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                }
                );
        }
    }
}
