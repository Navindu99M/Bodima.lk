using bodimabackend.Models;
using Microsoft.EntityFrameworkCore;


namespace bodimabackend.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetAllAsync() => await _context.Bookings.Include(b => b.Property).Include(b => b.User).ToListAsync();
        public async Task<Booking> GetByIdAsync(int id) => await _context.Bookings.FindAsync(id);
        public async Task AddAsync(Booking booking) => await _context.Bookings.AddAsync(booking);
        public async Task UpdateAsync(Booking booking) => _context.Bookings.Update(booking);
        public async Task DeleteAsync(Booking booking) => _context.Bookings.Remove(booking);
        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
