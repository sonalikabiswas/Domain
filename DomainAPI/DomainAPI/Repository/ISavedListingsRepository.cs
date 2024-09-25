using DomainAPI.Models;

namespace DomainAPI.Repository
{
    public interface ISavedListingsRepository
    {
        Task<IEnumerable<SavedListing>> GetSavedListings(long userId);
        Task<SavedListing> GetSavedListing(int Id);
        Task<SavedListing> GetSavedListing(SavedListing savedListing);
        Task<SavedListing> AddSavedListing(SavedListing SavedListing);
        Task<SavedListing> UpdateSavedListing(SavedListing SavedListing);
        Task DeleteSavedListing(SavedListing SavedListing);
        bool SavedListingExists(long id);

        Task<int> GetCountShorlistedByUsers(long id);
    }
}
