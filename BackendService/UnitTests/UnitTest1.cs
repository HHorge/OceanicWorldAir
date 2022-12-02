using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OceanicWorldAirService.Models;
using OceanicWorldAirService.Services;
using System.Text.Json;
using RouteModel = OceanicWorldAirService.Models.Route;
using Moq;


namespace UnitTests
{
    public class Tests
    {
       // private readonly Mock<IShippingHttpRequester> _mockIShippingHttpRequester;
       // private readonly Mock<>
       //
       // public Tests()
       // {
       //     _mockIShippingHttpRequester = new Mock<IShippingHttpRequester>();
       // }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckRoute()
        {
            List<ParcelDto> parcels = new List<ParcelDto>();
            var parcel1 = new ParcelDto()
            {
                Weigth = 1,
                Weapons = false,
                RecordedDelivery = false,
                LiveAnimals = false,
                CautiousParcels = true,
                RefrigeratedGoods = false
            };
            parcels.Add(parcel1);

           
            var startnode = 5;
            var endnode = 6;

            //_mockIShippingHttpRequester
            //    .Setup(requester => requester.EastIndiaTradingRequest(parcels, startnode, endnode))
            //    .ReturnsAsync(new Costs());

            var costCalcService = new CostCalculationService(new ShippingHttpRequester());
            var service = new RouteFindingService(costCalcService);

            //BookingResponse route = service.FindRoutes(parcels, startnode, endnode);

            //Assert.AreEqual(route.Start.Id, startnode);
            //Assert.AreEqual(route.End.Id, endnode);
            Assert.Pass();
        }

        [Test]
        public void test2()
        {

        }
    }
}