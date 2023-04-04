using System.ComponentModel.DataAnnotations;

namespace hotelBookingSystem.Models
{
    public class Branch
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage ="Branch name is required")]
        public string Name { get; set; }
        public string Location { get; set; }

    }
}
