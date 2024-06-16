using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RazorPagesApp.Pages
{
    public class SoapClientModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SoapClientModel> _logger;

        public SoapClientModel(HttpClient httpClient, ILogger<SoapClientModel> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        [BindProperty]
        public string Response { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                XNamespace soapenv = "http://schemas.xmlsoap.org/soap/envelope/";
                XNamespace web = "http://www.dataaccess.com/webservicesserver/";

                var soapEnvelope = new XElement(soapenv + "Envelope",
                    new XAttribute(XNamespace.Xmlns + "soapenv", soapenv.NamespaceName),
                    new XAttribute(XNamespace.Xmlns + "web", web.NamespaceName),
                    new XElement(soapenv + "Header"),
                    new XElement(soapenv + "Body",
                        new XElement(web + "ConversionRate",
                            new XElement(web + "FromCurrency", "USD"),
                            new XElement(web + "ToCurrency", "EUR"))));

                _logger.LogInformation("SOAP Request: {0}", soapEnvelope);

                var content = new StringContent(soapEnvelope.ToString(), Encoding.UTF8, "text/xml");
                content.Headers.ContentType = new MediaTypeHeaderValue("text/xml");

                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));

                var result = await _httpClient.PostAsync("https://www.dataaccess.com/webservicesserver/NumberConversion.wso", content);

                if (!result.IsSuccessStatusCode)
                {
                    _logger.LogError("SOAP Request failed with status code: {0}, reason: {1}", result.StatusCode, result.ReasonPhrase);
                    Response = $"Error: {result.ReasonPhrase}";
                }
                else
                {
                    Response = await result.Content.ReadAsStringAsync();
                    _logger.LogInformation("SOAP Response: {0}", Response);
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError("HTTP Request Exception: {0}", httpEx);
                Response = $"HTTP Request Exception: {httpEx.Message}";
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception: {0}", ex);
                Response = $"Exception: {ex.Message}";
            }

            return Page();
        }
    }
}