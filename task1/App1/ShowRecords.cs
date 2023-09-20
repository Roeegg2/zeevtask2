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
    public class ShowRecord : Activity, View.IOnClickListener, ListView.IOnItemLongClickListener
    {
        SQLiteConnection db;
        private List<Person> personlist;
        private ListView personlv;
        private string dbpath;
        private PersonAdapter adapter;
        private Button back;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.showrecordlayout);

            personlv = FindViewById<ListView>(Resource.Id.listview);
            back = FindViewById<Button>(Resource.Id.back);

            dbpath = Intent.GetStringExtra("dbpath");
            back.SetOnClickListener(this);
            personlv.OnItemLongClickListener = this;


            db = new SQLiteConnection(dbpath);
            personlist = getAllPersons(db);

            adapter = new PersonAdapter(this, personlist);
            personlv.Adapter = adapter;
        }

        public void OnClick(View v)
        {
            if (v == back) // i know this check is redundant but i'm keeping it for consistency
                Finish();
        }
        public bool OnItemLongClick(AdapterView parent, View view, int position, long id)
        {
            int personIdToDelete = personlist[position].Id;
            db.Delete<Person>(personIdToDelete);

            personlist.RemoveAt(position);
            adapter.NotifyDataSetChanged();
            return true;

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
    }
}