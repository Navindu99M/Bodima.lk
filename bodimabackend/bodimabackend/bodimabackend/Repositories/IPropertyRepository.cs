using bodimabackend.Models;

namespace bodimabackend.Repositories
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetAllAsync();
        Task<Property> GetByIdAsync(int id);
        Task AddAsync(Property property);
        Task UpdateAsync(Property property);
        Task DeleteAsync(Property property);
        Task<IEnumerable<Property>> GetPropertiesByOwnerIdAsync(int ownerId);
        Task SaveAsync();
    }
}
