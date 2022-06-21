using AtHome.APIHandler.interfaces;
using AtHome.DataRepository.interfaces;
using AtHome.Domain;
using AtHome.Service.interfaces;
using AtHome.Service.models;

namespace AtHome.Service.services
{
    public class ShippingService : IShippingService
    {
        private IRepository _repository;
        private IShippingCompanyAPI _shippingCompanyAPI;

        public ShippingService(IRepository repository, IShippingCompanyAPI shippingCompanyAPI)
        {
            _repository = repository;
            _shippingCompanyAPI = shippingCompanyAPI;
        }


        public void AddShippingCompany(ShippingCompany company)
        {
           _repository.StoreShippingCompany(company);
        }

        public async Task<CompanyShippingDeal> FindBestDealAsync(ShippingInfo shipingInfo)
        {
            CompanyShippingDeal bestDeal = null;

            var companies = _repository.GetShippingCompaniesInfo();

            var deals = new List<CompanyShippingDeal>();
            foreach (ShippingCompany company in companies)
            {
                decimal ammount = await _shippingCompanyAPI.GetShippingCost(company, shipingInfo);
                if(bestDeal == null)
                    bestDeal = new CompanyShippingDeal(){ Company = company, Amonut= ammount};
                else if ( ammount < bestDeal.Amonut)
                {
                    bestDeal.Company = company;
                    bestDeal.Amonut = ammount;
                }
            }

            return bestDeal;

        }

        public IList<ShippingCompany> GetRegisteredShippingCompanies()
        {
            return _repository.GetShippingCompaniesInfo();
        }
    }
}