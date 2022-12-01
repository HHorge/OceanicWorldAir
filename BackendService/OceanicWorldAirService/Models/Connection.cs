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
            return 2;
        }
    }
}
