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
    [Table("Persons")]
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public int age { get; set; }
        public string image { get; set; }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Fname
        {
            get { return fname; }
            set { fname = value; }
        }

        public string Lname
        {
            get { return lname; }
            set { lname = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public string Image
        {
            get { return image; }
            set { image = value; }
        }
        public Person()
        {
        }

        public Person(string fname, string lname, int age, string image)
        {

            this.fname = fname;
            this.lname = lname;
            this.age = age;
            this.image = image;
        }

        public void setPerson(string fname, string lname, int age)

        {
            this.fname = fname;
            this.lname = lname;
            this.age = age;
        }
    }
}