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

        [HttpPost(Name = "FindCosts")]
        public Costs FindCosts(ApiRequestObject request)
        {
            var result = _routeFindingService.FindCostForExternals(request.Parcels, request.StartCityId, request.DestinationCityId);

            return result;
        }
    }
}
