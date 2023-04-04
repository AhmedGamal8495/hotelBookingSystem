using hotelBookingSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace hotelBookingSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Room> rooms { get; set; }
        public DbSet<RoomType> roomTypes { get; set; }
        public DbSet<Branch> branches { get; set; }
        public DbSet<Cart> carts { get; set; }


    }
}