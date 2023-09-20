using Android.App;
using Android.Content;
using Android.Graphics;
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
    [Activity(Label = "ChangeElement")]
    public class ChangeElement : Activity, View.IOnClickListener
    {
        private Person personPtr;
        private ListView personlv;
        private ShowRecord sr;
        private SQLiteConnection db;
        private EditText fname, lname, age;
        private Button applyChanges, back, takepic;
        private string dbpath, strImage = null;
        private int index;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.changeelementlayout);

            fname = FindViewById<EditText>(Resource.Id.fname);
            lname = FindViewById<EditText>(Resource.Id.lname);
            age = FindViewById<EditText>(Resource.Id.age);
            applyChanges = FindViewById<Button>(Resource.Id.applychanges);
            back = FindViewById<Button>(Resource.Id.back);
            takepic = FindViewById<Button>(Resource.Id.takepic);

            applyChanges.SetOnClickListener(this);
            back.SetOnClickListener(this);
            takepic.SetOnClickListener(this);

            dbpath = Intent.GetStringExtra("dbpath");
            index = int.Parse(Intent.GetStringExtra("index"));

            db = new SQLiteConnection(dbpath);

        }
        public void OnClick(View v)
        {
            Person personPtr;
            string strFname, strLname;
            int intAge;

            if (v == takepic)
            {
                Intent intent = new Intent(Android.Provider.MediaStore.ActionImageCapture);
                StartActivityForResult(intent, 0);
            }
            else if (v == applyChanges)
            {
                strFname = fname.Text;
                strLname = lname.Text;
                intAge = int.Parse(age.Text);

                if (strImage == null)
                    MainActivity.alertDialogManager("ERROR", "You must take a picture!", this);
                
                else if (index >= 0)
                {
                    changePerson(ShowRecord.getAllPersons(db));
                    Toast.MakeText(this, "Changed person successfully!", ToastLength.Short).Show();
                }
                else // If the index is negative then we are adding a new person
                {
                    personPtr = new Person(strFname, strLname, intAge, strImage);
                    db.Insert(personPtr);
                    Toast.MakeText(this, "Added person successfully!", ToastLength.Short).Show();
                }
            }
            else if (v == back)
                Finish();
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == 0 && resultCode == Result.Ok)
            {
                // The camera activity has returned with a photo
                Bitmap photo = (Bitmap)data.Extras.Get("data");
                strImage = BitmapUtils.BitmapToBase64(photo);

            }
        }

        private void changePerson(List<Person> personList)
        {
            if (index >= personList.Count)
            {
                Toast.MakeText(this, "Index out of bounds!", ToastLength.Short).Show();
                return;
            }
            personList[index].Fname = fname.Text;
            personList[index].Lname = lname.Text;
            personList[index].Age = int.Parse(age.Text);
            db.Update(personList[index]);
        }
    }
}