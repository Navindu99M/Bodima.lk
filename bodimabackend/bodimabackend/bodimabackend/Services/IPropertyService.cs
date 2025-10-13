using bodimabackend.Models;
namespace bodimabackend.Services
{
    public interface IPropertyService
    {
        Task<IEnumerable<Property>> GetAllAsync();
        Task<Property> GetByIdAsync(int id);
        Task<Property> CreateAsync(Property property);
        //Task<Property> UpdateAsync(Property property);
        Task<bool> UpdateAsync(int propertyId, Property updatedProperty, int ownerId);
        Task<IEnumerable<Property>> GetPropertiesByOwnerIdAsync(int ownerId);
        Task<Property> AddPropertyAsync(Property property);
        //Task DeleteAsync(int id);
        Task<bool> SoftDeletePropertyAsync(int propertyId, int ownerId);

        //Image
        Task<PropertyImage> AddImageAsync(int propertyId, IFormFile file, int ownerId);
        Task<bool> DeleteImageAsync(int imageId, int ownerId);

    }
}
