namespace AtHome.Domain
{
    public class Address
    {
        public string AddressLine { get; set; }
        public int Latitde { get; set; }
        public int Longitude { get; set; }

    }
    public class ShippingInfo
    {
        public Address Source { get; set; }
        public Address Destination { get; set; }
        public double[] Dimentions { get; set; }

        public string SourceFieldName { get; set; }
        public string DestinationFieldName { get; set; }
        public string DimentionsFieldName { get; set; }


    }
}