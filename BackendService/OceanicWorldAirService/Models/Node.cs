using System;
using System.Collections.Generic;

namespace OceanicWorldAirService.Models
{
    public class Node
    {

        public string name;
        public List<Connection> Connections;
        public int? MinCostToStart;
        public Node NearestToStart;
        public bool Visited;

    }

}

