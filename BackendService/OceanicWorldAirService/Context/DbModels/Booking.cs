using System.ComponentModel.DataAnnotations;

namespace OceanicWorldAirService.Context.DbModels
{
    public class Booking
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string JsonRoute { get; set; } = string.Empty;

        [Required]
        public int Cost { get; set; }
        
        [Required]
        public int Time { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public string JsonParcel { get; set; } = string.Empty;
    }
}
