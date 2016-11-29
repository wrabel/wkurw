using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;

namespace wkurw
{
    public class NewCore
    {
        public static async Task<Fixer> PobierzDane (string pierwszy, string drugi)
        {
            dynamic result = await NewExchange.getDataFromWeb(pierwszy, drugi).ConfigureAwait(false);

            if (result["date"]!= null)
            {
                Fixer data = new Fixer();
                data.Date = (string)result["date"];
                data.BaseCurrency = (string)result["base"];
                data.Rates = (Dictionary<string, string>)result["rates"];
                return data;
            }
            else
            {
                return null;
            }
            
        }
    }
}