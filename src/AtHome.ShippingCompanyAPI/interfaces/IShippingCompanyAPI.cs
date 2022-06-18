using AtHome.Domain;

namespace AtHome.APIHandler.interfaces
{
    public interface IShippingCompanyAPI
    {
        decimal GetOffer(ShippingCompany company, ShippingInfo shipingInfo);
    }
}