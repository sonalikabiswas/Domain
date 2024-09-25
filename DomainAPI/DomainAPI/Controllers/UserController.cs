using AutoMapper;
using DomainAPI.DTO;
using DomainAPI.Models;
using DomainAPI.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DomainAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        // GET: api/<UserController>
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _userRepository.GetUsers();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var user = await _userRepository.GetUser(id);
            return user == null ? NotFound() : Ok(user);
        }

        // POST api/<UserController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post(UserDTO userPayload)
        {
            if (userPayload == null)
                return BadRequest();

            var newUser = _mapper.Map<User>(userPayload);
             newUser = await _userRepository.AddUser(newUser);
            return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, newUser);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, UserDTO userPayload)
        {
            if (id != userPayload.Id) return BadRequest();

            var existingUser = _userRepository.GetUser(id);

            if (existingUser == null) return NotFound();

            var userToUpdate = _mapper.Map<User>(userPayload);

            await _userRepository.UpdateUser(userToUpdate);
            return NoContent();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            var userToDelete = await _userRepository.GetUser(id);
            if (userToDelete == null) return NotFound();

            await _userRepository.DeleteUser(userToDelete);

            return NoContent();
        }
    }
}
