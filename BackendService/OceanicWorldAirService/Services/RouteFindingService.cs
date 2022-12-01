using RouteModel = OceanicWorldAirService.Models.Route;
using OceanicWorldAirService.Models;
using System.Collections.Immutable;
using Microsoft.Extensions.Hosting;
using System.Linq;


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
        ///
        
        private readonly ICostCalculationService _costCalculationService;

        public RouteFindingService(ICostCalculationService costCalculationService)
        {
            _costCalculationService = costCalculationService;
        }

        public BookingResponse FindRoutes(List<ParcelDto> parcels, int startCityId, int destinationCityId)
        {
            List<Parcel> parcelList = new List<Parcel>();

            foreach (ParcelDto parcelDto in parcels)
            {
                Parcel parcel = new Parcel()
                {
                    Id = Guid.NewGuid(),
                    Weigth = parcelDto.Weigth,
                    Dimensions = (parcelDto.Depth, parcelDto.Height, parcelDto.Weigth),
                    RecordedDelivery = parcelDto.RecordedDelivery,
                    Weapons = parcelDto.Weapons,
                    LiveAnimals = parcelDto.LiveAnimals,
                    CautiousParcels = parcelDto.CautiousParcels,
                    RefrigeratedGoods = parcelDto.RefrigeratedGoods
                };

                parcelList.Add(parcel);
            }

            if(!IsParcelSupported(parcelList))
            {
                //Error: your package will not fly    
            }

            List<Node> nodeList = GenerateAfricaMap();

            Node startNode = nodeList.First(p => p.Id == startCityId);
            Node endNode = nodeList.First(p => p.Id == destinationCityId);

            RouteModel route = GetShortestPathDijkstra(startNode, endNode, parcelList, false);

            float routePrice = 0;
            float routeTimeEstimat = 0;

            foreach (Connection connection in route.Connections.ToList())
            {
                if (connection.Costs.Price != null)
                {
                    routePrice += float.Parse(connection.Costs.Price);
                }

                routeTimeEstimat += connection.Costs.Time;
            }

            BookingResponse booking = new BookingResponse()
            {
                Id = Guid.NewGuid(),
                StartDestination = startNode.Name,
                EndDestination = endNode.Name,
                Price = string.Format("{0:N2}", routePrice),
                Time = routeTimeEstimat,
                Parcels = parcelList
            };

            return booking;
        }

        public Costs FindCostForExternals(List<Parcel> parcelList, int startCityId, int destinationCityId)
        {
            if (!IsParcelSupported(parcelList))
            {
                //Error: Your package will not fly
            }

            List<Node> nodeList = GenerateAfricaMap();

            Node startNode = nodeList.First(p => p.Id == startCityId);
            Node endNode = nodeList.First(p => p.Id == destinationCityId);

            List<Connection> connectionList = GetShortestPathDijkstra(startNode, endNode, parcelList).Connections.ToList();

            float routePrice = 0;
            float routeTimeEstimat = 0;

            foreach (Connection connection in connectionList)
            {
                if (connection.Costs.Price != null)
                {
                    routePrice += float.Parse(connection.Costs.Price);
                }

                routeTimeEstimat += connection.Costs.Time;
            }

            return new Costs()
            {
                Price = string.Format("{0:N2}", routePrice),
                Time = routeTimeEstimat
            };
        }

        private bool IsParcelSupported(List<Parcel> parcelList)
        {
            foreach (Parcel parcel in parcelList)
            {
                if (parcel.RecordedDelivery || parcel.LiveAnimals || parcel.Weigth > 20 || 
                    parcel.Dimensions.width > 200 || parcel.Dimensions.height > 200 || parcel.Dimensions.depth > 200)
                {
                    return false;
                }
            }

            return true;
        }

        public RouteModel GetShortestPathDijkstra(Node start, Node end, List<Parcel> parcelList, bool checkOnlyUs = true)
        {
            DijkstraSearch(start, end, parcelList, 1, checkOnlyUs);
            var shortestPath = new List<Connection>();
            //shortestPath.Add(end.NearestConnectionToStart);
            BuildShortestPath(shortestPath, end);
            shortestPath.Reverse(); // whatever if you want the correct order

            //calculate time and cost here for the shortest path and then return route
            RouteModel optimalRoute = new RouteModel();
            optimalRoute.Start = start;
            optimalRoute.End = end;
            optimalRoute.Connections = shortestPath;

            return optimalRoute;
        }

        public void BuildShortestPath(List<Connection> list, Node node)
        {
            if (node.NearestConnectionToStart == null || node.NearestToStart == null)
                return;
            list.Add(node.NearestConnectionToStart);
            BuildShortestPath(list, node.NearestToStart);
        }

        private void DijkstraSearch(Node start, Node end, List<Parcel> parcels, int searchType, bool checkOnlyUs)
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
                    if (checkOnlyUs && cnn.Company != Company.OceanicAirlines)
                    {
                        continue;
                    }
                    var childNode = cnn.ConnectedNode;
                    if (childNode.Visited)
                        continue;


                    float connectionCost;
                    Costs costObj = _costCalculationService.Cost(parcels, node.Id, childNode.Id, cnn);

                    if (costObj == null || costObj.Price == null)
                    {
                        continue;
                    }
                    else if (searchType == 0) //Cheapest Route
                    {
                        connectionCost = float.Parse(costObj.Price);
                    }
                    else if (searchType == 1) //Fastest Route
                    {
                        connectionCost = costObj.Time;
                    }
                    else 
                    {
                        connectionCost = (costObj.Time) / float.Parse(costObj.Price);
                    }

                    if (childNode.MinCostToStart == null ||
                        node.MinCostToStart + connectionCost < childNode.MinCostToStart)
                    {
                        childNode.MinCostToStart = node.MinCostToStart + connectionCost;
                        childNode.NearestConnectionToStart = cnn;
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

        public List<Node> GenerateAfricaMap()
        {
            var map = new List<Node>();

            //make all nodes
            var Sahara = new Node(0, "Sahara");
            var Timbuktu = new Node(1, "Timbuktu");
            var CanaryIslands = new Node(2, "Canary Islands");
            var Morocco = new Node(3, "Morocco");
            var CapeVerde = new Node(4, "Cape Verde");
            var SierraLeone = new Node(5, "Sierra Leone");
            var GoldCoast = new Node(6, "Gold Coast");
            var SlaveCoast = new Node(7, "Slave Coast");
            var StHelena = new Node(8, "St. Helena");
            var Congo = new Node(9, "Congo");
            var WhalefishBay = new Node(10, "Whalefish Bay");
            var Kandjama = new Node(11, "Kandjama");
            var AinGalaka = new Node(12, "Ain-Galaka");
            var Tripoli = new Node(13, "Tripoli");
            var Egypt = new Node(14, "Egypt");
            var Darfur = new Node(15, "Darfur");
            var BahrElGhasal = new Node(16, "Bahr El Ghasal");
            var Ocomba = new Node(17, "Ocomba");
            var VictoriaFalls = new Node(18, "Victoria Falls");
            var DragonMountain = new Node(19, "Dragon Mountain");
            var Tunis = new Node(20, "Tunis");
            var LakeVictoria = new Node(21, "Lake Victoria");
            var Suakin = new Node(22, "Suakin");
            var AddisAbeba = new Node(23, "Addis Abeba");
            var CapeGuardafui = new Node(24, "Cape Guardafui");
            var Tamatave = new Node(25, "Tamatave");
            var CapeStMarie = new Node(26, "Cape St.Marie");
            var Mozambique = new Node(27, "Mozambique");
            var CapeTown = new Node(28, "Cape Town");
            var Daressalam = new Node(29, "Daressalam");
            var Tangier = new Node(30, "tangier");
            var Cairo = new Node(31, "Cairo");


            //make all connections
            Tangier.Connections.Add(new Connection(Tangier, Tunis, Company.EastIndiaTrading));
            Tangier.Connections.Add(new Connection(Tangier, Tunis, Company.TelestarLogistics));
            Tangier.Connections.Add(new Connection(Tangier, Tripoli, Company.OceanicAirlines));
            Tangier.Connections.Add(new Connection(Tangier, Morocco, Company.TelestarLogistics));
            Tangier.Connections.Add(new Connection(Tangier, Morocco, Company.OceanicAirlines));
            Tangier.Connections.Add(new Connection(Tangier, CanaryIslands, Company.EastIndiaTrading));

            Tunis.Connections.Add(new Connection(Tunis, Tangier, Company.EastIndiaTrading));
            Tunis.Connections.Add(new Connection(Tunis, Tangier, Company.TelestarLogistics));
            Tunis.Connections.Add(new Connection(Tunis, Cairo, Company.EastIndiaTrading));
            Tunis.Connections.Add(new Connection(Tunis, Tripoli, Company.TelestarLogistics));

            Tripoli.Connections.Add(new Connection(Tripoli, Tunis, Company.TelestarLogistics));
            Tripoli.Connections.Add(new Connection(Tripoli, Egypt, Company.TelestarLogistics));
            Tripoli.Connections.Add(new Connection(Tripoli, Tangier, Company.OceanicAirlines));
            Tripoli.Connections.Add(new Connection(Tripoli, GoldCoast, Company.OceanicAirlines));
            Tripoli.Connections.Add(new Connection(Tripoli, Darfur, Company.OceanicAirlines));
            
            Cairo.Connections.Add(new Connection(Cairo, Tunis, Company.EastIndiaTrading));
            Cairo.Connections.Add(new Connection(Cairo, Suakin, Company.EastIndiaTrading));
            Cairo.Connections.Add(new Connection(Cairo, Egypt, Company.TelestarLogistics));
            Cairo.Connections.Add(new Connection(Cairo, Suakin, Company.OceanicAirlines));

            CanaryIslands.Connections.Add(new Connection(CanaryIslands, Tangier, Company.EastIndiaTrading));
            CanaryIslands.Connections.Add(new Connection(CanaryIslands, CapeVerde, Company.EastIndiaTrading));
            
            Morocco.Connections.Add(new Connection(Morocco, Tangier, Company.TelestarLogistics));
            Morocco.Connections.Add(new Connection(Morocco, Sahara, Company.TelestarLogistics));
            Morocco.Connections.Add(new Connection(Morocco, CapeVerde, Company.TelestarLogistics));
            Morocco.Connections.Add(new Connection(Morocco, Tangier, Company.OceanicAirlines));
            Morocco.Connections.Add(new Connection(Morocco, SierraLeone, Company.OceanicAirlines));
            Morocco.Connections.Add(new Connection(Morocco, GoldCoast, Company.OceanicAirlines));

            Sahara.Connections.Add(new Connection(Sahara, Tangier, Company.TelestarLogistics));
            Sahara.Connections.Add(new Connection(Sahara, Darfur, Company.TelestarLogistics));

            Egypt.Connections.Add(new Connection(Egypt, Cairo, Company.TelestarLogistics));
            Egypt.Connections.Add(new Connection(Egypt, Tripoli, Company.TelestarLogistics));
            Egypt.Connections.Add(new Connection(Egypt, Darfur, Company.TelestarLogistics));

            Suakin.Connections.Add(new Connection(Suakin, Darfur, Company.TelestarLogistics));
            Suakin.Connections.Add(new Connection(Suakin, Darfur, Company.OceanicAirlines));
            Suakin.Connections.Add(new Connection(Suakin, Cairo, Company.OceanicAirlines));
            Suakin.Connections.Add(new Connection(Suakin, LakeVictoria, Company.OceanicAirlines));
            Suakin.Connections.Add(new Connection(Suakin, AddisAbeba, Company.TelestarLogistics));
            Suakin.Connections.Add(new Connection(Suakin, Cairo, Company.EastIndiaTrading));
            Suakin.Connections.Add(new Connection(Suakin, CapeGuardafui, Company.EastIndiaTrading));

            CapeVerde.Connections.Add(new Connection(CapeVerde, CanaryIslands, Company.EastIndiaTrading));
            CapeVerde.Connections.Add(new Connection(CapeVerde, SierraLeone, Company.EastIndiaTrading));
            CapeVerde.Connections.Add(new Connection(CapeVerde, StHelena, Company.EastIndiaTrading));
            CapeVerde.Connections.Add(new Connection(CapeVerde, Morocco, Company.TelestarLogistics));
            CapeVerde.Connections.Add(new Connection(CapeVerde, SierraLeone, Company.TelestarLogistics));

            Timbuktu.Connections.Add(new Connection(Timbuktu, SierraLeone, Company.TelestarLogistics));
            Timbuktu.Connections.Add(new Connection(Timbuktu, GoldCoast, Company.TelestarLogistics));
            Timbuktu.Connections.Add(new Connection(Timbuktu, SlaveCoast, Company.TelestarLogistics));

            AinGalaka.Connections.Add(new Connection(AinGalaka, Darfur, Company.TelestarLogistics));
            AinGalaka.Connections.Add(new Connection(AinGalaka, SlaveCoast, Company.TelestarLogistics));
            AinGalaka.Connections.Add(new Connection(AinGalaka, Kandjama, Company.TelestarLogistics));

            Darfur.Connections.Add(new Connection(Darfur, Sahara, Company.TelestarLogistics));
            Darfur.Connections.Add(new Connection(Darfur, AinGalaka, Company.TelestarLogistics));
            Darfur.Connections.Add(new Connection(Darfur, SlaveCoast, Company.TelestarLogistics));
            Darfur.Connections.Add(new Connection(Darfur, Egypt, Company.TelestarLogistics));
            Darfur.Connections.Add(new Connection(Darfur, Suakin, Company.TelestarLogistics));
            Darfur.Connections.Add(new Connection(Darfur, BahrElGhasal, Company.TelestarLogistics));
            Darfur.Connections.Add(new Connection(Darfur, Tripoli, Company.OceanicAirlines));
            Darfur.Connections.Add(new Connection(Darfur, Suakin, Company.OceanicAirlines));
            Darfur.Connections.Add(new Connection(Darfur, Ocomba, Company.OceanicAirlines));

            SierraLeone.Connections.Add(new Connection(SierraLeone, CapeVerde, Company.TelestarLogistics));
            SierraLeone.Connections.Add(new Connection(SierraLeone, Timbuktu, Company.TelestarLogistics));
            SierraLeone.Connections.Add(new Connection(SierraLeone, CapeVerde, Company.EastIndiaTrading));
            SierraLeone.Connections.Add(new Connection(SierraLeone, GoldCoast, Company.EastIndiaTrading));
            SierraLeone.Connections.Add(new Connection(SierraLeone, StHelena, Company.EastIndiaTrading));
            SierraLeone.Connections.Add(new Connection(SierraLeone, Morocco, Company.OceanicAirlines));
            SierraLeone.Connections.Add(new Connection(SierraLeone, StHelena, Company.OceanicAirlines));

            GoldCoast.Connections.Add(new Connection(GoldCoast, Timbuktu, Company.TelestarLogistics));
            GoldCoast.Connections.Add(new Connection(GoldCoast, SierraLeone, Company.TelestarLogistics));
            GoldCoast.Connections.Add(new Connection(GoldCoast, Morocco, Company.OceanicAirlines));
            GoldCoast.Connections.Add(new Connection(GoldCoast, Tripoli, Company.OceanicAirlines));
            GoldCoast.Connections.Add(new Connection(GoldCoast, WhalefishBay, Company.OceanicAirlines));
            GoldCoast.Connections.Add(new Connection(GoldCoast, Congo, Company.OceanicAirlines));

            SlaveCoast.Connections.Add(new Connection(SlaveCoast, Timbuktu, Company.TelestarLogistics));
            SlaveCoast.Connections.Add(new Connection(SlaveCoast, AinGalaka, Company.TelestarLogistics));
            SlaveCoast.Connections.Add(new Connection(SlaveCoast, Darfur, Company.TelestarLogistics));
            SlaveCoast.Connections.Add(new Connection(SlaveCoast, Kandjama, Company.TelestarLogistics));

            BahrElGhasal.Connections.Add(new Connection(BahrElGhasal, Darfur, Company.TelestarLogistics));
            BahrElGhasal.Connections.Add(new Connection(BahrElGhasal, LakeVictoria, Company.TelestarLogistics));

            AddisAbeba.Connections.Add(new Connection(AddisAbeba, Suakin, Company.TelestarLogistics));
            AddisAbeba.Connections.Add(new Connection(AddisAbeba, CapeGuardafui, Company.TelestarLogistics));
            AddisAbeba.Connections.Add(new Connection(AddisAbeba, LakeVictoria, Company.TelestarLogistics));

            CapeGuardafui.Connections.Add(new Connection(CapeGuardafui, AddisAbeba, Company.TelestarLogistics));
            CapeGuardafui.Connections.Add(new Connection(CapeGuardafui, Daressalam, Company.TelestarLogistics));
            CapeGuardafui.Connections.Add(new Connection(CapeGuardafui, Suakin, Company.EastIndiaTrading));
            CapeGuardafui.Connections.Add(new Connection(CapeGuardafui, Mozambique, Company.EastIndiaTrading));
            CapeGuardafui.Connections.Add(new Connection(CapeGuardafui, Tamatave, Company.EastIndiaTrading));
            CapeGuardafui.Connections.Add(new Connection(CapeGuardafui, LakeVictoria, Company.OceanicAirlines));
            CapeGuardafui.Connections.Add(new Connection(CapeGuardafui, Tamatave, Company.OceanicAirlines));

            Kandjama.Connections.Add(new Connection(Kandjama, SlaveCoast, Company.TelestarLogistics));
            Kandjama.Connections.Add(new Connection(Kandjama, AinGalaka, Company.TelestarLogistics));
            Kandjama.Connections.Add(new Connection(Kandjama, Darfur, Company.TelestarLogistics));
            Kandjama.Connections.Add(new Connection(Kandjama, Congo, Company.TelestarLogistics));

            LakeVictoria.Connections.Add(new Connection(LakeVictoria, BahrElGhasal, Company.TelestarLogistics));
            LakeVictoria.Connections.Add(new Connection(LakeVictoria, Ocomba, Company.TelestarLogistics));
            LakeVictoria.Connections.Add(new Connection(LakeVictoria, AddisAbeba, Company.TelestarLogistics));
            LakeVictoria.Connections.Add(new Connection(LakeVictoria, Daressalam, Company.TelestarLogistics));
            LakeVictoria.Connections.Add(new Connection(LakeVictoria, Mozambique, Company.TelestarLogistics));
            LakeVictoria.Connections.Add(new Connection(LakeVictoria, Suakin, Company.OceanicAirlines));
            LakeVictoria.Connections.Add(new Connection(LakeVictoria, CapeGuardafui, Company.OceanicAirlines));
            LakeVictoria.Connections.Add(new Connection(LakeVictoria, DragonMountain, Company.OceanicAirlines));

            StHelena.Connections.Add(new Connection(StHelena, CapeVerde, Company.EastIndiaTrading));
            StHelena.Connections.Add(new Connection(StHelena, SierraLeone, Company.EastIndiaTrading));
            StHelena.Connections.Add(new Connection(StHelena, WhalefishBay, Company.EastIndiaTrading));
            StHelena.Connections.Add(new Connection(StHelena, CapeTown, Company.EastIndiaTrading));
            StHelena.Connections.Add(new Connection(StHelena, SierraLeone, Company.OceanicAirlines));
            StHelena.Connections.Add(new Connection(StHelena, CapeTown, Company.OceanicAirlines));

            Congo.Connections.Add(new Connection(Congo, GoldCoast, Company.OceanicAirlines));
            Congo.Connections.Add(new Connection(Congo, WhalefishBay, Company.OceanicAirlines));
            Congo.Connections.Add(new Connection(Congo, Kandjama, Company.TelestarLogistics));
            Congo.Connections.Add(new Connection(Congo, Ocomba, Company.TelestarLogistics));
            Congo.Connections.Add(new Connection(Congo, Mozambique, Company.TelestarLogistics));
            Congo.Connections.Add(new Connection(Congo, VictoriaFalls, Company.TelestarLogistics));
            Congo.Connections.Add(new Connection(Congo, DragonMountain, Company.TelestarLogistics));

            Ocomba.Connections.Add(new Connection(Ocomba, Darfur, Company.OceanicAirlines));
            Ocomba.Connections.Add(new Connection(Ocomba, CapeTown, Company.OceanicAirlines));
            Ocomba.Connections.Add(new Connection(Ocomba, Congo, Company.TelestarLogistics));
            Ocomba.Connections.Add(new Connection(Ocomba, LakeVictoria, Company.TelestarLogistics));

            Daressalam.Connections.Add(new Connection(Daressalam, CapeGuardafui, Company.TelestarLogistics));
            Daressalam.Connections.Add(new Connection(Daressalam, LakeVictoria, Company.TelestarLogistics));
            Daressalam.Connections.Add(new Connection(Daressalam, Mozambique, Company.TelestarLogistics));

            Mozambique.Connections.Add(new Connection(Mozambique, CapeGuardafui, Company.EastIndiaTrading));
            Mozambique.Connections.Add(new Connection(Mozambique, CapeStMarie, Company.EastIndiaTrading));
            Mozambique.Connections.Add(new Connection(Mozambique, Daressalam, Company.TelestarLogistics));
            Mozambique.Connections.Add(new Connection(Mozambique, DragonMountain, Company.TelestarLogistics));
            Mozambique.Connections.Add(new Connection(Mozambique, LakeVictoria, Company.TelestarLogistics));
            Mozambique.Connections.Add(new Connection(Mozambique, Congo, Company.TelestarLogistics));
            Mozambique.Connections.Add(new Connection(Mozambique, VictoriaFalls, Company.TelestarLogistics));

            WhalefishBay.Connections.Add(new Connection(WhalefishBay, SlaveCoast, Company.EastIndiaTrading));
            WhalefishBay.Connections.Add(new Connection(WhalefishBay, GoldCoast, Company.EastIndiaTrading));
            WhalefishBay.Connections.Add(new Connection(WhalefishBay, CapeTown, Company.EastIndiaTrading));
            WhalefishBay.Connections.Add(new Connection(WhalefishBay, StHelena, Company.EastIndiaTrading));
            WhalefishBay.Connections.Add(new Connection(WhalefishBay, Congo, Company.OceanicAirlines));
            WhalefishBay.Connections.Add(new Connection(WhalefishBay, CapeTown, Company.OceanicAirlines));
            WhalefishBay.Connections.Add(new Connection(WhalefishBay, GoldCoast, Company.OceanicAirlines));
            WhalefishBay.Connections.Add(new Connection(WhalefishBay, VictoriaFalls, Company.TelestarLogistics));
            WhalefishBay.Connections.Add(new Connection(WhalefishBay, CapeTown, Company.TelestarLogistics));

            VictoriaFalls.Connections.Add(new Connection(VictoriaFalls, WhalefishBay, Company.TelestarLogistics));
            VictoriaFalls.Connections.Add(new Connection(VictoriaFalls, DragonMountain, Company.TelestarLogistics));
            VictoriaFalls.Connections.Add(new Connection(VictoriaFalls, Mozambique, Company.TelestarLogistics));
            VictoriaFalls.Connections.Add(new Connection(VictoriaFalls, Congo, Company.TelestarLogistics));

            DragonMountain.Connections.Add(new Connection(DragonMountain, VictoriaFalls, Company.TelestarLogistics));
            DragonMountain.Connections.Add(new Connection(DragonMountain, Mozambique, Company.TelestarLogistics));
            DragonMountain.Connections.Add(new Connection(DragonMountain, Congo, Company.TelestarLogistics));
            DragonMountain.Connections.Add(new Connection(DragonMountain, CapeTown, Company.OceanicAirlines));
            DragonMountain.Connections.Add(new Connection(DragonMountain, LakeVictoria, Company.OceanicAirlines));

            Tamatave.Connections.Add(new Connection(Tamatave, CapeGuardafui, Company.OceanicAirlines));
            Tamatave.Connections.Add(new Connection(Tamatave, CapeGuardafui, Company.EastIndiaTrading));
            Tamatave.Connections.Add(new Connection(Tamatave, CapeTown, Company.OceanicAirlines));

            CapeStMarie.Connections.Add(new Connection(CapeStMarie, Mozambique, Company.EastIndiaTrading));
            CapeStMarie.Connections.Add(new Connection(CapeStMarie, CapeTown, Company.OceanicAirlines));
            CapeStMarie.Connections.Add(new Connection(CapeStMarie, CapeTown, Company.EastIndiaTrading));

            CapeTown.Connections.Add(new Connection(CapeTown, StHelena, Company.EastIndiaTrading));
            CapeTown.Connections.Add(new Connection(CapeTown, WhalefishBay, Company.EastIndiaTrading));
            CapeTown.Connections.Add(new Connection(CapeTown, CapeStMarie, Company.EastIndiaTrading));
            CapeTown.Connections.Add(new Connection(CapeTown, WhalefishBay, Company.TelestarLogistics));
            CapeTown.Connections.Add(new Connection(CapeTown, StHelena, Company.OceanicAirlines));
            CapeTown.Connections.Add(new Connection(CapeTown, WhalefishBay, Company.OceanicAirlines));
            CapeTown.Connections.Add(new Connection(CapeTown, Ocomba, Company.OceanicAirlines));
            CapeTown.Connections.Add(new Connection(CapeTown, DragonMountain, Company.OceanicAirlines));
            CapeTown.Connections.Add(new Connection(CapeTown, Tamatave, Company.OceanicAirlines));
            CapeTown.Connections.Add(new Connection(CapeTown, CapeStMarie, Company.OceanicAirlines));

            // add all nodes to world map
            map.Add(Sahara);
            map.Add(Timbuktu);
            map.Add(CanaryIslands);
            map.Add(Morocco);
            map.Add(CapeVerde);
            map.Add(SierraLeone);
            map.Add(GoldCoast);
            map.Add(SlaveCoast);
            map.Add(StHelena);
            map.Add(Congo);
            map.Add(WhalefishBay);
            map.Add(Kandjama);
            map.Add(AinGalaka);
            map.Add(Tripoli);
            map.Add(Egypt);
            map.Add(Darfur);
            map.Add(BahrElGhasal);
            map.Add(Ocomba);
            map.Add(VictoriaFalls);
            map.Add(DragonMountain);
            map.Add(Tunis);
            map.Add(LakeVictoria);
            map.Add(Suakin);
            map.Add(AddisAbeba);
            map.Add(CapeGuardafui);
            map.Add(Tamatave);
            map.Add(CapeStMarie);
            map.Add(Mozambique);
            map.Add(CapeTown);
            map.Add(Daressalam);
            map.Add(Tangier);
            map.Add(Cairo);

            return map;
        }


    }
}
