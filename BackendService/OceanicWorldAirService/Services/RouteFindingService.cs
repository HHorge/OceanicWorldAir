using OceanicWorldAirService.Models;
using RouteModel = OceanicWorldAirService.Models.Route;

namespace OceanicWorldAirService.Services
{
    /// <summary>
    /// Implementation of RouteFindingService.
    /// </summary>
    public class RouteFindingService : IRouteFindingService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RouteFindingService"/> class.
        /// </summary>
        public RouteFindingService()
        {
        }

        public async Task<IEnumerable<RouteModel>> FindRoutes(List<Parcel> parcelList, string startCity, string destinationCity)
        {
            if(DoesItFly(parcelList, startCity, destinationCity))
            {
                //TODO: Algorithm
            } 

            return new List<RouteModel>()
            {
                new RouteModel(),
            };
        }

        private bool DoesItFly(List<Parcel> parcelList, string startCity, string destinationCity)
        {
            //TODO: Do our checks
        }
    }
}
