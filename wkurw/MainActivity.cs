using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Support.V4.Widget;
using Android.Support.V4.App;

namespace wkurw
{
    [Activity(Label = "App1", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        DrawerLayout mDrawerLayout;
        List<string> mLeftItems = new List<string>();
        ArrayAdapter mLeftAdapter;
        ListView mLeftDrawer;
        ActionBarDrawerToggle mDrawerToggle;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.Drawer);
            mLeftDrawer = FindViewById<ListView>(Resource.Id.leftList);

            mLeftItems.Add("Hello Word");
            mLeftItems.Add("Kalkulator");

            mDrawerToggle = new ActionBarDrawerToggle(this, mDrawerLayout, Resource.Drawable.ic_menu, Resource.String.open_drawer, Resource.String.close_drawer);

            mLeftAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, mLeftItems);
            mLeftDrawer.Adapter = mLeftAdapter;

            mDrawerLayout.SetDrawerListener(mDrawerToggle);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);         
        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            mDrawerToggle.SyncState();
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (mDrawerToggle.OnOptionsItemSelected(item))
                return true;
            return base.OnOptionsItemSelected(item);
        }
    }
}

