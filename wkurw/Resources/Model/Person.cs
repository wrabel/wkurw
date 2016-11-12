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
using SQLite;

namespace wkurw
{
    public class Person
    {
        [PrimaryKey,AutoIncrement]

        public int Id { get; set; }

        public string Nick { get; set; }

        public string Email { get; set; }

        public string data { get; set; }
       
    }
}