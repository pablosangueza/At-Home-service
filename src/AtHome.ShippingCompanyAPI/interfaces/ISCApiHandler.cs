using AtHome.Domain;

namespace AtHome.ShippingCompanyAPI.interfaces
{
    public interface ISCApiHandler
    {
        Task<decimal> ProcessShippingCalculationAsync(ShippingCompany company, ShippingInfo shipingInfo);
    }
}