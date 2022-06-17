using AtHome.Domain;

namespace AtHome.APIHandler.interfaces
{
    public interface IShippingCompanyAPI
    {
        double GetOffer(ShippingCompany company, ShippingInfo shipingInfo);
    }
}