using bodimabackend.Models;
using bodimabackend.Repositories;
using Microsoft.EntityFrameworkCore;
using bodimabackend.Controllers;

namespace bodimabackend.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _repo;

        public PropertyService(IPropertyRepository repo)
        {
            _repo = repo;
        }

        //private readonly AppDbContext _context;
        //public PropertyService(AppDbContext context)
        //{
        //    _context = context;
        //}

        public async Task<IEnumerable<Property>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<Property> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        //public async Task<IEnumerable<Property>> GetPropertiesByOwnerIdAsync(int ownerId)
        //{
        //    return await _context.Properties
        //        .Where(p => p.OwnerId == ownerId)
        //        .ToListAsync();
        //}
        public async Task<IEnumerable<Property>> GetPropertiesByOwnerIdAsync(int ownerId)
        {
            return await _repo.GetPropertiesByOwnerIdAsync(ownerId);
        }

        public async Task<Property> AddPropertyAsync(Property property)
        {
            await _repo.AddAsync(property);
            await _repo.SaveAsync();
            return property;
        }

        public async Task<Property> CreateAsync(Property property)
        {
            await _repo.AddAsync(property);
            await _repo.SaveAsync();
            return property;
        }

        public async Task<Property> UpdateAsync(Property property)
        {
            await _repo.UpdateAsync(property);
            await _repo.SaveAsync();
            return property;
        }

        public async Task DeleteAsync(int id)
        {
            var prop = await _repo.GetByIdAsync(id);
            if (prop != null)
            {
                await _repo.DeleteAsync(prop);
                await _repo.SaveAsync();
            }
        }
    }
}
