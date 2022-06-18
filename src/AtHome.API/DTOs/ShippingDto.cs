using System.ComponentModel;

namespace AtHome.API.DTOs
{
    public class ShippingDto
    {
        public string Source { get; set; }
        public string Destination { get; set; }

        [DefaultValue(new double[] {50,30,40})]
        public double[] Dimentions { get; set; }

    }
}