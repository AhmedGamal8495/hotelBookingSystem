using Microsoft.AspNetCore.Identity;

namespace hotelBookingSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
