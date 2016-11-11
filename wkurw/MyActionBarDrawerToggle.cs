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
        //otwieranie menu
        public override void OnDrawerOpened(View drawerView)
        {
            int drawerType = (int)drawerView.Tag;
            if (drawerType == 0)
            {   //Left Menu
                base.OnDrawerOpened(drawerView);
                mActivity.ActionBar.Title = "Czego chcesz? ";

            }
        }
        //zamykanie menu
        public override void OnDrawerClosed(View drawerView)
        {
            int drawerType = (int)drawerView.Tag;
            if (drawerType == 0)
            {   //Left Menu
                base.OnDrawerClosed(drawerView);
                mActivity.ActionBar.Title = "My Andro App";
            }
            
        }
        //zammienosc menus
        public override void OnDrawerSlide(View drawerView, float slideOffset)
        {
            int drawerType = (int)drawerView.Tag;
            if (drawerType == 0)
            {   //Left Menu
                base.OnDrawerSlide(drawerView, slideOffset);
            }
        }
    }
}