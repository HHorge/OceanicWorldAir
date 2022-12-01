using System.Text.Json.Serialization;

namespace OceanicWorldAirService.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Company
    {
        EastIndiaTrading = 1,

        TelestarLogistics = 2,

        OceanicAirlines = 3,
    }
}
