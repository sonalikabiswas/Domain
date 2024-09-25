using DomainAPI.Models;

namespace DomainAPI.Repository
{
    public interface  IListingRepository
    {
        Task<IEnumerable<Listing>> GetListings();
        Task<Listing> GetListing(long Id);
        Task<Listing> AddListing(Listing listing);
        Task<Listing> UpdateListing(Listing listing);
        Task DeleteListing(Listing listing);

        bool ListingExists(long id);
    }
}
