using AtHome.API.DTOs;
using AtHome.Domain;
using AtHome.Service.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AtHome.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ShippingController : ControllerBase
    {
        private readonly ILogger<ShippingController> _logger;
        private IShippingService _shippingService;

        public ShippingController(ILogger<ShippingController> logger, IShippingService shippingService)
        {
            _logger = logger;
            _shippingService = shippingService;
        }

        [HttpGet("GetShippingCompanies")]
        public IActionResult GetShippingCompanies()
        {
            IList<ShippingCompany> companies = _shippingService.GetRegisteredShippingCompanies();
            return Ok(companies.Select(c =>
            new
            {
                CompanyName = c.Name,
                CompanyApiUrl = c.ServiceURI,
                ServiceType = c.ServiceType.ToString(),
                FormatResponse = c.FormatResponse.ToString()
            }));

        }


        [HttpPost("AddShippingCompany")]
        public IActionResult AddShippingCompany([FromBody] ShippingCompanyDto companyData)
        {
            _shippingService.AddShippingCompany(
                new Domain.ShippingCompany()
                {
                    Name = companyData.CompanyName,
                    ServiceURI = companyData.ApiUri,
                    ServiceType = GetServiceType(companyData.ServiceType),
                    FormatResponse = GetFormatResponse(companyData.FormatResponse)

                }
            );
            return Ok();
        }

        private FormatResponse GetFormatResponse(string formatResponse)
        {
            return (FormatResponse)Enum.Parse(typeof(FormatResponse), formatResponse);
        }

        private ServiceType GetServiceType(string serviceType)
        {
            return (ServiceType)Enum.Parse(typeof(ServiceType), serviceType);

        }

        [HttpGet("GetBestShippingDeal")]
        public async Task<IActionResult> GetBestShippingDeal([FromQuery] ShippingDto shippingData)
        {
            var bestDeal = await _shippingService.FindBestDealAsync(
                new Domain.ShippingInfo()
                {
                    Source = new Domain.Address() { AddressLine = shippingData.Source },
                    Destination = new Domain.Address() { AddressLine = shippingData.Destination },
                    Dimentions = shippingData.Dimentions,
                    SourceFieldName = shippingData.SourceFieldName,
                    DestinationFieldName = shippingData.DestinationFieldName,
                    DimentionsFieldName = shippingData.DimentionsFieldName

                });
            return Ok(new { CompanyName = bestDeal.Company.Name, CompanyURL = bestDeal.Company.ServiceURI, Ammont = string.Format("{0:0.00}", bestDeal.Amonut) });
        }


    }
}