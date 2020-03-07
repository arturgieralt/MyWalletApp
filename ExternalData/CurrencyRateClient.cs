using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MyWalletApp.ExternalData
{
    public class CurrencyRateClient
    {
        private HttpClient _httpClient;
        public CurrencyRateClient (HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task<ExchangeRatesApiResponse> GetRatesForTimePeriod(DateTime startDate, DateTime endDate, string baseCurrency, string[] currencies) {
            var symbols = string.Join(",", currencies);
            var start = startDate.ToString(ExchangeRateConfig.API_DATE_STRING_FORMAT);
            var end = endDate.ToString(ExchangeRateConfig.API_DATE_STRING_FORMAT);
            var url = $"https://api.exchangeratesapi.io/history?start_at={start}&end_at={end}&base{baseCurrency}&symbols={symbols}";

            var contentString = await _httpClient.GetStringAsync(url);

            return JsonConvert.DeserializeObject<ExchangeRatesApiResponse>(contentString);
        }
    }
}