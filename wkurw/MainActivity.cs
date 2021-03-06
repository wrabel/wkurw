﻿using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Content;
using Android.Views.InputMethods;
using System;
using Android.Net.Wifi;
using System.Threading.Tasks;
using Plugin.Connectivity;

namespace wkurw // Wielofunkcjyjny Kalkulator Uwzgledniajacy Realna Walute
{
    [Activity(Label = "Kalkulator walut", MainLauncher = true, Icon = "@drawable/icon",Theme ="@style/CustomTheme")]
    public class MainActivity : Activity, IKalku, IOnline
    { 
        //do odpowiednich funkji dodany lektor zalezny od bycia online i swichu
        #region (widok raczki itp)

        //wstepna deklaracaj widoku
        private RelativeLayout mrelativ;
        private Button buttom_ob, buttom_znak;
        private TextView txt_wynik;
        private Switch switch1, switch2,lang_switch;
        private EditText txt1, txt2;
        private bool pierwszyON, drugiON,lektorON;
        private int znaczek;

        private Spinner kombi1, kombi2, kombi3;

        //internet
        
        private WifiManager wifiManager;
        private bool isOnline;
        private ImageView _net_view;
        private Online online = new Online();
        private TexTtoSpeecH lektor = new TexTtoSpeecH();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //deklaracja widoku

            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.calculator);


            ActionBar.Title = " Oblicze za ciebie ;] ";
            ActionBar.SetDisplayShowTitleEnabled(true);

            mrelativ = FindViewById<RelativeLayout>(Resource.Id.m_View);
            mrelativ.Click += Mrelativ_Click;

            buttom_ob = FindViewById<Button>(Resource.Id.button_oblicz);
            buttom_znak = FindViewById<Button>(Resource.Id.button_znak);
            txt_wynik = FindViewById<TextView>(Resource.Id.txt_wynik);
            switch2 = FindViewById<Switch>(Resource.Id.switch_dziel_mnoz);
            switch1 = FindViewById<Switch>(Resource.Id.switch_dodac_odjac);
            lang_switch = FindViewById<Switch>(Resource.Id.switch_lektor);
            txt1 = FindViewById<EditText>(Resource.Id.txt_waluta_1);
            txt2 = FindViewById<EditText>(Resource.Id.txt_waluta_2);

            kombi1 = FindViewById<Spinner>(Resource.Id.txt_kombi_1);
            kombi2 = FindViewById<Spinner>(Resource.Id.txt_kombi_2);
            kombi3 = FindViewById<Spinner>(Resource.Id.txt_kombi_3);

            _net_view = FindViewById<ImageView>(Resource.Id.imageView1);


            txt1.TextChanged += TextChanged;
            txt2.TextChanged += TextChanged;
            txt1.LongClick += Txt1_LongClick;
            txt2.LongClick += Txt2_LongClick;

            //towrzenie tablicy stringow wykorzystawane w polach kobi (spinner)
            var kombi_1 = new string[walut_lista.Items.Count + 1];
            kombi_1[0] = "wybierz walute:";
            int i = 1;
            foreach (var iteam in walut_lista.Items)
            {
                kombi_1[i++] = iteam.SC;
            }

