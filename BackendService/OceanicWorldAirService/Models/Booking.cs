namespace OceanicWorldAirService.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public Route Route { get; set; } = default!;

        public int Cost { get; set; }

        public int Time { get; set; }

        public int CustomerId { get; set; }

        public Parcel Parcel { get; set; } = default!;
    }
}
