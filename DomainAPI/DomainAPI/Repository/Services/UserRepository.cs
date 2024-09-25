using DomainAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DomainAPI.Repository.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly DomainContext _context;

        public UserRepository(DomainContext context)
        {
            _context = context;
        }

        public async Task<User> AddUser(User user)
        {
            var result = _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteUser(User User)
        {
            _context.Users.Remove(User);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUser(long Id)
        {
            return await _context.Users.FindAsync(Id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> UpdateUser(User User)
        {
            _context.Entry(User).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return User;
        }

        public bool UserExists(long id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
