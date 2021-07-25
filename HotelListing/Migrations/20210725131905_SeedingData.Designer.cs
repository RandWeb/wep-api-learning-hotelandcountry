﻿// <auto-generated />
using HotelListing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelListing.Migrations
{
    [DbContext(typeof(HotelDbContext))]
    [Migration("20210725131905_SeedingData")]
    partial class SeedingData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HotelListing.Data.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<string>("ShortName");

                    b.HasKey("Id");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Iran",
                            ShortName = "ir"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Soryie",
                            ShortName = "so"
                        },
                        new
                        {
                            Id = 3,
                            Name = "iraq",
                            ShortName = "aq"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Brazel",
                            ShortName = "br"
                        });
                });

            modelBuilder.Entity("HotelListing.Data.Entities.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<int>("CountryId");

                    b.Property<string>("Name");

                    b.Property<double>("Rating");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Hotels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "bolvar k",
                            CountryId = 1,
                            Name = "Pars",
                            Rating = 3.2000000000000002
                        },
                        new
                        {
                            Id = 2,
                            Address = "darband",
                            CountryId = 1,
                            Name = "nor afshar",
                            Rating = 4.2000000000000002
                        },
                        new
                        {
                            Id = 3,
                            Address = "sabz k",
                            CountryId = 3,
                            Name = "baghdad",
                            Rating = 2.1000000000000001
                        },
                        new
                        {
                            Id = 4,
                            Address = "santigao k",
                            CountryId = 4,
                            Name = "saires",
                            Rating = 1.2
                        });
                });

            modelBuilder.Entity("HotelListing.Data.Entities.Hotel", b =>
                {
                    b.HasOne("HotelListing.Data.Entities.Country", "Country")
                        .WithMany("Hotels")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
