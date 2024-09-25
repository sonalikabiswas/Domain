using DomainAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DomainAPI.Repository.Services
{
    public class SavedListingRepository : ISavedListingsRepository
    {
        private readonly DomainContext _context;

        public SavedListingRepository(DomainContext context)
        {
            _context = context;
        }

        public async Task<SavedListing> AddSavedListing(SavedListing savedListings)
        {
            var result = _context.SavedListings.Add(savedListings);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        public async Task DeleteSavedListing(SavedListing savedListing)
        {
             _context.SavedListings.Remove(savedListing);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetCountShorlistedByUsers(long id)
        { 

            return _context.SavedListings.Where(x => x.ListingId == id).Select(x=> x.UserId).Distinct().Count();
        }
        public async Task<IEnumerable<SavedListing>> GetSavedListings(long userId)
        {
            return await _context.SavedListings.Where(x => x.UserId == userId).Include(c => c.Listing).ToListAsync();
        }

        public async Task<SavedListing> GetSavedListing(int id)
        {
            var savedListing = await _context.SavedListings.Where(c => c.Id == id).Include(c => c.Listing).Include(c => c.User).FirstOrDefaultAsync();
            return savedListing;
        }

        public async Task<SavedListing> GetSavedListing(SavedListing savedListing)
        {
            return await _context.SavedListings.Where(c => c.UserId == savedListing.UserId && c.ListingId == savedListing.ListingId).FirstOrDefaultAsync();
        }

        public async Task<SavedListing> UpdateSavedListing(SavedListing savedListing)
        {
            _context.Entry(savedListing).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return savedListing;
        }

        public bool SavedListingExists(long id)
        {
            return (_context.SavedListings?.Any(e => e.Id == id)).GetValueOrDefault();
        }

      
    }
}
