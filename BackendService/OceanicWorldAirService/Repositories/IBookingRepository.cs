using OceanicWorldAirService.Context.DbModels;

namespace OceanicWorldAirService.Repositories
{
    public interface IBookingRepository
    {
        Booking CreateBooking(Booking body);
    }
}
