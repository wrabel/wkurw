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

namespace wkurw
{
    public class NewDataExchange
    {
        public string BaseCurrency { get; set; }
        
        public string Date { get; set; }

        public Dictionary<string, string> Rates { get; set; }

        
    }

}