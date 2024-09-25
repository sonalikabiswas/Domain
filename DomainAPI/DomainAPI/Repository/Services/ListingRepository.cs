using AutoMapper;
using DomainAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace DomainAPI.Repository.Services
{
    public class ListingRepository : IListingRepository
    {
        private readonly DomainContext _context;

        public ListingRepository(DomainContext context)
        {
            _context = context;
        }

        public async Task<Listing> AddListing(Listing listing)
        {
            var result = _context.Listings.Add(listing);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteListing(Listing listing)
        {
            _context.Listings.Remove(listing);
            await _context.SaveChangesAsync();
        }

        public async Task<Listing> GetListing(long Id)
        {
            return await _context.Listings.FindAsync(Id);
        }

        public async Task<IEnumerable<Listing>> GetListings()
        {
            return await _context.Listings.ToListAsync();
        }

        public async Task<Listing> UpdateListing(Listing listing)
        {
            _context.Entry(listing).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return listing;
        }

        public bool ListingExists(long id)
        {
            return (_context.Listings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
