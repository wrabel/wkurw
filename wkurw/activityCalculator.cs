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
    [Activity(Label = "activityCalculator")]
    public class activityCalculator : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.calculator);

            ActionBar.Title = " Oblicze za ciebie ;] ";
            ActionBar.SetDisplayShowTitleEnabled(true);
            // Create your application here
        }
    }
}