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
        private Button buttom_ob;
        private TextView txt_wynik;
        private Switch switch1; 
        private Switch switch2;
        private EditText txt1;
        //private string cos;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.calculator);

            ActionBar.Title = " Oblicze za ciebie ;] ";
            ActionBar.SetDisplayShowTitleEnabled(true);

            buttom_ob = FindViewById<Button>(Resource.Id.button_oblicz);
            txt_wynik = FindViewById<TextView>(Resource.Id.txt_wynik);
            switch2 = FindViewById<Switch>(Resource.Id.switch_dziel_mnoz);
            switch1 = FindViewById<Switch>(Resource.Id.switch_dodac_odjac);
            txt1 = FindViewById<EditText>(Resource.Id.txt_waluta_1);

            buttom_ob.Click += oblicz_click;

        }
        private void oblicz_click (object sender, EventArgs e)
        {
           // cos = txt1.Text;
            txt_wynik.Text = string.Format("wynik: {0}", txt1.Text);
        }
    }
}