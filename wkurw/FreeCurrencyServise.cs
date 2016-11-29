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
using Newtonsoft.Json.Linq;

namespace wkurw
{
    public class FreeCurrencyServise
    {
        public double GetDataFromService (string waluta1, string waluta2)
        {
            var srodek = waluta1 + "_" + waluta2;

            var exUrl = "http://free.currencyconverterapi.com/api/v3/convert?q=" + srodek + "&compact=y";
            string kursWymiany = "";
            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(exUrl);

                var stuff = JObject.Parse(json);


                foreach (var rate in stuff[srodek].Cast<JProperty>())
                {
                    kursWymiany = rate.Value.ToString();
                }
            }
            return double.Parse(kursWymiany);
        }
    }
}