            //dodanie itemsow do spinnerow na podstawie tablicy strongow
            kombi1.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, kombi_1);
            kombi2.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, kombi_1);
            kombi3.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, kombi_1);

            //nadanie funkcji "po wyborze z listy kombi"
            kombi1.ItemSelected += kombi_selected;
            kombi2.ItemSelected += kombi_selected;
            kombi3.ItemSelected += kombi_selected;

            //poczatkowe ustawienia widocznosci na stronie 
            pierwszyON = false;
            drugiON = false;
            znaczek = 0;

            kombi2.Visibility = ViewStates.Gone;  // kombi
            kombi3.Visibility = ViewStates.Visible; // kombi

            txt2.Visibility = ViewStates.Gone;
            buttom_znak.Visibility = ViewStates.Gone;

            //przypisanie buttomom funkcji
            buttom_ob.Click += oblicz_click;
            buttom_ob.LongClick += Buttom_ob_LongClick;
            buttom_znak.Click += zmien_znak;

            //przypisanie swithcom funkcji
            switch1.CheckedChange += visible_of_first;
            switch2.CheckedChange += visible_of_second;
            lang_switch.CheckedChange += Lang_switch_CheckedChange;

            //wywolanie sprawdzania internetu
            ChceckNetwork();
            CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged; 
        }

        #endregion

        #region (lektor)
        // czytanie przy zmianie wprowadzanego tekstu / long kliku
        private void Txt2_LongClick(object sender, View.LongClickEventArgs e)
        {
            if (lektorON) lektor.say_(txt2.Text.ToString());
        }

        private void Txt1_LongClick(object sender, View.LongClickEventArgs e)
        {
            if (lektorON) lektor.say_(txt1.Text.ToString());
        }

        private void TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (lektorON) lektor.say_(e.Text.ToString());
        }

        public void Lang_switch_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        { //swich od wlacznika lektora
            if (isOnline) lektorON = lang_switch.Checked;
            else
            {
                lang_switch.Checked = false;
                Toast.MakeText(this, "Lektor potrzebuje internetu", ToastLength.Short).Show();
            }

        }
        #endregion

        #region (funkcje internetowe)
        private void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            // rasia sluzaca do zmiany obrazka kiedy zmieniamy polaczenie z internetem
            ZmienKolor();
        }
        
        public void ZmienKolor() // zmienianie koloru przycisku w zaleznosci od obecnego ONLINE
        {
            isOnline = online.is_Online();
            if (isOnline) _net_view.SetImageResource(Resource.Drawable.kwadrat_zielony);
            else
            {
                _net_view.SetImageResource(Resource.Drawable.kwadrat_czerwony);
                lang_switch.Checked = false;
            }
        }

        public async void ChceckNetwork() // sprawdzanie mozliwosci ONLINE i ew wejscie w ustawienia
        {
            await Task.Delay(400);
            ZmienKolor();
            if (!isOnline)
                {
                    AlertDialog.Builder build = new AlertDialog.Builder(this);
                    AlertDialog alerNet = build.Create();
                    alerNet.SetCancelable(false);
                    alerNet.SetTitle("Warning!");
                    alerNet.SetMessage("Internet jest wymagany");
                    alerNet.SetButton("Włacz wifi", (s, ev) =>
                    {
                        wifiManager = (WifiManager)GetSystemService(WifiService);
                        wifiManager.SetWifiEnabled(true);
                    });
                    alerNet.SetButton2("Włacz dane komorkowe", (s, ev) =>
                    {
                        var intent = new Intent(Android.Provider.Settings.ActionSettings);
                        StartActivity(intent);

                    });
                    alerNet.Show();
                } 
        }
        #endregion

        #region (wylaczenie focusu na oknie (wylaczenie metod wprowadzania/klawiatur))
        private void Mrelativ_Click(object sender, EventArgs e) // "dotkniecie" elementow jak i samego layouta
        {
            InputMethodManager inmutManager = (InputMethodManager)this.GetSystemService(Activity.InputMethodService);
            inmutManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.None);
        }
        #endregion

        #region (wybor kombi)
        //instrukcja dzialan funkcji "wybor z listy kombi" -> wyswitla pelna angielska nazwe danej waluty
        private void kombi_selected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (e.Parent.GetItemAtPosition(e.Position) != e.Parent.GetItemAtPosition(0))
            {
                var wybor = walut_lista.Items[e.Position - 1];
                Toast.MakeText(this, "wybraleś: " + wybor.e_nazwa, ToastLength.Short).Show();

                if (lektorON) lektor.say_("wybraleś: " + wybor.e_nazwa);
                
            }
        }
        #endregion

        #region (metody interfejsowe)
        public void zmien_znak(object sender, EventArgs e) // zmienianie znaku po nacisnieciu buttom_znak
        {

            znaczek %= 2;
            if (pierwszyON)
            {
                buttom_znak.Text = (znaczek == 0 ? "+" : "-");
                if (lektorON)
                {
                    if (znaczek == 0) lektor.say_("dodawanie");
                    else lektor.say_("odejmowanie");
                }
            }
            if (drugiON)
            {
                buttom_znak.Text = (znaczek == 0 ? "x" : "/");
                if (lektorON)
                {
                    if (znaczek == 0) lektor.say_("mnożenie");
                    else lektor.say_("dzielenie");
                }
            }
            znaczek++;

        }

        public void visible_of_first(object sender, CompoundButton.CheckedChangeEventArgs e) // nastepstwa po wlaczeniu switcha1
        {
            if (drugiON)
            {
                switch2.Checked = false; // -> jesli drugi tez jest wlaczony wylacza go
            }
            pierwszyON = e.IsChecked;
            if (e.IsChecked)
            {

                buttom_znak.Text = "znak"; // "zeruje" znak 
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

        public void visible_of_second(object sender, CompoundButton.CheckedChangeEventArgs e) // nastepstwa po wlaczeniu switcha2
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
                
            }
            else
            {
                buttom_znak.Visibility = ViewStates.Gone;
                txt2.Visibility = ViewStates.Gone;
                txt2.Text = "";
                
            }
        }
        #endregion

        #region (klikacze)
        private void Buttom_ob_LongClick(object sender, View.LongClickEventArgs e) // dlugie przycisniecie guzika oblicz 
        { // sprawdzanie online wyrzucone do innej metody i zamiast niego wszystko robione pod try 
            try
            {
                if (kombi1.SelectedItemPosition == 0 || kombi3.SelectedItemPosition == 0)
                {
                    Toast.MakeText(this, "wybierz walute", ToastLength.Short).Show();
                    if (lektorON) lektor.say_("wybierz walute");
                    return;
                }

                if (pierwszyON || drugiON)
                {
                    return;
                }

                var CurrencyDara = new FreeCurrencyServise();
                var rates = CurrencyDara.GetDataFromService(kombi1.SelectedItem.ToString(), kombi3.SelectedItem.ToString());

                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                AlertDialog alerDialog = builder.Create();
                alerDialog.SetCanceledOnTouchOutside(false);
                alerDialog.SetTitle(string.Format("Przelicznik: {0} : {1}", kombi1.SelectedItem.ToString(), kombi3.SelectedItem.ToString()));
                alerDialog.SetMessage(string.Format(" < {0} > ", rates));
                alerDialog.SetButton("OK", (s, ev) => { });
                alerDialog.Show();
                if (lektorON) lektor.say_("Przelicznik wynosi " + rates);
            }
            catch // wiadomosc o braku polaczenia z internetem
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                AlertDialog alerDialog = builder.Create();
                alerDialog.SetTitle("Warning!");
                alerDialog.SetCanceledOnTouchOutside(false);
                alerDialog.SetMessage("Sprawdz polaczenie z internetem");
                alerDialog.SetButton("OK", (s, ev) => { });
                alerDialog.Show();
                ZmienKolor();
            }
        }

        private void oblicz_click(object sender, EventArgs e) // cala funkcjia liczaca po kliknieciu 
        {   // sprawdzanie online wyrzucone do innej metody i zamiast niego wszystko robione pod try 
            try
            {
                if (txt1.Text == "")
                {
                    Toast.MakeText(this, "podaj kwote", ToastLength.Short).Show();
                    if (lektorON) lektor.say_("Podaj kwotę");
                    return;
                }

                var pierwszy_txt = txt1.Text.Replace(".", ",");
                if (pierwszy_txt.StartsWith(","))
                {
                    pierwszy_txt = "0" + pierwszy_txt;
                }

                if (kombi1.SelectedItemPosition == 0 || kombi3.SelectedItemPosition == 0)
                {
                    Toast.MakeText(this, "wybierz walute", ToastLength.Short).Show();
                    if (lektorON) lektor.say_("wybierz walutę");
                    return;
                }

                var CurrencyDara = new FreeCurrencyServise();
                var rate_from_1 = CurrencyDara.GetDataFromService(kombi1.SelectedItem.ToString(), kombi3.SelectedItem.ToString());
                var kwota_z_1 = double.Parse(pierwszy_txt);
                var exchange_z_1 = kwota_z_1 * rate_from_1;

                if (!pierwszyON && !drugiON)
                {
                    txt_wynik.Text = string.Format("wynik: {0:N2}", exchange_z_1);
                    if (lektorON) lektor.say_(string.Format("wynik wynosi {0:N2}", exchange_z_1));
                    return;
                }

                if (pierwszyON)
                {
                    if (txt2.Text == "")
                    {
                        Toast.MakeText(this, "podaj 2 nominal", ToastLength.Short).Show();
                        if (lektorON) lektor.say_("podaj drugi nominał");
                        return;
                    }

                    var drugi_txt = txt2.Text.Replace(".", ",");
                    if (drugi_txt.StartsWith(","))
                    {
                        drugi_txt = "0" + drugi_txt;
                    }

                    if (kombi2.SelectedItemPosition == 0)
                    {
                        Toast.MakeText(this, "wybierz 2 walute", ToastLength.Short).Show();
                        if (lektorON) lektor.say_("wybierz drugą walutę");
                        return;
                    }

                    if (buttom_znak.Text == "znak")
                    {
                        Toast.MakeText(this, "ustal znak", ToastLength.Short).Show();
                        if (lektorON) lektor.say_("ustal znak");
                        return;
                    }
        
                    var rate_from_2 = CurrencyDara.GetDataFromService(kombi2.SelectedItem.ToString(), kombi3.SelectedItem.ToString());
                    var kwota_z_2 = double.Parse(drugi_txt);
                    var exchane_z_2 = kwota_z_2 * rate_from_2;

                    switch (buttom_znak.Text)
                    {
                        case "+":
                            txt_wynik.Text = string.Format("wynik: {0:N2}", exchange_z_1 + exchane_z_2);
                            if (lektorON) lektor.say_(string.Format("wynik wynosi {0:N2}", exchange_z_1 + exchane_z_2));
                            break;
                        case "-":
                            txt_wynik.Text = string.Format("wynik: {0:N2}", exchange_z_1 - exchane_z_2);
                            if (lektorON) lektor.say_(string.Format("wynik wynosi {0:N2}", exchange_z_1 - exchane_z_2));
                            break;
                    }
                    return;
                }

                if (drugiON)
                {
                    if (txt2.Text == "")
                    {
                        Toast.MakeText(this, "podaj liczbe", ToastLength.Short).Show();
                        if(lektorON) lektor.say_("podaj liczbę");
                        return;
                    }

                    if (buttom_znak.Text == "znak")
                    {
                        Toast.MakeText(this, "ustal znak", ToastLength.Short).Show();
                        if (lektorON) lektor.say_("ustal znak");
                        return;
                    }

                    switch (buttom_znak.Text)
                    {
                        case "x":
                            txt_wynik.Text = string.Format("wynik: {0:N2}", exchange_z_1 * double.Parse(txt2.Text));
                            if (lektorON) lektor.say_(string.Format("wynik wynosi {0:N2}", exchange_z_1 * double.Parse(txt2.Text)));
                            break;
                        case "/":
                            txt_wynik.Text = string.Format("wynik: {0:N2}", exchange_z_1 / double.Parse(txt2.Text));
                            if (lektorON) lektor.say_(string.Format("wynik wynosi {0:N2}", exchange_z_1 / double.Parse(txt2.Text)));
                            break;
                    }
                    return;
                }
            }
            catch
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                AlertDialog alerDialog = builder.Create();
                alerDialog.SetTitle("Warning!");
                alerDialog.SetCanceledOnTouchOutside(false);
                alerDialog.SetMessage("Sprawdz polaczenie z internetem");
                alerDialog.SetButton("OK", (s, ev) => { });
                alerDialog.Show();
                ZmienKolor();
            }
            
        }
        #endregion

    }
}