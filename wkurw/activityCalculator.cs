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
        private Button buttom_ob,buttom_znak;
        private TextView txt_wynik;
        private Switch switch1,switch2; 
        private EditText txt1, txt2;
        bool pierwszyON, drugiON ;
        private int znaczek;

        private TextView kombi1, kombi2,kombi3; // to beda pola kombi ale tymczasowo zwykly text

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.calculator);

            ActionBar.Title = " Oblicze za ciebie ;] ";
            ActionBar.SetDisplayShowTitleEnabled(true);

            buttom_ob = FindViewById<Button>(Resource.Id.button_oblicz);
            buttom_znak = FindViewById<Button>(Resource.Id.button_znak);
            txt_wynik = FindViewById<TextView>(Resource.Id.txt_wynik);
            switch2 = FindViewById<Switch>(Resource.Id.switch_dziel_mnoz);
            switch1 = FindViewById<Switch>(Resource.Id.switch_dodac_odjac);
            txt1 = FindViewById<EditText>(Resource.Id.txt_waluta_1);
            txt2 = FindViewById<EditText>(Resource.Id.txt_waluta_2);

            kombi1 = FindViewById<TextView>(Resource.Id.txt_kombi_1);
            kombi2 = FindViewById<TextView>(Resource.Id.txt_kombi_2);
            kombi3 = FindViewById<TextView>(Resource.Id.txt_kombi_3);

            pierwszyON = false;
            drugiON = false;
            buttom_ob.Click += oblicz_click;
            buttom_znak.Click += zmien_znak;
            znaczek = 0;

            kombi2.Visibility = ViewStates.Gone; //bedzie kombi
            kombi3.Visibility = ViewStates.Visible; //bedzie kombi

            txt2.Visibility = ViewStates.Gone;
            buttom_znak.Visibility = ViewStates.Gone;

            switch1.CheckedChange += visible_of_first;
            switch2.CheckedChange += visible_of_second;

            

        }

        private void zmien_znak(object sender, EventArgs e)
        {
            znaczek %= 2;
            if (pierwszyON)
            {
                buttom_znak.Text = (znaczek==0 ? "+" : "-") ;
            }
            if (drugiON)
            {
                buttom_znak.Text = (znaczek == 0 ? "x" : "/");
            }
            znaczek++;
        }

        private void visible_of_first(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (drugiON)
            {
                switch2.Checked = false;
            }
            pierwszyON = e.IsChecked;
            if (e.IsChecked)
            {
                buttom_znak.Text = "znak";
                buttom_znak.Visibility = ViewStates.Visible;
                txt2.Visibility = ViewStates.Visible;
                
                kombi2.Visibility = ViewStates.Visible; //do zmiany na kombi
            }
            else
            {
                buttom_znak.Visibility = ViewStates.Gone;
                txt2.Visibility = ViewStates.Gone;
                kombi2.Visibility = ViewStates.Gone; //do zmainy na kombi
            }
        }

        private void visible_of_second(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (pierwszyON)
            {
                switch1.Checked = false;
            }
            drugiON = e.IsChecked;
            if (e.IsChecked)
            {
                buttom_znak.Text = "znak";
                buttom_znak.Visibility = ViewStates.Visible;
                txt2.Visibility = ViewStates.Visible;

                kombi3.Visibility = ViewStates.Gone; // kombi
            }
            else
            {
                buttom_znak.Visibility = ViewStates.Gone;
                txt2.Visibility = ViewStates.Gone;

                kombi3.Visibility = ViewStates.Visible; // kombi
            }
        }

        private void oblicz_click (object sender, EventArgs e)
        {
            if (!pierwszyON && !drugiON)
            {
                txt_wynik.Text = string.Format("wynik: {0}", txt1.Text);
            }
            if (pierwszyON)
            {
                switch (buttom_znak.Text)
                {
                    case "+" :
                        txt_wynik.Text = string.Format("wynik: {0}", double.Parse(txt1.Text) + double.Parse(txt2.Text)); break;
                    case "-" :
                        txt_wynik.Text = string.Format("wynik: {0}", double.Parse(txt1.Text) - double.Parse(txt2.Text)); break;
                    default:
                        txt_wynik.Text = "Ustal znak"; break;
                }
            }
            if (drugiON)
            {
                switch (buttom_znak.Text)
                {
                    case "x":
                        txt_wynik.Text = string.Format("wynik: {0}", double.Parse(txt1.Text) * double.Parse(txt2.Text)); break;
                    case "/":
                        txt_wynik.Text = string.Format("wynik: {0}", double.Parse(txt1.Text) / double.Parse(txt2.Text)); break;
                    default:
                        txt_wynik.Text = "Ustal znak"; break;
                }

            }
            
        }
    }
}