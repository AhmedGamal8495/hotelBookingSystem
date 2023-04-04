using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotelBookingSystem.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        public Room room { get; set; }

        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }

        [Required(ErrorMessage ="Date Is Required")]
        public DateTime FromDate { get; set; }
        [Required(ErrorMessage = "Date Is Required")]
        public DateTime ToDate { get; set; }

        public double TotalPrice { get; set; }

    }
}
