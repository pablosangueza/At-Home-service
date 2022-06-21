using AtHome.Domain;
using AtHome.ShippingCompanyAPI.interfaces;

namespace AtHome.ShippingCompanyAPI.handlers
{
    public class RpcApiHandler : ISCApiHandler
    {
        public Task<decimal> ProcessShippingCalculationAsync(ShippingCompany company, ShippingInfo shipingInfo)
        {
            throw new NotImplementedException();
        }
    }
}