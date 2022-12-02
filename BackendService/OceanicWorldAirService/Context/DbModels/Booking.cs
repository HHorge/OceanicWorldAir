using System.ComponentModel.DataAnnotations;

namespace OceanicWorldAirService.Context.DbModels
{
    public class Booking
    {
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        public string StartDestination { get; set; } = string.Empty;

        [Required]
        public string EndDestination { get; set; } = string.Empty;

        [Required]
        public string Price { get; set; } = string.Empty;

        [Required]
        public float Time { get; set; }

        [Required]
        public string JsonParcelsList { get; set; } = string.Empty;
    }
}
