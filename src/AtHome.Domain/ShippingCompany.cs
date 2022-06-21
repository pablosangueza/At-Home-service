namespace AtHome.Domain
{
    public enum ServiceType
    {
        REST = 0,
        SOAP = 1,
        RPC = 2,
        REST_RANDOM_MOCK = 3
    }
    public enum FormatResponse
    {
        JSON = 0,
        XML = 1
    }
    public class ShippingCompany
    {
        public string Name { get; set; }
        public string ServiceURI { get; set; }
        public ServiceType ServiceType { get; set; }
        public FormatResponse FormatResponse { get; set; }

    }
}