using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.Res;
using Android.Support.V4.App;

namespace wkurw
{
    [Activity(Label = "activityClicer")]
    public class activityClicer : Activity
    {
        private Button MyClicer;
        private TextView txtCD;
        private int count = 10;
        private int clicks = 0;
        Timer timer;
        private bool isfirst = true;
        private bool blocked = false;
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.Title = " Fast Clicks ;] ";
            ActionBar.SetDisplayShowTitleEnabled(true);

            SetContentView(Resource.Layout.Clicer);

            MyClicer = FindViewById<Button>(Resource.Id.MyClicer);
            txtCD = FindViewById<TextView>(Resource.Id.txtCD);
            MyClicer.Click += MyClicer_Click;
        }

        private void MyClicer_Click(object sender, EventArgs e)
        {
            if (isfirst)
            {
                timer.Start();
                isfirst = false;
            }
            if (blocked == false) MyClicer.Text = string.Format("{0} clicks!", clicks++);
            if (blocked) MyClicer.Text = string.Format("Your score: {0} clicks!", clicks);
        }

        protected override void OnResume()
        {
            base.OnResume();
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (count > 0 )
            {
                count--;
                RunOnUiThread(() =>
                {
                    txtCD.Text = "" + count;
                });
            }
            else
            {
                count = 0;
                txtCD.Text = "" + count;
                timer.Stop();
                blocked = true;
            }
        }
    }
}