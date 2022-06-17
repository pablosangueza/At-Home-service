using AtHome.APIHandler.interfaces;
using AtHome.Domain;

namespace AtHome.APIHandler.handlers
{
    public class ShippingCompanyAPI : IShippingCompanyAPI
    {
        public double GetOffer(ShippingCompany company, ShippingInfo shipingInfo)
        {
            Random random =new Random();
            return random.NextDouble() * (100 - 0) + 0;
        }
    }
}