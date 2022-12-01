using Microsoft.AspNetCore.Mvc;
using OceanicWorldAirService.Models;
using OceanicWorldAirService.Services;
using RouteModel = OceanicWorldAirService.Models.Route;

namespace OceanicWorldAirService.Controllers
{
    /// <summary>
    /// The controller for RouteFinding.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RouteFindingController : ControllerBase
    {
        private readonly IRouteFindingService _routeFindingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteFindingController"/> class.
        /// </summary>
        /// <param name="routeFindingService">Implementation of <see cref="IRouteFindingService"/> class.</param>>
        public RouteFindingController(IRouteFindingService routeFindingService)
        {
            _routeFindingService = routeFindingService;
        }

        [HttpPost(Name = "FindRoute")]
        public async Task<ActionResult<IEnumerable<RouteModel>>> FindRoutes(List<Parcel> parcelList, int startCityId, int destinationCityId)
        {
            var result = _routeFindingService.FindRoutes(parcelList, startCityId, destinationCityId);

            return Ok(result);
        }
    }
}
