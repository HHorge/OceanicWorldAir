using OceanicWorldAirService.Models;
using System.Net.Http.Headers;
using System.Text;

namespace OceanicWorldAirService.Services
{
    public interface IShippingHttpRequester
    {
        public Task<Costs> EastIndiaTradingRequest(List<Parcel> parcelList, int startCityId, int destinationCityId);
    }
}
