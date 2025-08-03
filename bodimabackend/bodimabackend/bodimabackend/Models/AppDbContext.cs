using Microsoft.EntityFrameworkCore;

namespace bodimabackend.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //User to Properties
            modelBuilder.Entity<User>()
            .HasMany(u => u.Properties)
            .WithOne(p => p.Owner)
            .HasForeignKey(p => p.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);  // prevent cascade

            // User to Bookings
            modelBuilder.Entity<User>()
           .HasMany(u => u.Bookings)
           .WithOne(b => b.User)
           .HasForeignKey(b => b.UserId)
           .OnDelete(DeleteBehavior.Restrict);  // prevent cascade

            // Property to Bookings
            modelBuilder.Entity<Property>()
            .HasMany(p => p.Bookings)
            .WithOne(b => b.Property)
            .HasForeignKey(b => b.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);  // only allow this one to cascade
        }
    }
}
