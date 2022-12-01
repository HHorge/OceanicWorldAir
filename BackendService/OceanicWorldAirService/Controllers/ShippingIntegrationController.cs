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
    public class ShippingIntegrationController : ControllerBase
    {
        private readonly IRouteFindingService _routeFindingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteFindingController"/> class.
        /// </summary>
        /// <param name="routeFindingService">Implementation of <see cref="IRouteFindingService"/> class.</param>>
        public ShippingIntegrationController(IRouteFindingService routeFindingService)
        {
            _routeFindingService = routeFindingService;
        }

        [HttpGet(Name = "FindCost")]
        public async Task<ActionResult<Costs>> FindCosts(List<Parcel> parcelList, string startCity, string destinationCity)
        {
            var result = await _routeFindingService.FindCostForExternals(parcelList, startCity, destinationCity);

            return Ok(result);
        }
    }
}
