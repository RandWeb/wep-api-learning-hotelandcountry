using HotelListing.Data;
using HotelListing.Data.Entities;
using HotelListing.Repository.Contracts;
using HotelListing.Repository.Implementaion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IGenericRepository<Country> _countries;
        private IGenericRepository<Hotel> _hotels;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Country> Countries => (_countries != null ? _countries : new GenericRepository<Country>(_context));

        public IGenericRepository<Hotel> Hotels => (_hotels!=null? _hotels: new GenericRepository<Hotel>(_context));

        public void Dispose()
        { 
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
           await _context.SaveChangesAsync();
        }
    }
}
