using System.ComponentModel;

namespace AtHome.API.DTOs
{
    public class ShippingDto
    {
        [DefaultValue("Source")]
        public string SourceFieldName { get; set; }
        [DefaultValue("Destination")]
        public string DestinationFieldName { get; set; }
        [DefaultValue("Dimentions")]
        public string DimentionsFieldName { get; set; }



        [DefaultValue("Source Address")]
        public string Source { get; set; }
        [DefaultValue("Destination Address")]
        public string Destination { get; set; }

        [DefaultValue(new double[] {50,30,40})]
        public double[] Dimentions { get; set; }

    }
}