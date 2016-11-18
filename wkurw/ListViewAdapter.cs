using System.Collections.Generic;

using Android.App;
using Android.Views;
using Android.Widget;

namespace wkurw
{
    public class ViewHolder : Java.Lang.Object
    {
        public TextView txtName { get; set; }
        public TextView txtEmail { get; set; }
        public TextView txtdata { get; set; }
    }
    public class ListViewAdapter : BaseAdapter
    {
        private Activity activity;
        private List<Person> lstPerson;
        public ListViewAdapter(Activity activity, List<Person> lstPerson)
        {
            this.activity = activity;
            this.lstPerson = lstPerson;
        }

        public override int Count
        {
            get
            {
                return lstPerson.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return lstPerson[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.list_view_data, parent, false);

            var txtName = view.FindViewById<TextView>(Resource.Id.textView1);
            var txtEmail = view.FindViewById<TextView>(Resource.Id.textView2);
            var txtdata = view.FindViewById<TextView>(Resource.Id.textView3);

            txtName.Text = lstPerson[position].Nick;
            txtEmail.Text = lstPerson[position].Email;
            txtdata.Text = lstPerson[position].data;

            return view;
        }
    }
}