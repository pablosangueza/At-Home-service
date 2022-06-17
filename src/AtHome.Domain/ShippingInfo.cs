namespace AtHome.Domain
{
    public class Coordinate
    {
        public int Latitde { get; set; }
        public int Longitude { get; set; }

    }
    public class ShippingInfo
    {
        public Coordinate Source { get; set; }
        public Coordinate Destination { get; set; }
        public double[] Dimentions { get; set; }


    }
}