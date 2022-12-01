﻿using OceanicWorldAirService.Models;
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

        public async Task<IEnumerable<RouteModel>> FindRoutes(List<Parcel> parcelList, string startCity, string destinationCity)
        {
            if(!DoesItFly(parcelList, startCity, destinationCity))
            {
                //Error: your package will not fly    
            }

            //TODO: Call Algorithm (GetShortestPathDijkstra)
            throw new NotImplementedException();
        }

        public Task<Costs> FindCostForExternals(List<Parcel> parcelList, string startCity, string destinationCity)
        {
            if (!DoesItFly(parcelList, startCity, destinationCity))
            {
                //Error: Your package will not fly
            }

            throw new NotImplementedException();
            //TODO: Find Price and Time estimat between the destinations for the externals and return it as the Object "Costs"
        }

        private bool DoesItFly(List<Parcel> parcelList, string startCity, string destinationCity)
        {
            //TODO: Do our checks
            foreach (Parcel parcel in parcelList)
            {
                if (parcel.RecordedDelivery)
                {
                    //return error (not supported)
                }
                if (parcel.Weapons)
                {
                    //extra price +100%
                }
                if (parcel.LiveAnimals)
                {
                    //return error (not supported)
                }
                if (parcel.CautiousParcels)
                {
                    //extra price +75%
                }
                if (parcel.RefrigeratedGoods)
                {
                    //extra price +10%
                }
            }

            throw new NotImplementedException();

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
