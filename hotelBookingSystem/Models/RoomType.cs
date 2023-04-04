using System.ComponentModel.DataAnnotations;

namespace hotelBookingSystem.Models
{
    public class RoomType
    {
        [Required(ErrorMessage = "ID Is required")]
        public int Id { get; set; }

       [Required(ErrorMessage = "Name Is required")]
        public string Name { get; set; }

        public string description { get; set; }
    }
}
