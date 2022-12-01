using OceanicWorldAirService.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace OceanicWorldAirService.Services
{
    public class ShippingHttpRequester : IShippingHttpRequester
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<Costs> EastIndiaTradingRequest(List<Parcel> parcelList, int startCityId, int destinationCityId)
        {
            ApiRequestObject request = new ApiRequestObject()
            {
                Parcels = parcelList,
                StartCityId = startCityId,
                DestinationCityId = destinationCityId
            };

            var content = JsonSerializer.Serialize(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = client.PostAsync("https://wa-eit-dk2.azurewebsites.net/GetCosts", byteContent);

            var responseString = await response.Result.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,              
            };

            Costs costs = JsonSerializer.Deserialize<Costs>(responseString, options);

            return costs;
        }
    }
}
