using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Widget;

// tu bedzie widok po zalogowaniu jeszcze brak pomyslow
namespace wkurw
{
    [Activity(Label = "loged", Theme = "@style/AppTheme")]
    public class loged : Activity
    {
        ListView lstData;
        List<Person> lstSource = new List<Person>();
        DataBase db;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            SetContentView(Resource.Layout.after_log);

            db = new DataBase();
            db.createDataBase();

            lstData = FindViewById<ListView>(Resource.Id.listView);

            var txtName =FindViewById<TextView>(Resource.Id.textView1);
            var txtEmail = FindViewById<TextView>(Resource.Id.textView2);

            LoadData();

        }
        private void LoadData()
        {
            lstSource = db.selectTablePerson();
            var adapter = new ListViewAdapter(this, lstSource);
            lstData.Adapter = adapter;
        }
    }
}
