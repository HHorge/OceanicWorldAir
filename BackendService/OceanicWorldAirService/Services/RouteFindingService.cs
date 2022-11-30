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

        public async Task<IEnumerable<RouteModel>> FindRoutes()
        {
            return new List<RouteModel>()
            {
                new RouteModel(),
            };
        }
    }
}
