using System.ComponentModel.DataAnnotations;

namespace OceanicWorldAirService.Models
{
    public class Costs
    {
        [Required]
        public Guid Id { get; set; } 

        public string? Price { get; set; } //DKK

        public float Time { get; set; } //Hours
    }
}
