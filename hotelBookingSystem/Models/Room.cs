using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotelBookingSystem.Models
{
    public class Room
    {
        [Required(ErrorMessage = "Id Is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Number Is required")]
        public int Number { get; set; }

        [Required(ErrorMessage = "Price Is required")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Status Is required")]
        public bool Status { get; set; }

        public string Image { get; set; }

        [NotMapped]
        public IFormFile file { get; set; }

        public int RoomTypeId { get; set; }
        public  RoomType roomType { get; set; }

        public int BranchId { get; set; }
        public Branch branch { get; set; }

    }
}
