namespace OceanicWorldAirService.Models
{
    public class BookingResponse
    {
        public Guid Id { get; set; }
        public string StartDestination { get; set; } = string.Empty;
        public string EndDestination { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;

        public float Time { get; set; }
        public List<Parcel> Parcels { get; set; } = new List<Parcel>();
    }
}
