using AtHome.Domain;
using AtHome.ShippingCompanyAPI.interfaces;

namespace AtHome.ShippingCompanyAPI.handlers
{
    public class MockRandomApiHandler : ISCApiHandler
    {
        public async Task<decimal> ProcessShippingCalculationAsync(ShippingCompany company, ShippingInfo shipingInfo)
        {
            Random random = new Random();
            decimal value = (decimal)random.NextDouble() * (100 - 0) + 0;
            return value;
        }
    }
}