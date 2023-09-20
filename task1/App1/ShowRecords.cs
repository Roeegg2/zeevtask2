using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App1
{
    [Activity(Label = "ShowRecord")]
    public class ShowRecord : Activity, View.IOnClickListener, ListView.IOnItemClickListener
    {
        SQLiteConnection db;
        private List<Person> personlist;
        private ListView personlv;
        private string dbpath;
        private Button back;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.showrecordlayout);

            personlv = FindViewById<ListView>(Resource.Id.listview);
            back = FindViewById<Button>(Resource.Id.back);

            dbpath = Intent.GetStringExtra("dbpath");
            back.SetOnClickListener(this);
            personlv.OnItemClickListener = this;


            db = new SQLiteConnection(dbpath);
            personlist = getAllPersons(db);
            showAllPersons(personlist);
        }

        public void OnClick(View v)
        {
            if (v == back) // i know this check is redundant but i'm keeping it for consistency
                Finish();
        }
        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            // add if()
            db.Delete<Person>(personlist[position].Id);
            Toast.MakeText(this, "Deleted person successfully!", ToastLength.Short).Show();
            personlist = getAllPersons(db);
            showAllPersons(personlist);
        }

        // made this public since ChangeElement.cs needs it too
        public static List<Person> getAllPersons(SQLiteConnection db)
        {
            string strSql = string.Format("SELECT * FROM persons");
            var persons = db.Query<Person>(strSql);

            List<Person> personsList = new List<Person>();
            if (persons.Count > 0)
            {
                foreach (var item in persons)
                {
                    personsList.Add(item);
                }
            }
            return personsList;
        }

        private void showAllPersons(List<Person> personsList)
        {
            var adapter = new PersonAdapter(this, personsList);
            personlv.Adapter = adapter;
        }
    }
}