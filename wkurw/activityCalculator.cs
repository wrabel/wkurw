using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Views.InputMethods;

namespace wkurw
{
    [Activity(Label = "activityCalculator")]
    public class activityCalculator : Activity
    {
        //wstepna deklaracaj widoku
        RelativeLayout mrelativ;
        private Button buttom_ob,buttom_znak;
        private TextView txt_wynik;
        private Switch switch1,switch2; 
        private EditText txt1, txt2;
        bool pierwszyON, drugiON ;
        private int znaczek;

        private Spinner kombi1, kombi2,kombi3;

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
            txt1 = FindViewById<EditText>(Resource.Id.txt_waluta_1);
            txt2 = FindViewById<EditText>(Resource.Id.txt_waluta_2);

            kombi1 = FindViewById<Spinner>(Resource.Id.txt_kombi_1);
            kombi2 = FindViewById<Spinner>(Resource.Id.txt_kombi_2);
            kombi3 = FindViewById<Spinner>(Resource.Id.txt_kombi_3);

            //towrzenie tablicy stringow wykorzystawane w polach kobi (spinner)
            var kombi_1 = new string[walut_lista.Items.Count+1];
            kombi_1[0] = "wybierz walute:";
            int i = 1;
            foreach( var iteam in walut_lista.Items)
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
            buttom_znak.Click += zmien_znak;
            //przypisanie swithcom funkcji
            switch1.CheckedChange += visible_of_first;
            switch2.CheckedChange += visible_of_second;


}

        private void Mrelativ_Click(object sender, EventArgs e)
        {
            InputMethodManager inmutManager = (InputMethodManager)this.GetSystemService(Activity.InputMethodService);
            inmutManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.None);
        }


        //instrukcja dzialan funkcji "wybor z listy kombi" -> wyswitla pelna angielska nazwe danej waluty
        private void kombi_selected(object sender, AdapterView.ItemSelectedEventArgs e)
                {
                    if (e.Parent.GetItemAtPosition(e.Position) != e.Parent.GetItemAtPosition(0))
                    {
                        var wybor = walut_lista.Items[e.Position - 1];
                        Toast.MakeText(this, "wybrales: " + wybor.e_nazwa, ToastLength.Short).Show();
                    }
                }

        private void zmien_znak(object sender, EventArgs e) // zmienianie znaku po nacisnieciu buttom_znak
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

        private void visible_of_first(object sender, CompoundButton.CheckedChangeEventArgs e) // nastepstwa po wlaczeniu switcha1
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

        private void visible_of_second(object sender, CompoundButton.CheckedChangeEventArgs e) // nastepstwa po wlaczeniu switcha2
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

        private void oblicz_click(object sender, EventArgs e) // cala funkcjia liczaca po kliknieciu (tutaj sa problemy)
        {
            if (txt1.Text != "") // -> sprawdza czy pierwsze pole nie jest puste, jesli jest kaze podac kwote do zamiany
            {
                if (kombi1.SelectedItemPosition != 0 && kombi3.SelectedItemPosition != 0)  // -> sprawdza czy w polu kombi1 (wejsciowym) i kombi3(wyjsciowym) sa wybrane waluty, jesli nie kaze wybrac walute
                {
                    var CurrencyDara = new FreeCurrencyServise();
                    var rate_from_1 = CurrencyDara.GetDataFromService(kombi1.SelectedItem.ToString(), kombi3.SelectedItem.ToString());
                    //var rate_from_1 = await NewExchange.getDataFromWeb(kombi1.SelectedItem.ToString(), kombi3.SelectedItem.ToString());
                    //podstawowe wyliczenia / bazowa operacja kalkulatora walut
                    //var exchangewebservice = new ExchangeWebService();  // i gdzies tu w ponizszych dwoch linijkach jest blad ;/
                    //var rate_from_1 = (kombi1.SelectedItem.ToString() != kombi3.SelectedItem.ToString() ? await exchangewebservice.GetSingleRate // jezeli wybrane waluty roznia sie wysyla zapytanie do api i zwraca otrzymany przelicznik
                    //    (kombi1.SelectedItem.ToString(),kombi3.SelectedItem.ToString()) : 1);  // natomiast jesli sa takie same waluty , by zaoszczedzic na czasie oczekiwania, zwraca 1 (bez zapytan do api) 
                    var kwota_z_1 = double.Parse(txt1.Text); // nie jest potrzebne ale ladniej wyglada -> przypisanie zmiennej wartosci z pola z pierwsza kwota (wejsciowa)
                    var exchange_z_1 = kwota_z_1 * rate_from_1; // wyliczanie wyniku -> kwota * przelicznik
                    

                    if (!pierwszyON && !drugiON) // sprawdza czy oba switche sa wylaczone 
                    {
                            txt_wynik.Text = string.Format("wynik: {0}", exchange_z_1); // wypisuje wynik z operacji w.w.
                    }
                    if (pierwszyON) // podczas gdy switch1 jest wlaczony   
                    {
                        if (txt2.Text == "") //sprawdza czy wpisalismy 2 nominal do przeliczen
                        {
                            Toast.MakeText(this, "podaj 2 nominal", ToastLength.Short).Show();
                        }
                        else // analogiczna bazowa operacja kalkulatora dla pola wartosci i kombi2 (wartosci wejsciowych 2) i waluty wyjsicowej
                        {
                            var rate_from_2 = CurrencyDara.GetDataFromService(kombi2.SelectedItem.ToString(), kombi3.SelectedItem.ToString());
                            //var rate_from_2 = await NewExchange.getDataFromWeb(kombi2.SelectedItem.ToString(), kombi3.SelectedItem.ToString());
                            //    var rate_from_2 = (kombi2.SelectedItem.ToString() != kombi3.SelectedItem.ToString() ? await exchangewebservice.GetSingleRate
                            //(
                            //kombi2.SelectedItem.ToString(),
                            //kombi3.SelectedItem.ToString()
                            //) : 1);
                            var kwota_z_2 = double.Parse(txt2.Text);
                            var exchane_z_2 = kwota_z_2 * rate_from_2;

                            switch (buttom_znak.Text) // wypisanie wyniku odpowiednich dzialan w zaleznosci od wybranego znaku na przycisku
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
                    if (drugiON) // opsi prostego wylicznie gdy drugi switch jest wlaczony
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