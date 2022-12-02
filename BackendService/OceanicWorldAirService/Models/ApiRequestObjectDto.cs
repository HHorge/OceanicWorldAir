namespace OceanicWorldAirService.Models
{
    public class ApiRequestObjectDto
    {
        public List<ParcelDto> Parcels { get; set; }
        public int StartCityId { get; set; }
        public int DestinationCityId { get; set; }
    }
}
