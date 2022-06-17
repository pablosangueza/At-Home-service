using AtHome.Domain;

namespace AtHome.DataRepository.interfaces
{
    public interface IRepository
    {
        IList<ShippingCompany> GetShippingCompaniesInfo();
        void StoreShippingCompany(ShippingCompany company);
    }
}