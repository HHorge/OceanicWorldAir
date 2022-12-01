using OceanicWorldAirService.Models;
using RouteModel = OceanicWorldAirService.Models.Route;

namespace OceanicWorldAirService.Services
{
    /// <summary>
    /// RouteFindingService interface.
    /// </summary>
    public interface IRouteFindingService
    {
        /// <summary>
        /// Method for retrieving the calculated Routes.
        /// </summary>
        public RouteModel FindRoutes(List<ParcelDto> parcelList, int startCityId, int destinationCityId);
        public Costs FindCostForExternals(List<Parcel> parcelList, int startCityId, int destinationCityId);
    }
}
