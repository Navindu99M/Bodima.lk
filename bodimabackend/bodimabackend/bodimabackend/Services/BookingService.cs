using bodimabackend.Models;
using bodimabackend.Repositories;

namespace bodimabackend.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repo;

        public BookingService(IBookingRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Booking>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<Booking> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task<Booking> CreateAsync(Booking booking)
        {
            await _repo.AddAsync(booking);
            await _repo.SaveAsync();
            return booking;
        }

        public async Task<Booking> UpdateAsync(Booking booking)
        {
            await _repo.UpdateAsync(booking);
            await _repo.SaveAsync();
            return booking;
        }

        public async Task DeleteAsync(int id)
        {
            var b = await _repo.GetByIdAsync(id);
            if (b != null)
            {
                await _repo.DeleteAsync(b);
                await _repo.SaveAsync();
            }
        }
    }
}
