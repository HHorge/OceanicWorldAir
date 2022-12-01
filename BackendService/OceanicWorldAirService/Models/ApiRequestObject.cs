namespace OceanicWorldAirService.Models
{
    public class ApiRequestObject
    {
        public List<Parcel> Parcels { get; set; }
        public int StartCityId { get; set; }
        public int DestinationCityId { get; set; }
    }
}
