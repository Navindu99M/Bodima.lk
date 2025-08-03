using bodimabackend.Models;

namespace bodimabackend.Services
{
    public interface IUserService
    {
        Task<User> RegisterAsync(User user);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByIdAsync(int id);
    }
}
