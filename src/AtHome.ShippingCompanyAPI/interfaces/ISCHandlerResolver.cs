using AtHome.Domain;

namespace AtHome.ShippingCompanyAPI.interfaces
{
    public interface ISCHandlerResolver
    {
        ISCApiHandler Resolve(ServiceType serviceType);
    }
}