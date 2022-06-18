using System.ComponentModel;

namespace AtHome.API.DTOs
{
    public class ShippingCompanyDto
    {
        [DefaultValue("Company Name A")]
        public string CompanyName { get; set; }

        [DefaultValue("https://companya.com/calculateshipping")]
        public string ApiUri { get; set; }

        [DefaultValue("REST")]
        public string ServiceType { get; set; }

        [DefaultValue("JSON")]
        public string FormatResponse { get; set; }
    }
}