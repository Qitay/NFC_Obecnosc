using System;
using System.Data;
using System.IO;
using Android.App;
using Android.Widget;
using Android.OS;
using Mono.Data.Sqlite;
using System.Threading.Tasks;
using SQLite;


using Android.OS;
using Android.Views;
using Android.Runtime;
using Android.Content;
using Android.Nfc;
using Android.Nfc.Tech;


namespace Obecnosc
{


    [Activity(Label = "BAZA DANYCH", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, NfcAdapter.IReaderCallback
    {
        TextView txtResult;
        EditText editText;
        public static SqliteConnection connection;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // assign variables to the used view widgets
            var btnCreate = FindViewById<Button>(Resource.Id.button1);
            var btnCreate2 = FindViewById<Button>(Resource.Id.button2);
            var btnCreate3 = FindViewById<Button>(Resource.Id.button3);
            var btnCreate4 = FindViewById<Button>(Resource.Id.button4);
            var btnCreate5 = FindViewById<Button>(Resource.Id.button5);
            txtResult = FindViewById<TextView>(Resource.Id.textView1);
            editText = FindViewById<EditText>(Resource.Id.editText);
            // get the context of the button for use with Toast
            var context = btnCreate.Context;
            var name= "";
            // create and test the database connection
            editText.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) => {
                name = e.Text.ToString();
            };
            // create the event for the button
            btnCreate.Click += async delegate
            {
                txtResult.Text = await createTable();
            };
            btnCreate2.Click += async delegate
            {
                txtResult.Text = await crateDB();
            };
            btnCreate3.Click += async delegate
            {
                txtResult.Text = await insert(name);
            };
            btnCreate4.Click += async delegate
            {
                txtResult.Text = await show();
            };
            btnCreate5.Click += async delegate
            {
                txtResult.Text = await delete();
            };
            //EnableReaderMode();
        }

        //protected override void OnPause()
        //{
        //    base.OnPause();
        //    DisableReaderMode();
        //}

        //protected override void OnResume()
        //{
        //    base.OnResume();
        //    EnableReaderMode();
        //}

        //private void EnableReaderMode()
        //{
        //    //Log.Info(TAG, "Enabling reader mode");
        //    Activity activity = this.Activity;
        //    NfcAdapter nfc = NfcAdapter.GetDefaultAdapter(activity);
        //    if (nfc != null)
        //    {
        //        nfc.EnableReaderMode(activity, mLoyaltyCardReader, READER_FLAGS, null);
        //    }
        //}

        //private void DisableReaderMode()
        //{
        //    //Log.Info(TAG, "Enabling reader mode");
        //    Activity activity = this.Activity;
        //    NfcAdapter nfc = NfcAdapter.GetDefaultAdapter(activity);
        //    if (nfc != null)
        //    {
        //        nfc.DisableReaderMode(activity);
        //    }
        //}

        public void OnTagDiscovered(Android.Nfc.Tag tag)
        {
            txtResult = FindViewById<TextView>(Resource.Id.textView1);
            txtResult.Text = "aaaa";
            IsoDep isoDep = IsoDep.Get(tag);
            if (isoDep != null)
            {
                txtResult.Text = "aaaa";
            }
        }

        [Table("People")]
        public class People
        {
            [PrimaryKey, AutoIncrement, Column("id")]
            public int ID { get; set; }
            public string Imie { get; set; }
            public string nazwisko { get; set; }

        }

        private async Task<string> crateDB()
        {
            var output = "";
            output += "Create Database";
            string DBpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "my.db3");
            var db = new SQLiteConnection(DBpath);
            output += "DB createt";
            return output;
        }

        private async Task<string> createTable()
        {
            // create a connection string for the database
            try
            {
                string DBpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "my.db3");
                var db = new SQLiteConnection(DBpath);
                db.CreateTable<People>();
                string result = "table created";
                return result;
            }
            catch(Exception ex)
            {
                return "Error:" + ex.Message;
            }
            
        }

        private async Task<string> insert(string Imie)
        {
            try
            {
                string DBpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "my.db3");
                var db = new SQLiteConnection(DBpath);
                People item = new People();
                item.Imie = Imie;
                item.nazwisko = "Duda";
                db.Insert(item);
                return "added";
            }
            catch(Exception ex)
            {
                return "error" + ex.Message;
            }
        }

        private async Task<string> show()
        {
            string DBpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "my.db3");
            var db = new SQLiteConnection(DBpath);
            string output = "";
            var table = db.Table<People>();
            foreach(var item in table)
            {
                output += "\n" + item.ID + " " + item.Imie + " " + item.nazwisko;
            }
            return output;
        }

        private async Task<string> delete()
        {
            string DBpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "my.db3");
            var db = new SQLiteConnection(DBpath);
            db.DeleteAll<People>();
            return "delete";
        }
    }
}

