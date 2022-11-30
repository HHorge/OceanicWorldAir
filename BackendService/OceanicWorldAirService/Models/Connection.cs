using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OceanicWorldAirService.Models
{
    public class Connection
    {
        public Node startNode;
        public Node ConnectedNode;
        public int transportationMethod;
        public int Cost()
        {

            int cost = 0;
            if (this.transportationMethod == 0) //plane
            {
                //call caluclate plane cost
            }
            else if (this.transportationMethod == 0) //car
            {
                //call calculate car api
            }
            else if (this.transportationMethod == 0) //boat
            {
                //call calculate boat api
            }

            return cost;
        }
    }
}
