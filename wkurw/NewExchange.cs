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
            dynamic data = null;

            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject(json);
                
            }
            return data;
        }
    }
}