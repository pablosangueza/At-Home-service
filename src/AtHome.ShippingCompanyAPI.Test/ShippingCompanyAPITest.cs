using AtHome.APIHandler.interfaces;
using AtHome.ShippingCompanyAPI.interfaces;
using AtHome.ShippingCompanyAPI.handlers;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using AtHome.Domain;
using System.Threading.Tasks;

namespace AtHome.ShippingCompanyAPI.Test;

public class ShippingCompanyAPITest
{
    private IShippingCompanyAPI _shippingCompanyAPI;
    private Mock<ILogger<AtHome.APIHandler.handlers.ShippingCompanyAPI>> _mockLogger;
    private Mock<ISCHandlerResolver> _mockScHandlerResolver;
    [SetUp]
    public void Setup()
    {
        _mockLogger = new Mock<ILogger<AtHome.APIHandler.handlers.ShippingCompanyAPI>>();
        _mockScHandlerResolver = new Mock<ISCHandlerResolver>();
        _shippingCompanyAPI = new AtHome.APIHandler.handlers.ShippingCompanyAPI(_mockLogger.Object, _mockScHandlerResolver.Object);


    }

    [Test]
    public async Task TestGetShippingAmmountAsync()
    {
        ShippingInfo shippingInfo = new ShippingInfo()
        {
            Source = new Address { AddressLine= "Av. Address 1", Latitde = 123, Longitude = 566},
            Destination = new Address { AddressLine= "Av. Address 12", Latitde = 789, Longitude = 987},
            Dimentions = new double[] {50,50,50}
            
        };

        var company = new ShippingCompany(){ServiceURI = "https://company1.net/getoffer", ServiceType = ServiceType.REST, FormatResponse= FormatResponse.JSON};

        _mockScHandlerResolver.Setup(s => s.Resolve(It.IsAny<ServiceType>())).Returns(new MockRandomApiHandler());

        decimal ammonut =await _shippingCompanyAPI.GetShippingCost(company,shippingInfo);


        Assert.Pass();
    }
}