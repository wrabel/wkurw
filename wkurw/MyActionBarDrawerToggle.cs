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
using Android.Support.V4.App;
using Android.Support.V4.Widget;

namespace wkurw
{
    class MyActionBarDrawerToggle : ActionBarDrawerToggle
    {
        Activity mActivity;

        public MyActionBarDrawerToggle (Activity activity, DrawerLayout drawerLayout, int imageResorce, int openDrawerDesc, int closeDrawerDesc)
            : base (activity, drawerLayout, imageResorce,openDrawerDesc,closeDrawerDesc)
        {
            mActivity = activity;
        }

        public override void OnDrawerOpened(Android.Views.View drawerView)
        {
            base.OnDrawerOpened(drawerView);
            mActivity.ActionBar.Title = "Czego chcesz? ";
        }

        public override void OnDrawerClosed(Android.Views.View drawerView)
        {
            base.OnDrawerClosed(drawerView);
            mActivity.ActionBar.Title = "My Andro App";
        }

        public override void OnDrawerSlide(Android.Views.View drawerView, float slideOffset)
        {
            base.OnDrawerSlide(drawerView, slideOffset);
        }
    }
}