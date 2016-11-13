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
using Android.Util;

namespace wkurw
{
    [Activity(Label = "activityRegEdit")]
    public class activityRegEdit : Activity
    {
        ListView lstData2;
        List<Person> lstSource = new List<Person>();
        DataBase db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.reg_editable);
            
            db = new DataBase();
            db.createDataBase();
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            Log.Info("DB_PATH", folder);

            lstData2 = FindViewById<ListView>(Resource.Id.e_listView);

            var edtName = FindViewById<EditText>(Resource.Id.e_Name);
            var edtEmail = FindViewById<EditText>(Resource.Id.e_Email);
            
            var btnEdit = FindViewById<Button>(Resource.Id.btnEdit);
            var btnDelete = FindViewById<Button>(Resource.Id.btnDelete);

            int poprzedni = 0;
            bool first=true;
            
            LoadData();
            

            btnEdit.Click += delegate {

                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                AlertDialog alerDialog = builder.Create();
                alerDialog.SetTitle("WARNING !");
                alerDialog.SetMessage("Zedytowac zawartosc?");
                alerDialog.SetButton("Tak", (s, ev) =>
                {
                    Person person = new Person()
                    {
                        Id = int.Parse(edtName.Tag.ToString()),
                        Nick = edtName.Text,
                        Email = edtEmail.Text
                    };
                    db.updateTablePerson(person);
                    LoadData();
                });
                alerDialog.SetButton2("Anuluj", (s, ev) => { });
                alerDialog.Show();
                
            };

            btnDelete.Click += delegate {

                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                AlertDialog alerDialog = builder.Create();
                alerDialog.SetTitle("WARNING !");
                alerDialog.SetMessage("Usunac zawartosc?");
                alerDialog.SetButton("Tak", (s, ev) =>
                {
                    Person person = new Person()
                    {
                        Id = int.Parse(edtName.Tag.ToString()),
                        Nick = edtName.Text,
                        Email = edtEmail.Text
                    };
                    db.deleteTablePerson(person);
                    LoadData();

                    edtName.Text = "";
                    edtEmail.Text = "";
                });
                alerDialog.SetButton2("Anuluj", (s, ev) => { });
                alerDialog.Show();               
               
            };

            lstData2.ItemClick += (s, e) => {
                var txtName = e.View.FindViewById<TextView>(Resource.Id.textView1);
                var txtEmail = e.View.FindViewById<TextView>(Resource.Id.textView2);
                if (first)
                {
                    lstData2.GetChildAt(e.Position).SetBackgroundColor(Android.Graphics.Color.DarkGray);
                    first = false;
                    poprzedni = e.Position;

                    edtName.Text = txtName.Text;
                    edtName.Tag = e.Id;
                    edtEmail.Text = txtEmail.Text;
                }
                else
                {
                    lstData2.GetChildAt(poprzedni).SetBackgroundColor(Android.Graphics.Color.Transparent);
                    if (poprzedni == e.Position)
                    {
                        first = true;
                        edtName.Text = "";
                        edtEmail.Text = "";
                    }
                    else
                    {
                        lstData2.GetChildAt(e.Position).SetBackgroundColor(Android.Graphics.Color.DarkGray);

                        edtName.Text = txtName.Text;
                        edtName.Tag = e.Id;
                        edtEmail.Text = txtEmail.Text;
                    }

                    poprzedni = e.Position;
                }
            };
        }



        private void LoadData()
        {
            lstSource = db.selectTablePerson();
            var adapter = new ListViewAdapter(this, lstSource);
            lstData2.Adapter = adapter;
        }
    }
}