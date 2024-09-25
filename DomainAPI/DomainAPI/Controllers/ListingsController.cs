using AutoMapper;
using DomainAPI.DTO;
using DomainAPI.Models;
using DomainAPI.Repository;
using DomainAPI.Repository.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DomainAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingsController : ControllerBase
    {
        private readonly IListingRepository _listingRepository;
        private readonly IMapper _mapper;

        public ListingsController(IListingRepository listingRepository, IMapper mapper)
        {
            _listingRepository = listingRepository;
            _mapper = mapper;
        }

        // GET: api/Listings
        [HttpGet]
        public async Task<IEnumerable<Listing>> GetListings()
        {
            return await _listingRepository.GetListings();
        }

        // GET: api/Listings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Listing>> GetListing(long id)
        {
            var listing = await _listingRepository.GetListing(id);

            if (listing == null)
            {
                return NotFound();
            }

            return listing;
        }

        // PUT: api/Listings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutListing(long id, ListingDTO listingPayload)
        {
            if (id != listingPayload.Id)
            {
                return BadRequest();
            }
            var existingListing = _listingRepository.GetListing(id);

            if (existingListing == null) return NotFound();

            var listing = _mapper.Map<Listing>(listingPayload);
            try
            {
                await _listingRepository.UpdateListing(listing);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_listingRepository.ListingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Listings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Listing>> PostListing(ListingDTO listingPayload)
        {   
            if (listingPayload == null)
                return BadRequest();
            var newListing = _mapper.Map<Listing>(listingPayload);

            newListing = await _listingRepository.AddListing(newListing);

            return CreatedAtAction("GetListing", new { id = newListing.Id }, newListing);
        }

        // DELETE: api/Listings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListing(long id)
        {
            var listing = await  _listingRepository.GetListing(id);
            if (listing == null)
            {
                return NotFound();
            }

            await _listingRepository.DeleteListing(listing);

            return NoContent();
        }

       
    }
}
