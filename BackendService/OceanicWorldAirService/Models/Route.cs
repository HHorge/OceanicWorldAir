namespace OceanicWorldAirService.Models
{
    public class Route
    {
        public int Id { get; set; }

        public Node Start { get; set; } = default!;

        public Node End { get; set; } = default!;

        public ICollection<Connection> Connections { get; set; } = new List<Connection>();
    }
}
