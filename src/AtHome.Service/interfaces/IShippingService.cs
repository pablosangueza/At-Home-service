using AtHome.Domain;
using AtHome.Service.models;

namespace AtHome.Service.interfaces
{
    public interface IShippingService
    {
         void AddShippingCompany(ShippingCompany company);
         
          CompanyShippingDeal FindBestDeal(ShippingInfo shipingInfo);
    }
}