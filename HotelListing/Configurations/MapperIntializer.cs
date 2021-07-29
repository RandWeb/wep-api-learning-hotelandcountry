using AutoMapper;
using HotelListing.Data.Entities;
using HotelListing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Configurations
{
    public class MapperIntializer:Profile
    {
        public MapperIntializer()
        {
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<CountryDTO, CountryCommand>().ReverseMap();
            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<HotelDTO, HotelCommand>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserDTO, UserCommand>().ReverseMap();
        }
    }
}
