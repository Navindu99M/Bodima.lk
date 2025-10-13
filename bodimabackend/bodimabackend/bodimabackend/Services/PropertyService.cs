using bodimabackend.Models;
using bodimabackend.Repositories;
using Microsoft.EntityFrameworkCore;
using bodimabackend.Controllers;

namespace bodimabackend.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly AppDbContext _context;
        private readonly IPropertyRepository _repo;

        public PropertyService(AppDbContext context, IPropertyRepository repo)
        {
            _context = context;
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

        //public async Task<Property> UpdateAsync(Property property)
        //{
        //    await _repo.UpdateAsync(property);
        //    await _repo.SaveAsync();
        //    return property;
        //}
        public async Task<bool> UpdateAsync(int propertyId, Property updatedProperty, int ownerId)
        {
            var existing = await _repo.GetByIdAsync(propertyId);
            if(existing == null || existing.OwnerId != ownerId) return false;

            existing.Title = updatedProperty.Title;
            existing.Description = updatedProperty.Description;
            existing.Location = updatedProperty.Location;
            existing.PricePerMonth = updatedProperty.PricePerMonth;

            await _repo.UpdateAsync(existing);
            await _repo.SaveAsync();
            return true;
        }

        //public async Task DeleteAsync(int id)
        //{
        //    var prop = await _repo.GetByIdAsync(id);
        //    if (prop != null)
        //    {
        //        await _repo.DeleteAsync(prop);
        //        await _repo.SaveAsync();
        //    }
        //}

        public async Task<bool> SoftDeletePropertyAsync(int propertyId, int ownerId)
        {
            var property = await _context.Properties.FindAsync(propertyId);
            if(property == null || property.OwnerId != ownerId)
                return false;

            property.IsAvailable = 1;
            await _context.SaveChangesAsync();
            return true;
        }

        //Image
        public async Task<PropertyImage> AddImageAsync(int propertyId, IFormFile file, int ownerId)
        {
            var property = await _context.Properties.FindAsync(propertyId);

            if (property == null || property.OwnerId != ownerId)
                throw new UnauthorizedAccessException("You cannot upload images for this property.");

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var image = new PropertyImage
            {
                PropertyId = propertyId,
                ImageUrl = $"/images/{fileName}"
            };

            _context.PropertyImages.Add(image);
            await _context.SaveChangesAsync();

            return image;
        }



        public async Task<bool> DeleteImageAsync(int imageId, int ownerId)
        {
            var image = await _context.PropertyImages
                .Include(i => i.Property)
                .FirstOrDefaultAsync(i => i.Id == imageId);

            if (image == null || image.Property.OwnerId != ownerId)
                return false;

            _context.PropertyImages.Remove(image);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
