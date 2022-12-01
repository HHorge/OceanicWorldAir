using OceanicWorldAirService.Services;

namespace OceanicWorldAirService.Models
{
    public class Connection
    {
        public int Id { get; set; }

        public Node StartNode { get; set; } = default!;

        public Node ConnectedNode { get; set; } = default!;

        public Company Company { get; set; }


        public Connection(Node start, Node end, Company comp)
        {
            StartNode = start;
            ConnectedNode = end;
            Company = comp;
        }
        /// This method should be covered in the one of the Service for example <see cref="RouteFindingService"/>
        public Costs Cost(List<Parcel> parcels)
        {
            Costs costs = new Costs();

            if (Company == Company.OceanicAirlines) //plane
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


                costs.Price = string.Format("{0:N2}%", totalPrice);
                costs.Time = 8;
            }
            else if (Company == Company.TelestarLogistics) //car
            {
                //call calculate car api
            }
            else if (Company == Company.EastIndiaTrading) //boat
            {
                //call calculate boat api
            }

            return costs;
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

        private double UsdToDkk (int Usd)
        {
            return Usd * 0.14;
        }
    }
}
