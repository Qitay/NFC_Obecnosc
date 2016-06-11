using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using Android.Nfc;
using Android.Nfc.Tech;
using System.Security.Cryptography;

namespace Obecnosc
{
    public class ReaderFragment : Fragment, CallbackClass.InfoCallback
    {
        // Recommend NfcAdapter flags for reading from other Android devices. Indicates that this
        // activity is interested in NFC-A devices (including other Android devices), and that the
        // system should not check for the presence of NDEF-formatted data (e.g. Android Beam).
        public NfcReaderFlags READER_FLAGS = NfcReaderFlags.NfcA | NfcReaderFlags.SkipNdefCheck;
        public CallbackClass callbackClass;

        private TextView Field;


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.layout1, container, false);
            if (v != null)
            {
                Field = (TextView)v.FindViewById(Resource.Id.textView2);
                Field.Text = "Waiting...";

                callbackClass = new CallbackClass(new WeakReference<CallbackClass.InfoCallback>(this));

                // Disable Android Beam and register our card reader callback
                EnableReaderMode();
            }
            return v;
        }

        public override void OnPause()
        {
            base.OnPause();
            DisableReaderMode();
        }

        public override void OnResume()
        {
            base.OnResume();
            EnableReaderMode();
        }

        private void EnableReaderMode()
        {
            Console.WriteLine("Enabling reader mode");
            Field.Text = "Waiting...";
            Activity activity = this.Activity;
            NfcAdapter nfc = NfcAdapter.GetDefaultAdapter(activity);
            if (nfc != null)
            {
                nfc.EnableReaderMode(activity, callbackClass, READER_FLAGS, null);
            }
        }

        private void DisableReaderMode()
        {
            Console.WriteLine("Disabling reader mode");
            Field.Text = "Waiting...";
            Activity activity = this.Activity;
            NfcAdapter nfc = NfcAdapter.GetDefaultAdapter(activity);
            if (nfc != null)
            {
                nfc.DisableReaderMode(activity);
            }
        }

        #region AccountCallback implementation
        // This callback is run on a background thread, but updates to UI elements must be performed
        // on the UI thread.
        public void InfoRecieved(string account)
        {
            this.Activity.RunOnUiThread(() => Field.Text = account);
        }
        #endregion
    }
}