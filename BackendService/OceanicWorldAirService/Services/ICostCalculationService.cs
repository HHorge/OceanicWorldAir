using OceanicWorldAirService.Models;

namespace OceanicWorldAirService.Services
{
    public interface ICostCalculationService
    {
        public Costs Cost(List<Parcel> parcels, int startCityId, int destinationCityId, Connection connection, bool oceanicAirlinesSupportedParcel);
    }
}
