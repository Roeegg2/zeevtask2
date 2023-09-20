using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App1
{
    class PersonAdapter : BaseAdapter<Person>
    {
        Android.Content.Context context;
        List<Person> objects;
        public PersonAdapter(Android.Content.Context context, System.Collections.Generic.List<Person> objects)
        {
            this.context = context;
            this.objects = objects;
        }
        public List<Person> GetList()
        {
            return this.objects;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override int Count
        {
            get { return this.objects.Count; }
        }
        public override Person this[int position]
        {
            get { return this.objects[position]; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Android.Views.LayoutInflater layoutInflater = ((ShowRecord)context).LayoutInflater;
            Android.Views.View view = layoutInflater.Inflate(Resource.Layout.personlistviewlayout, parent, false);

            TextView fname = view.FindViewById<TextView>(Resource.Id.fnameid);
            TextView lname = view.FindViewById<TextView>(Resource.Id.lnameid);
            TextView age = view.FindViewById<TextView>(Resource.Id.ageid);
            ImageView photo = view.FindViewById<ImageView>(Resource.Id.photoid);

            Person person = objects[position];
            if (person != null)
            {
                 photo.SetImageBitmap(BitmapUtils.Base64ToBitmap(person.Image));
                fname.Text = "" + person.Fname;
                lname.Text = person.Lname;
                age.Text = person.Age.ToString();
            }

            return view;
        }
    }
}