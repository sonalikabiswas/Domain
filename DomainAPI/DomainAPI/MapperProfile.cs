using AutoMapper;
using DomainAPI.DTO;
using DomainAPI.Models;

namespace DomainAPI
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserDTO, User>();
            CreateMap<ListingDTO,Listing>();
            CreateMap<SavedListingDTO, SavedListing>();

            CreateMap<User, UserDTO>();
            CreateMap<Listing, ListingDTO>();
            CreateMap<SavedListing, SavedListingDTO>();

        }
    }
}
