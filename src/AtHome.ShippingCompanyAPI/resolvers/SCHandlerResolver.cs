using AtHome.Domain;
using AtHome.ShippingCompanyAPI.handlers;
using AtHome.ShippingCompanyAPI.interfaces;

namespace AtHome.ShippingCompanyAPI.resolvers
{
    public class SCHandlerResolver : ISCHandlerResolver
    {
        private readonly IEnumerable<ISCApiHandler> _scHandlers;

        public SCHandlerResolver(IEnumerable<ISCApiHandler> scHandlers)
        {
            _scHandlers = scHandlers;
        }

        public ISCApiHandler Resolve(ServiceType serviceType)
        {
            switch (serviceType)
            {
                case ServiceType.REST:
                    return _scHandlers.SingleOrDefault(x => x.GetType().ToString() == typeof(RestApiHandler).ToString());
                case ServiceType.SOAP:
                    return _scHandlers.SingleOrDefault(x => x.GetType().ToString() == typeof(SoapApiHandler).ToString());
                case ServiceType.RPC:
                    return _scHandlers.SingleOrDefault(x => x.GetType().ToString() == typeof(RpcApiHandler).ToString());
                case ServiceType.REST_RANDOM_MOCK:
                    return _scHandlers.SingleOrDefault(x => x.GetType().ToString() == typeof(MockRandomApiHandler).ToString());

                default:
                    return null;
            }


        }
    }
}