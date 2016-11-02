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
        private Switch switch1,switch2; 
        private EditText txt1, txt2;

        private TextView kombi1, kombi2; // to beda pola kombi ale tymczasowo zwykly text

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
            txt2 = FindViewById<EditText>(Resource.Id.txt_waluta_2);

            kombi1 = FindViewById<TextView>(Resource.Id.txt_kombi_1);
            kombi2 = FindViewById<TextView>(Resource.Id.txt_kombi_2);


            buttom_ob.Click += oblicz_click;

            kombi2.Visibility = ViewStates.Gone; //bedzie kombi

            txt2.Visibility = ViewStates.Gone;

            switch1.CheckedChange += visible_of_first;
            switch2.CheckedChange += visible_of_second;
        }

        private void visible_of_first(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (e.IsChecked)
            {
                txt2.Visibility = ViewStates.Visible;
                kombi2.Visibility = ViewStates.Visible;
            }
            else
            {
                txt2.Visibility = ViewStates.Gone;
                kombi2.Visibility = ViewStates.Gone;
            }
        }

        private void visible_of_second(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (e.IsChecked)
            {
                txt2.Visibility = ViewStates.Visible;
            }
            else
            {
                txt2.Visibility = ViewStates.Gone;
            }
        }

        private void oblicz_click (object sender, EventArgs e)
        {
            txt_wynik.Text = string.Format("wynik: {0}", txt1.Text);
        }
    }
}