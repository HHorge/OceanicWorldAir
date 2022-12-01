using Microsoft.AspNetCore.Mvc;
using OceanicWorldAirService.Context.DbModels;
using OceanicWorldAirService.Models;
using OceanicWorldAirService.Repositories;
using OceanicWorldAirService.Services;
using System.Text.Json;

namespace OceanicWorldAirService.Controllers
{
    /// <summary>
    /// The controller for RouteFinding.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class DbAddTestController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;

        public DbAddTestController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        [HttpPost(Name = "CreateBooking")]
        public Context.DbModels.Booking CreateBooking(Context.DbModels.Booking inputBody)
        {
            Models.Route route = new Models.Route()
            {
                Id = 324,
                Start = new Node(1, "testName"),
                End = new Node(4, "testend"),
                Connections = new List<Connection>()
                {
                    new Connection(new Node(3,""),new Node(5,""), new Company()),
                }
            };

            inputBody.JsonRoute = JsonSerializer.Serialize(route);

            var result = _bookingRepository.CreateBooking(inputBody);
            return result;
        }
    }
}

