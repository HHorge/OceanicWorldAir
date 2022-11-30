namespace OceanicWorldAirService.Models
{
    public class Node
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<Connection> Connections { get; set; } = new List<Connection>();

        public int? MinCostToStart { get; set; }

        public Node NearestToStart { get; set; } = default!;

        public bool Visited { get; set; }
    }

}

