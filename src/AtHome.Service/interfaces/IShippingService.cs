using AtHome.Domain;
using AtHome.Service.models;

namespace AtHome.Service.interfaces
{
    public interface IShippingService
    {
        void AddShippingCompany(ShippingCompany company);
        Task<CompanyShippingDeal> FindBestDealAsync(ShippingInfo shipingInfo);
        IList<ShippingCompany> GetRegisteredShippingCompanies();
    }
}