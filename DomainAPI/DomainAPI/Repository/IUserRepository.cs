using DomainAPI.Models;

namespace DomainAPI.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(long Id);
        Task<User> AddUser(User user);
        Task<User> UpdateUser(User user);
        Task DeleteUser(User user);
        bool UserExists(long id);
    }
}
