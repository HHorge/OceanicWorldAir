namespace OceanicWorldAirService.Models
{
    public class ParcelDto
    {
        public float Weigth { get; set; }

        public float Depth { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }

        public bool RecordedDelivery { get; set; }

        public bool Weapons { get; set; }

        public bool LiveAnimals { get; set; }

        public bool CautiousParcels { get; set; }

        public bool RefrigeratedGoods { get; set; }
    }
}
