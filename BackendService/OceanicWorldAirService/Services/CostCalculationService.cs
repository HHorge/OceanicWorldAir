using OceanicWorldAirService.Models;

namespace OceanicWorldAirService.Services
{
    public class CostCalculationService : ICostCalculationService
    {
        IShippingHttpRequester _httpRequester;

        public CostCalculationService(IShippingHttpRequester httpRequester)
        {
            _httpRequester = httpRequester;
        }

        public Costs Cost(List<Parcel> parcels, int startCityId, int destinationCityId, Connection connection)
        {
            if (connection.Company == Company.OceanicAirlines) //plane
            {
                double totalPrice = 0;

                foreach (Parcel parcel in parcels)
                {
                    double price = GetDefaulCostOfParcel(parcel);

                    if (parcel.Weapons)
                    {
                        price = price * 2;
                    }

                    if (parcel.CautiousParcels)
                    {
                        price = price * 1.75;
                    }

                    if (parcel.RefrigeratedGoods)
                    {
                        price = price * 1.1;
                    }

                    totalPrice += price;
                }

                connection.Costs = new Costs()
                {
                    Price = string.Format("{0:N2}", totalPrice),
                    Time = 8
                };

            }
            else if (connection.Company == Company.TelestarLogistics) //car
            {
                //connection.Costs =  _httpRequester.ShippingRequest(parcels, startCityId, destinationCityId, connection.Company).Result;
            }
            else if (connection.Company == Company.EastIndiaTrading) //boat
            {
                connection.Costs = _httpRequester.EastIndiaTradingRequest(parcels, startCityId, destinationCityId).Result;
            }

            return connection.Costs;
        }
        private double GetDefaulCostOfParcel(Parcel parcel)
        {
            int category = GetDimensionCategory(parcel);

            return CalculateDefaultPrice(parcel.Weigth, category);
        }

        private int GetDimensionCategory(Parcel parcel)
        {
            if (parcel.Dimensions.depth < 25 && parcel.Dimensions.width < 25 && parcel.Dimensions.height < 25)
            {
                return 1;
            }
            else if (parcel.Dimensions.depth < 40 && parcel.Dimensions.width < 40 && parcel.Dimensions.height < 40)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        private double CalculateDefaultPrice(float weigth, int category)
        {
            if (weigth < 1)
            {
                switch (category)
                {
                    case 1:
                        return UsdToDkk(40);
                    case 2:
                        return UsdToDkk(48);
                    default:
                        return UsdToDkk(80);
                }
            }
            else if (weigth < 5)
            {
                switch (category)
                {
                    case 1:
                        return UsdToDkk(60);
                    case 2:
                        return UsdToDkk(68);
                    default:
                        return UsdToDkk(100);
                }
            }
            else
            {
                switch (category)
                {
                    case 1:
                        return UsdToDkk(80);
                    case 2:
                        return UsdToDkk(88);
                    default:
                        return UsdToDkk(120);
                }
            }
        }

        private double UsdToDkk(int Usd)
        {
            return Usd * 0.14;
        }
    }
}
