using Microsoft.EntityFrameworkCore;
using OceanicWorldAirService.Context;
using OceanicWorldAirService.Context.DbModels;
using OceanicWorldAirService.Helpers;

namespace OceanicWorldAirService.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DatabaseContext _context;

        public BookingRepository(DatabaseContext context)
        {
            _context = context;
        }

        public Booking CreateBooking(Booking body)
        {
            _context.Set<Booking>().Add(body);
            _context.SaveChanges();
            return body;
        }
    }
}
