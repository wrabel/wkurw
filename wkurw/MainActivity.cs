using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace wkurw
{
    [Activity(Label = "My Andro App", MainLauncher = true, Icon = "@drawable/icon",Theme ="@style/CustomTheme")]
    public class MainActivity : Activity
    {

        DrawerLayout mDrawerLayout;
        List<string> mLeftItems = new List<string>();
        ArrayAdapter mLeftAdapter;
        ListView mLeftDrawer;

        List<string> mRightItems = new List<string>();
        ArrayAdapter mRightAdapter;
        ListView mRightDrawer;

        ActionBarDrawerToggle mDrawerToggle;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.Drawer);
            mRightDrawer = FindViewById<ListView>(Resource.Id.rightList);

            mRightItems.Add("support us");
            mRightItems.Add("exit");

            mLeftDrawer = FindViewById<ListView>(Resource.Id.leftList);
            mLeftItems.Add("Hello Word");
            mLeftItems.Add("Kalkulator");

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

