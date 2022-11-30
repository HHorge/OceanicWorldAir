using Microsoft.AspNetCore.Mvc;
using OceanicWorldAirService.Services;

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

        [HttpGet(Name = "FindRoute")]
        public async Task<ActionResult<IEnumerable<int>>> FindRoutes()
        {
            // TODO: Change the return type to IEnumerable<model with retun body like nodes, total price, total time>
            var result = await _routeFindingService.FindRoutes();

            return Ok(result);
        }
    }
}
