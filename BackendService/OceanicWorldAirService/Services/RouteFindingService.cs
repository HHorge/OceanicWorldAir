using RouteModel = OceanicWorldAirService.Models.Route;

namespace OceanicWorldAirService.Services
{
    /// <summary>
    /// Implementation of RouteFindingService.
    /// </summary>
    public class RouteFindingService : IRouteFindingService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RouteFindingService"/> class.
        /// </summary>
        public RouteFindingService()
        {
        }

        public async Task<IEnumerable<RouteModel>> FindRoutes()
        {
            return new List<RouteModel>()
            {
                new RouteModel(),
            };
        }
        public List<Node> GetShortestPathDijkstra(Node start, Node end)
        {
            DijkstraSearch(start, end);
            var shortestPath = new List<Node>();
            shortestPath.Add(end);
            BuildShortestPath(shortestPath, end);
            shortestPath.Reverse();
            return shortestPath;
        }

        public void BuildShortestPath(List<Node> list, Node node)
        {
            if (node.NearestToStart == null)
                return;
            list.Add(node.NearestToStart);
            BuildShortestPath(list, node.NearestToStart);
        }

        private void DijkstraSearch(Node start, Node end)
        {
            start.MinCostToStart = 0;
            var prioQueue = new List<Node>();
            prioQueue.Add(start);
            do
            {
                prioQueue = prioQueue.OrderBy(x => x.MinCostToStart).ToList();
                var node = prioQueue.First();
                prioQueue.Remove(node);
                foreach (var cnn in node.Connections)
                {
                    var childNode = cnn.ConnectedNode;
                    if (childNode.Visited)
                        continue;
                    if (childNode.MinCostToStart == null ||
                        node.MinCostToStart + cnn.Cost() < childNode.MinCostToStart)
                    {
                        childNode.MinCostToStart = node.MinCostToStart + cnn.Cost();
                        childNode.NearestToStart = node;
                        if (!prioQueue.Contains(childNode))
                            prioQueue.Add(childNode);
                    }
                }
                node.Visited = true;
                if (node == end)
                    return;
            } while (prioQueue.Any());
        }



    }
}
