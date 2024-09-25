using DomainAPI.Models;
using DomainAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DomainAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavedListingsController : ControllerBase
    {
        private readonly ISavedListingsRepository _savedListingsRepository;
        private readonly IUserRepository _userRepository;
        private readonly IListingRepository _listingRepository;
        public SavedListingsController(ISavedListingsRepository savedListingsRepository, IUserRepository userRepository, IListingRepository listingRepository)
        {
            _savedListingsRepository = savedListingsRepository;
            _userRepository = userRepository;
            _listingRepository = listingRepository;
        }
        // GET: api/<SavedListingsController>/5
        [HttpGet]
        public async Task<IEnumerable<SavedListing>> Get(long userId)
        {
            return await _savedListingsRepository.GetSavedListings(userId);
        }

        // POST api/<SavedListingsController>
        [HttpPost]
        public async Task<IActionResult> Post(long listingId, long userId)
        {

            var user = await _userRepository.GetUser(userId);
            if (user == null)
                return BadRequest();

            var listing = await _listingRepository.GetListing(listingId);
            if (listing == null)
                return BadRequest();

            SavedListing savedListing
               = new SavedListing();
            savedListing.UserId = userId;
            savedListing.ListingId = listingId;

            savedListing = await _savedListingsRepository.AddSavedListing(savedListing);

            return CreatedAtAction(nameof(GetById), new { id = savedListing.Id }, savedListing);
        }


        // GET api/<SavedListingsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SavedListing), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var savedListing = await _savedListingsRepository.GetSavedListing(id);
            return savedListing == null ? NotFound() : Ok(savedListing);
        }


        // GET api/<SavedListingsController>/5
        [HttpGet]
        [Route("/UserCountByListingId")]
        public async Task<IActionResult> GetByListingId(int id)
        {
            var count = await _savedListingsRepository.GetCountShorlistedByUsers(id);
            return Ok(count);
        }

        // PUT api/<SavedListingsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, SavedListing listing)
        {
            if (id != listing.Id) return BadRequest();
            await _savedListingsRepository.UpdateSavedListing(listing);
            return NoContent();
        }

        // DELETE api/<SavedListingsController>/5
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long listingId, long userId)
        {
            SavedListing savedListing  = new SavedListing();
            savedListing.UserId = userId;
            savedListing.ListingId = listingId;

            var existingSavedListing = await _savedListingsRepository.GetSavedListing(savedListing);
            if (existingSavedListing == null) return NotFound();

            await _savedListingsRepository.DeleteSavedListing(existingSavedListing);
            
            return NoContent();
        }
    }
}
