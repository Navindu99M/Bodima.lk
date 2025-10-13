using bodimabackend.Models;
using Microsoft.EntityFrameworkCore;

namespace bodimabackend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync() => await _context.Users.ToListAsync();
        public async Task<User> GetByIdAsync(int id) => await _context.Users.FindAsync(id);
        public async Task<User> GetByEmailAsync(string email) => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        public async Task AddAsync(User user) => await _context.Users.AddAsync(user);
        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
