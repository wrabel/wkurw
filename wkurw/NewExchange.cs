using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace wkurw
{
    public class NewExchange
    {
        public static async Task<dynamic> getDataFromWeb(string baseCurrency, string destCurrency)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync ("http://api.fixer.io/latest?base=" + baseCurrency + "&symbols=" + destCurrency);

            if (response != null)
            {
                var rates = JsonConvert.DeserializeObject<Fixer>(await response.Content.ReadAsStringAsync());
                var exchange = rates.Rates[destCurrency];
                return double.Parse(exchange);

            }
            return 0 ;
        }
    }
}