using StockApp.ServicesContracts;
using System.Text.Json;

namespace StockApp.Services
{
    public class FinnhubService:IFinnhubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<Dictionary<string,object>> GetStockQuote(string stockQuote)
        {

            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage requestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockQuote}&token={_configuration["FinnhubToken"]}"),
                    Method = HttpMethod.Get,
                };

                HttpResponseMessage responeMessage = await httpClient.SendAsync(requestMessage);

                Stream stream = responeMessage.Content.ReadAsStream();
                StreamReader reader = new StreamReader(stream);
                string response = reader.ReadToEnd();

                Dictionary<string,object>? DectioanryResponse = JsonSerializer.Deserialize<Dictionary<string,object>>(response);

                if(DectioanryResponse == null)
                {
                    throw new InvalidOperationException("some error with finnhub Server");
                }
                if (DectioanryResponse.ContainsKey("error"))
                    throw new InvalidOperationException(DectioanryResponse["error"].ToString());

                return DectioanryResponse;
            }
        }
    }
}
