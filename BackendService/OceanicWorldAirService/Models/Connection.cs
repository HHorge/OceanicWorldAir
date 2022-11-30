using OceanicWorldAirService.Services;

namespace OceanicWorldAirService.Models
{
    public class Connection
    {
        public int Id { get; set; }

        public Node StartNode { get; set; } = default!;

        public Node ConnectedNode { get; set; } = default!;

        public Company Company { get; set; }


        /// This method should be covered in the one of the Service for example <see cref="RouteFindingService"/>
        public int Cost()
        {

            int cost = 0;
            if (Company == Company.OceanicAirlines) //plane
            {
                //call caluclate plane cost
            }
            else if (Company == Company.TelestarLogistics) //car
            {
                //call calculate car api
            }
            else if (Company == Company.EastIndiaTrading) //boat
            {
                //call calculate boat api
            }

            return cost;
        }
    }
}
