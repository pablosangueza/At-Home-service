using AtHome.Domain;

namespace AtHome.APIHandler.interfaces
{
    public interface IShippingCompanyAPI
    {
        Task<decimal> GetShippingCost(ShippingCompany company, ShippingInfo shipingInfo);
    }
}