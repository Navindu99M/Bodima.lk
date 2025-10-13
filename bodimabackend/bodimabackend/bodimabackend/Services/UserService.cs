using bodimabackend.Models;
using bodimabackend.Repositories;

namespace bodimabackend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<User> RegisterAsync(User user)
        {
            await _repo.AddAsync(user);
            await _repo.SaveAsync();
            return user;
        }

        public async Task<User> GetByEmailAsync(string email) => await _repo.GetByEmailAsync(email);
        public async Task<User> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);
    }
}
