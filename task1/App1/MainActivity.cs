using Android.App;
using Android.Widget;
using Android.OS;
using SQLite;
using System.Collections.Generic;
using Android.Views;
using Android.Content;
using AndroidX.Annotations;
using System.Linq;


namespace App1
{
    [Activity(Label = "SqliteIntro1", MainLauncher = true)]
    public class MainActivity : Activity, View.IOnClickListener
    {
        private SQLiteConnection db;

        private Button changeElAct, showRecordsAct;
        private EditText index;

        private string dbpath;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            changeElAct = FindViewById<Button>(Resource.Id.changeel);
            showRecordsAct = FindViewById<Button>(Resource.Id.showrecord);
            index = FindViewById<EditText>(Resource.Id.index);

            changeElAct.SetOnClickListener(this);
            showRecordsAct.SetOnClickListener(this);

            dbpath = System.IO.Path.Combine(
                                   System.Environment.GetFolderPath(
                                   System.Environment.SpecialFolder.Personal),
                                  "persondb");

            db = new SQLiteConnection(dbpath);

            db.CreateTable<Person>();
        }

        public void OnClick(View v)
        {
            Intent intent;

            if (v == changeElAct)
            {
                if (string.IsNullOrEmpty(index.Text))
                {
                    alertDialogManager("ERROR", "if you want to access this activity, you must enter a valid index number", this);
                    return;
                }
                intent = new Intent(this, typeof(ChangeElement));
                intent.PutExtra("dbpath", dbpath);
                intent.PutExtra("index", index.Text);
                StartActivity(intent);
            }
            else if (v == showRecordsAct)
            {
                intent = new Intent(this, typeof(ShowRecord));
                intent.PutExtra("dbpath", dbpath);
                StartActivity(intent);
            }
        }

        public static void alertDialogManager(string msgHeader, string message, Context context)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(context);
            builder.SetTitle(msgHeader);
            builder.SetMessage(message);

            AlertDialog dialog = builder.Create();
            dialog.Show();
        }
    }
}