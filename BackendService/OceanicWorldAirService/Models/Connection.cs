using OceanicWorldAirService.Services;

namespace OceanicWorldAirService.Models
{
    public class Connection
    {
        public int Id { get; set; }

        public Node StartNode { get; set; } = default!;

        public Node ConnectedNode { get; set; } = default!;

        public Company Company { get; set; }

        public Costs Costs { get; set; }


        public Connection(Node start, Node end, Company comp)
        {
            StartNode = start;
            ConnectedNode = end;
            Company = comp;
        }
    }
}
