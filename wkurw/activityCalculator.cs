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

        private Spinner kombi1, kombi2,kombi3; //kombi 



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

            kombi1 = FindViewById<Spinner>(Resource.Id.txt_kombi_1);
            kombi2 = FindViewById<Spinner>(Resource.Id.txt_kombi_2);
            kombi3 = FindViewById<Spinner>(Resource.Id.txt_kombi_3);

            var kombi_1 = new string[]
            {
                "wybierz walute:",
                "PLN",
                "USD", //dolar
                "EUR", //eoru
                "JPY" //japonski jen :D
            };
            /*
                        var kombi_2 = new string[]
                        {
                            "wybierz walute:",
                            "PLN",
                            "USD", //dolar
                            "EUR", //eoru
                            "JPY" //japonski jen :D
                        };

                        var kombi_3 = new string[]
                        {
                            "wybierz walute:",
                            "PLN",
                            "USD", //dolar
                            "EUR", //eoru
                            "JPY" //japonski jen :D
                        };
            */
            kombi1.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, kombi_1);
            kombi2.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, kombi_1);
            kombi3.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, kombi_1);

            /*           kombi1.ItemSelected += kombi1_selected;
                       kombi2.ItemSelected += kombi1_selected;
                       kombi3.ItemSelected += kombi1_selected;
                       */
            pierwszyON = false;
            drugiON = false;
            buttom_ob.Click += oblicz_click;
            buttom_znak.Click += zmien_znak;
            znaczek = 0;

            kombi2.Visibility = ViewStates.Gone;  // kombi
            kombi3.Visibility = ViewStates.Visible; // kombi

            txt2.Visibility = ViewStates.Gone;
            buttom_znak.Visibility = ViewStates.Gone;

            switch1.CheckedChange += visible_of_first;
            switch2.CheckedChange += visible_of_second;

}
            private void cos()
        {

        }
        /*
                private void kombi3_selected(object sender, AdapterView.ItemSelectedEventArgs e)
                {
                    if (e.Parent.GetItemAtPosition(e.Position) != e.Parent.GetItemAtPosition(0))
                    {
                        Toast.MakeText(this, "wybrales: " + e.Parent.GetItemAtPosition(e.Position).ToString(), ToastLength.Short).Show();
                    }
                }

                private void kombi2_selected(object sender, AdapterView.ItemSelectedEventArgs e)
                {
                    if (e.Parent.GetItemAtPosition(e.Position) != e.Parent.GetItemAtPosition(0))
                    {
                        Toast.MakeText(this, "wybrales: " + e.Parent.GetItemAtPosition(e.Position).ToString(), ToastLength.Short).Show();
                    }
                }

                private void kombi1_selected(object sender, AdapterView.ItemSelectedEventArgs e)
                {

                    if (e.Parent.GetItemAtPosition(e.Position) != e.Parent.GetItemAtPosition(0))
                    {
                        Toast.MakeText(this, "wybrales: " + e.Parent.GetItemAtPosition(e.Position).ToString(), ToastLength.Short).Show(); 
                    }
                }
         */
        private void zmien_znak(object sender, EventArgs e)
        {   

            znaczek %= 2;
            if (pierwszyON)
            {   
                buttom_znak.Text = (znaczek == 0 ? "+" : "-");                
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
                
                kombi2.Visibility = ViewStates.Visible; //kombi
            }
            else
            {
                buttom_znak.Visibility = ViewStates.Gone;
                txt2.Visibility = ViewStates.Gone;
                txt2.Text = "";
                kombi2.Visibility = ViewStates.Gone; //kombi
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

                //kombi3.Visibility = ViewStates.Visible; //kombi
            }
            else
            {
                buttom_znak.Visibility = ViewStates.Gone;
                txt2.Visibility = ViewStates.Gone;
                txt2.Text = "";

                //kombi3.Visibility = ViewStates.Gone; // kombi
            }
        }

        private async void oblicz_click(object sender, EventArgs e)
        {
            if (txt1.Text != "")
            {
                if (kombi1.SelectedItemPosition != 0 && kombi3.SelectedItemPosition != 0)
                {
                    var exchangewebservice = new ExchangeWebService();
                    var rate_from_1 = (kombi1.SelectedItem.ToString() != kombi3.SelectedItem.ToString() ? await exchangewebservice.GetSingleRate
                        (kombi1.SelectedItem.ToString(),kombi3.SelectedItem.ToString()) : 1);
                    var kwota_z_1 = double.Parse(txt1.Text);
                    var exchange_z_1 = kwota_z_1 * rate_from_1;

                    

                    if (!pierwszyON && !drugiON)
                    {
                        if (kombi1.SelectedItemPosition == kombi3.SelectedItemPosition)
                        {
                            txt_wynik.Text = string.Format("wynik: {0}", txt1.Text);
                        }
                        else
                        {
                            txt_wynik.Text = string.Format("wynik: {0}", exchange_z_1);
                        }
                    }
                    if (pierwszyON)
                    {
                        if (txt2.Text == "")
                        {
                            Toast.MakeText(this, "podaj 2 nominal", ToastLength.Short).Show();
                        }
                        else
                        {
                            var exchangewebservice2 = new ExchangeWebService();
                            var rate_from_2 = (kombi2.SelectedItem.ToString() != kombi3.SelectedItem.ToString() ? await exchangewebservice.GetSingleRate
                        (
                        kombi2.SelectedItem.ToString(),
                        kombi3.SelectedItem.ToString()
                        ) : 1);
                            var kwota_z_2 = double.Parse(txt2.Text);
                            var exchane_z_2 = kwota_z_2 * rate_from_2;

                            switch (buttom_znak.Text)
                            {
                                case "+":
                                    txt_wynik.Text = string.Format("wynik: {0}", exchange_z_1 + exchane_z_2); break;
                                case "-":
                                    txt_wynik.Text = string.Format("wynik: {0}", exchange_z_1 - exchane_z_2); break;
                                default:
                                    txt_wynik.Text = "Ustal znak"; break;
                            }
                        }
                        
                    }
                    if (drugiON)
                    {
                        if (txt2.Text == "")
                        {
                            Toast.MakeText(this, "podaj liczbe", ToastLength.Short).Show();
                        }
                        else
                        {
                            switch (buttom_znak.Text)
                            {
                                case "x":
                                    txt_wynik.Text = string.Format("wynik: {0}", exchange_z_1 * double.Parse(txt2.Text)); break;
                                case "/":
                                    txt_wynik.Text = string.Format("wynik: {0}", exchange_z_1 / double.Parse(txt2.Text)); break;
                                default:
                                    txt_wynik.Text = "Ustal znak"; break;
                            }
                        }
                    }
                }
                else
                {
                    Toast.MakeText(this, "wybierz walute ", ToastLength.Short).Show();
                }
            }
            else
            {
                Toast.MakeText(this, "podaj kwote ", ToastLength.Short).Show();
            }
        }
    }
}