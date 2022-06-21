using System.Net;
using System.Net.Http.Headers;
using System.Text;
using AtHome.Domain;
using AtHome.ShippingCompanyAPI.interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AtHome.ShippingCompanyAPI.handlers
{
    public class EndpointResponseDto
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Url { get; set; }
        public JToken Payload { get; set; }
    }
    public class EndpointRequestDto
    {
        public HttpMethod HttpMethod { get; set; }
        public string RelativeUrl { get; set; }
        public JToken Payload { get; set; }
        public string MediaType { get; set; }
    }
    public class RestApiHandler : ISCApiHandler
    {
        private HttpClient _client;

        private readonly ILogger<RestApiHandler> _logger;


        private void Init(string baseUrl, string mediaType)
        {
            if (string.IsNullOrEmpty(baseUrl))
            {
                _logger.LogWarning($"baseUrl is null or empty");
                throw new ArgumentException("APIBaseUrl cannot be null or empty");
            }

            if (_client == null)
            {
                _client = new HttpClient();

                _client.BaseAddress = new Uri(baseUrl);
                _client.MaxResponseContentBufferSize = int.MaxValue;
                _client.Timeout = TimeSpan.FromSeconds(300);

                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType)); //"application/json"

            }
        }

        private async Task<EndpointResponseDto> Send(EndpointRequestDto endpoint)
        {
            if (endpoint.HttpMethod == null)
            {
                throw new ArgumentException("HttpMethod is null");
            }

            if (string.IsNullOrEmpty(endpoint.RelativeUrl))
            {
                throw new ArgumentException("Relative is null or empty");
            }

            var uri = new Uri(_client.BaseAddress, endpoint.RelativeUrl);

            HttpContent content = endpoint.Payload != null
                    ? new StringContent(JsonConvert.SerializeObject(endpoint.Payload), Encoding.UTF8, endpoint.MediaType)//"application/json"
                    : null;

            var request = new HttpRequestMessage
            {
                Method = endpoint.HttpMethod,
                RequestUri = uri,
                Content = content
            };

            try
            {
                using (var httpResponse = await _client.SendAsync(request))
                {
                    if (!httpResponse.IsSuccessStatusCode)
                    {
                        string message = $" API request to ({uri}) failed.";
                        var centralException = new Exception(message);


                        _logger.LogWarning($"{message}\n\tStatus({(int)httpResponse.StatusCode})\n\tReason ({httpResponse.ReasonPhrase})");
                        throw centralException;
                    }

                    string responseMessageContent = await httpResponse.Content.ReadAsStringAsync();
                    var response = (JToken)JsonConvert.DeserializeObject(responseMessageContent);

                    return new EndpointResponseDto
                    {
                        StatusCode = httpResponse.StatusCode,
                        Url = uri.ToString(),
                        Payload = response
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Unhandled Exception ({ex.Message})\n\tStackTrace ({ex.StackTrace})\n\tInnerException ({ex.InnerException})");
                throw ex;
            }

        }


        public async Task<decimal> ProcessShippingCalculationAsync(ShippingCompany company, ShippingInfo shipingInfo)
        {
            string mediaType = GetMediaType(company.FormatResponse);
            Init(company.ServiceURI,mediaType);

            var endpointRequest = new EndpointRequestDto
            {
                HttpMethod = HttpMethod.Get,
                RelativeUrl = string.Format(company.ServiceURI),
                Payload = JToken.FromObject(new 
                {
                    Source = shipingInfo.Source,
                    Destination = shipingInfo.Destination,
                    Dimentions = string.Format($"{shipingInfo.Dimentions[0]}, {shipingInfo.Dimentions[1]}, {shipingInfo.Dimentions[2]}")
                })
            };

            var endpointResult = await Send(endpointRequest);
            return endpointResult.Payload.ToObject<decimal>();

        }

        private string GetMediaType(FormatResponse formatResponse)
        {
            switch (formatResponse)
            {
                case FormatResponse.XML:
                    return "application/xml";
                default:
                    return "application/json";

            }
        }
    }
}