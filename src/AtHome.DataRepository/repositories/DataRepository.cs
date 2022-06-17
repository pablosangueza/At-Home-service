using AtHome.DataRepository.interfaces;
using AtHome.Domain;

namespace AtHome.DataRepository.repositories
{
    public class DataRepository : IRepository
    {
        private static IList<ShippingCompany> _shippingCompanies = new List<ShippingCompany>();

        public IList<ShippingCompany> GetShippingCompaniesInfo()
        {
            return _shippingCompanies;
        }

        public void StoreShippingCompany(ShippingCompany company)
        {
            _shippingCompanies.Add(company);
        }
    }
}