using System;
using System.Collections.Generic;
using AtHome.APIHandler.interfaces;
using AtHome.DataRepository.interfaces;
using AtHome.Domain;
using AtHome.Service.interfaces;
using AtHome.Service.services;
using Moq;
using NUnit.Framework;

namespace AtHome.Service.Test;

public class ShippingServiceTest
{

    private IShippingService _shippingService;
    private Mock<IRepository> _mockRepository;
    private Mock<IShippingCompanyAPI> _mockSHAPI;

    
    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IRepository>();
        _mockSHAPI = new Mock<IShippingCompanyAPI>();
        _shippingService = new ShippingService(_mockRepository.Object, _mockSHAPI.Object);
    }

    [Test]
    public void TestBeastDeal()
    {
        ShippingInfo shippingInfo = new ShippingInfo()
        {
            Source = new Coordinate { Latitde = 123, Longitude = 566},
            Destination = new Coordinate { Latitde = 789, Longitude = 987},
            Dimentions = new double[] {50,50,50}
            
        };

        List<ShippingCompany> companies = new List<ShippingCompany>();
        companies.Add (new ShippingCompany(){ServiceURI = "https://company1.net/getoffer", ServiceType = ServiceType.REST, FormatResponse= FormatResponse.JSON});
        companies.Add (new ShippingCompany(){ServiceURI = "https://company2.com/calcaulate", ServiceType = ServiceType.REST, FormatResponse= FormatResponse.JSON });
        companies.Add (new ShippingCompany(){ServiceURI = "https://company3.com/shipammount", ServiceType = ServiceType.SOAP, FormatResponse= FormatResponse.XML});
        
        _mockRepository.Setup(s => s.GetShippingCompaniesInfo()).Returns(companies);

        _mockSHAPI.Setup( s=>s.GetOffer(It.IsAny<ShippingCompany>(), shippingInfo)).Returns(new Random().NextDouble() * (100 - 0) + 0);

        var deal =_shippingService.FindBestDeal(shippingInfo);

        Assert.Pass();
    }
    [Test]
    public void TestAddShippingCompanies()
    {
        var company = new ShippingCompany(){ServiceURI = "https://company1.net/getoffer", ServiceType = ServiceType.REST, FormatResponse= FormatResponse.JSON};

        _shippingService.AddShippingCompany(company);

        Assert.Pass();

    }
}