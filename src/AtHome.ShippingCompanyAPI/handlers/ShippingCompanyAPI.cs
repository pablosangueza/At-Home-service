using AtHome.APIHandler.interfaces;
using AtHome.Domain;
using AtHome.ShippingCompanyAPI.interfaces;
using Microsoft.Extensions.Logging;

namespace AtHome.APIHandler.handlers
{
    public class ShippingCompanyAPI : IShippingCompanyAPI
    {
        private ISCHandlerResolver _scHandlerResolver;
        private readonly ILogger<ShippingCompanyAPI> _logger;


        public ShippingCompanyAPI(ILogger<ShippingCompanyAPI> logger, ISCHandlerResolver scHandlerResolver)
        {
            _scHandlerResolver = scHandlerResolver;
            _logger = logger;
        }

        public async Task<decimal> GetShippingCost(ShippingCompany company, ShippingInfo shipingInfo)
        {

            ISCApiHandler scApiHandler = _scHandlerResolver.Resolve(company.ServiceType);
            decimal ammount = 0;
            try
            {
                if (scApiHandler != null)
                    ammount = await scApiHandler.ProcessShippingCalculationAsync(company, shipingInfo);
                else
                    throw new Exception($"There is not API Hanlder for: {company.ServiceType.ToString()}");

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                throw ex;
            }

            return ammount;

         
        }
    }
}