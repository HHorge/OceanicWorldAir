using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using OceanicWorldAirService.Context.DbModels;
using OceanicWorldAirService.Models;
using OceanicWorldAirService.Repositories;
using OceanicWorldAirService.Services;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OceanicWorldAirService.Controllers
{
    /// <summary>
    /// The controller for RouteFinding.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class DbAddController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;

        public DbAddController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        [HttpPost("CreateBooking")]
        public Booking CreateBooking(BookingResponse inputBody)
        {
            if (inputBody == null)
            {
                return new Booking();
            }

            string jsonParcelList = "";

            foreach(var parcel in inputBody.Parcels)
            {
                parcel.Id = Guid.NewGuid();
            }

            try
            {
                jsonParcelList = JsonSerializer.Serialize(inputBody.Parcels);
            }
            catch(Exception ex)
            {
                jsonParcelList = ex.ToString();
            }

            var createBookingBody = new Booking()
            {
                Id = inputBody.Id,
                StartDestination = inputBody.StartDestination,
                EndDestination = inputBody.EndDestination,
                Price = inputBody.Price,
                Time = inputBody.Time,
                JsonParcelsList = jsonParcelList,
            };

            var result = _bookingRepository.CreateBooking(createBookingBody);
            return result;
        }
    }
}

