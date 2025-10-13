using bodimabackend.Models;
using Microsoft.EntityFrameworkCore;

namespace bodimabackend.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly AppDbContext _context;

        public PropertyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Property>> GetPropertiesByOwnerIdAsync(int ownerId)
        {
            return await _context.Properties
                .Where(p => p.OwnerId == ownerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetAllAsync() => await _context.Properties.Include(p => p.Owner).ToListAsync();
        public async Task<Property> GetByIdAsync(int id) => await _context.Properties.FindAsync(id);
        public async Task AddAsync(Property property) => await _context.Properties.AddAsync(property);
        public async Task UpdateAsync(Property property) => _context.Properties.Update(property);
        public async Task DeleteAsync(Property property) => _context.Properties.Remove(property);
        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
