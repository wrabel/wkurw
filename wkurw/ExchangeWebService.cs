using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;


//using System.Diagnostics;

namespace wkurw
{
    public class ExchangeWebService
    {
        HttpClient client;
        public ExchangeWebService()
        {
            client = new HttpClient(new Xamarin.Android.Net.AndroidClientHandler());
        }

        public async Task<Fixer> GetExchangeRates(string baseCurrency = "USD")
        {
            var endpoint = string.Format("http://api.fixer.io/latest?base={0}", baseCurrency);
//            Debug.WriteLine(endpoint);
            var uri = new Uri(endpoint);
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var rates = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Fixer>(rates);
            }
            return new Fixer();
        }

        public async Task<double> GetSingleRate(string baseCurrency = "USD", string destCurrency = "EUR")
        {
            var endpoint = string.Format("http://api.fixer.io/latest?base={0}&symbols={1}", baseCurrency, destCurrency);
  //          Debug.WriteLine(endpoint);
            var uri = new Uri(endpoint);
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var rates = JsonConvert.DeserializeObject<Fixer>(await response.Content.ReadAsStringAsync());
                var exchange = rates.Rates[destCurrency];

                return double.Parse(exchange);
            }
            return 0;
        }
    }
}
