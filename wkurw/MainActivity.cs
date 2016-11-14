using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using Android.Content;
using Android.Views.InputMethods;
using Android.Util;
using System;

namespace wkurw
{
    [Activity(Label = "My Andro App", MainLauncher = true, Icon = "@drawable/icon",Theme ="@style/CustomTheme")]
    public class MainActivity : Activity
    {
        //deklaracja widoku
        private string left1 = "Hello Word", left2 = "Kalkulator";
        DrawerLayout mDrawerLayout;
        List<string> mLeftItems = new List<string>();
        ArrayAdapter mLeftAdapter;
        ListView mLeftDrawer;

        RelativeLayout mrelative;

        List<string> mRightItems = new List<string>();
        ArrayAdapter mRightAdapter;
        ListView mRightDrawer;

        ActionBarDrawerToggle mDrawerToggle;



        Button btnLogin;
        DataBase db;
        List<Person> lstSource = new List<Person>();
        EditText txt_nick, txt_email;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            mrelative = FindViewById<RelativeLayout>(Resource.Id.mainView);
            mrelative.Click += Mrelative_Click;

            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.Drawer);
            mRightDrawer = FindViewById<ListView>(Resource.Id.rightList);

            mRightItems.Add("RegEdit");
            mRightItems.Add("");
            mRightItems.Add("");
            mRightItems.Add("");
            mRightItems.Add("support us");
            mRightItems.Add("exit");

            mLeftDrawer = FindViewById<ListView>(Resource.Id.leftList);
            mLeftItems.Add(left1);
            mLeftItems.Add(left2);

            mLeftDrawer.Tag = 0;
            mRightDrawer.Tag = 1;

            mDrawerToggle = new MyActionBarDrawerToggle(this, mDrawerLayout, Resource.Drawable.ic_menu, Resource.String.open_drawer, Resource.String.close_drawer);

            mLeftAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, mLeftItems);
            mLeftDrawer.Adapter = mLeftAdapter;

            mRightAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, mRightItems);
            mRightDrawer.Adapter = mRightAdapter;

            mDrawerLayout.SetDrawerListener(mDrawerToggle);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayShowTitleEnabled(true);

            mLeftDrawer.ItemClick += mLeftDrawer_ItemClickShort;
            mLeftDrawer.ItemLongClick += mLeftDrawer_ItemClickLong;

            mRightDrawer.ItemClick += mRightDrawer_ItemClickShort;
            mRightDrawer.ItemLongClick += mRightDrawer_ItemClickLong;


            //Create DataBase
            db = new DataBase();
            db.createDataBase();
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            Log.Info("DB_PATH", folder);
            

            txt_nick = FindViewById<EditText>(Resource.Id.txtUserNick);
            txt_email = FindViewById<EditText>(Resource.Id.txtUserMail);
            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);


            btnLogin.Click += BtnLogin_Click;



        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            
                if (txt_nick.Text == "" && txt_email.Text == "")
                {
                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                AlertDialog alerDialog = builder.Create();
                alerDialog.SetTitle("Witaj !");
                alerDialog.SetIcon(Resource.Drawable.user_);
                alerDialog.SetMessage("Logujesz sie jako Gość");
                alerDialog.SetButton("OK", (s, ev) =>
                {
                    Intent intent2 = new Intent(this, typeof(loged));
                    this.StartActivity(intent2);
                    this.Finish();
                });
                alerDialog.SetButton2("anuluj", (s, ev) => { });
                alerDialog.Show();

              

                }
                else
                {
                Person person = new Person()
                {
                    Nick = txt_nick.Text,
                    Email = txt_email.Text,
                    data = string.Format("{0}/{1}/{2} {3}:{4}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.Hour, DateTime.Now.Minute)
                };
                db.insertIntoTablePerson(person);
                Intent intent = new Intent(this, typeof(loged));
                this.StartActivity(intent);
                this.Finish();
            }
            
        }

        private void Mrelative_Click(object sender, System.EventArgs e)
        {
            InputMethodManager inmutManager = (InputMethodManager)this.GetSystemService(Activity.InputMethodService);
            inmutManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.None);
        }

        protected void mRightDrawer_ItemClickLong(object sender, AdapterView.ItemLongClickEventArgs e)
        { 
            Toast.MakeText(this, "krotki opis: " + mRightItems[e.Position],ToastLength.Short).Show();
        }

        protected void mRightDrawer_ItemClickShort(object sender, AdapterView.ItemClickEventArgs e)
        {
            string iteam = mRightItems[e.Position];
            switch (iteam)
            {
                case "RegEdit":
                    Intent intentt = new Intent(this, typeof(activityRegEdit));
                    this.StartActivity(intentt); break;
                case "support us":
                    Finish(); break;
                case "exit":
                    Process.KillProcess(Process.MyPid()); break;
            }
        }

        protected void mLeftDrawer_ItemClickShort (object sender, AdapterView.ItemClickEventArgs e)
        {
           string iteam = mLeftItems[e.Position];
           switch (iteam)
            {
                case "Hello Word" :
                    Intent intent = new Intent(this, typeof(activityClicer));
                    this.StartActivity(intent); break;
                case "Kalkulator" :
                    Intent intent2 = new Intent(this, typeof(activityCalculator));
                    this.StartActivity(intent2);
                    this.Finish(); break;
            }
        }
        
        protected void mLeftDrawer_ItemClickLong (object sender, AdapterView.ItemLongClickEventArgs e)
        {
            Toast.MakeText(this, "krotki opis: " + mLeftItems[e.Position], ToastLength.Short).Show();
        }
        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            mDrawerToggle.SyncState();
        }

        public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            mDrawerToggle.OnConfigurationChanged(newConfig);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.action_bar, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (mDrawerToggle.OnOptionsItemSelected(item))
            {
                if (mDrawerLayout.IsDrawerOpen(mRightDrawer))
                {
                    mDrawerLayout.CloseDrawer(mRightDrawer);
                }
                return true;
            }

            switch (item.ItemId)
            {
                case Resource.Id.support:
                    if (mDrawerLayout.IsDrawerOpen(mRightDrawer))
                    {
                        mDrawerLayout.CloseDrawer(mRightDrawer);
                    }
                    else
                    {
                        mDrawerLayout.CloseDrawer(mLeftDrawer);
                        mDrawerLayout.OpenDrawer(mRightDrawer);
                    }
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }

            
        }
    }
}
