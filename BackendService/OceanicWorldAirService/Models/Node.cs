namespace OceanicWorldAirService.Models
{
    public class Node
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<Connection> Connections { get; set; } = new List<Connection>();

        public int? MinCostToStart { get; set; }

        public Connection NearestConnectionToStart { get; set; } = default!;
        public Node? NearestToStart { get; set; }

        public bool Visited { get; set; }

        public Node(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

}

