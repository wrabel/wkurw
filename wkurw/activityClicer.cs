using System;
using System.Timers;
using Android.App;
using Android.OS;
using Android.Widget;
using Android.Views;

namespace wkurw
{

    [Activity(Label = "activityClicer", Theme = "@style/AppTheme")]
    public class activityClicer : Activity
    {
        private Button MyClicer,RES;
        private TextView txtCD;
        private int count = 10;
        private int clicks = 0;
        Timer timer;
        private bool isfirst = true;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.Title = " Fast Clicks ;] ";
            ActionBar.SetDisplayShowTitleEnabled(true);

            SetContentView(Resource.Layout.Clicer);

            MyClicer = FindViewById<Button>(Resource.Id.MyClicer);
            txtCD = FindViewById<TextView>(Resource.Id.txtCD);
            RES = FindViewById<Button>(Resource.Id.button_res);
            RES.Click += RES_Click;

            RES.Visibility = ViewStates.Gone;
            MyClicer.Click += MyClicer_Click;
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += OnTimedEvent;
        }

        private void RES_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MyClicer_Click(object sender, EventArgs e)
        {
            if (isfirst)
            {
                timer.Enabled = true;
                isfirst = false;
            }
            if (MyClicer.Enabled)
            {
                MyClicer.Text = string.Format("{0} clicks!", clicks++);
            }   
        }
        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            count--;
            RunOnUiThread(() =>
            {
                txtCD.Text = "" + count;
            });

            if (count == 0)
            {
                timer.Stop();
                RunOnUiThread(() =>
                {
                    MyClicer.Text = string.Format("Your score: {0}!", clicks);
                });
                MyClicer.Enabled = false;
            }
        }
    }
}