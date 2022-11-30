using OceanicWorldAirService.Services;

namespace OceanicWorldAirService.Models
{
    public class Connection
    {
        public Node StartNode { get; set; } = default!;

        public Node ConnectedNode { get; set; } = default!;

        public int TransportationMethod { get; set; }


        /// This method should be covered in the one of the Service for example <see cref="RouteFindingService"/>
        public int Cost()
        {

            int cost = 0;
            if (TransportationMethod == 0) //plane
            {
                //call caluclate plane cost
            }
            else if (TransportationMethod == 0) //car
            {
                //call calculate car api
            }
            else if (TransportationMethod == 0) //boat
            {
                //call calculate boat api
            }

            return cost;
        }
    }
}
