//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Util;
//using Android.Views;
//using Android.Widget;

//using Android.Nfc;
//using Android.Nfc.Tech;
//using System.Security.Cryptography;

//namespace Obecnosc
//{
//    public class ReaderFragment : Fragment
//    {
        
//        // Recommend NfcAdapter flags for reading from other Android devices. Indicates that this
//        // activity is interested in NFC-A devices (including other Android devices), and that the
//        // system should not check for the presence of NDEF-formatted data (e.g. Android Beam).
//        public NfcReaderFlags READER_FLAGS = NfcReaderFlags.NfcA | NfcReaderFlags.SkipNdefCheck;
//        public CallbackClass mLoyaltyCardReader;
//        private TextView mAccountField;


//        public override void OnCreate(Bundle savedInstanceState)
//        {
//            base.OnCreate(savedInstanceState);
//        }

//        public override View OnCreateView(LayoutInflater inflater, ViewGroup container,
//            Bundle savedInstanceState)
//        {
//            // Inflate the layout for this fragment
//            View v = inflater.Inflate(Resource.Layout.main_fragment, container, false);
//            if (v != null)
//            {
//                mAccountField = (TextView)v.FindViewById(Resource.Id.card_account_field);
//                mAccountField.Text = "Waiting...";

//                mLoyaltyCardReader = new CallbackClass(new WeakReference<CallbackClass.AccountCallback>(this));

//                // Disable Android Beam and register our card reader callback
//                EnableReaderMode();
//            }
//            return v;
//        }

//        public override void OnPause()
//        {
//            base.OnPause();
//            DisableReaderMode();
//        }

//        public override void OnResume()
//        {
//            base.OnResume();
//            EnableReaderMode();
//        }

//        private void EnableReaderMode()
//        {

//            Log.Info(TAG, "Enabling reader mode");
//            Activity activity = this.Activity;
//            NfcAdapter nfc = NfcAdapter.GetDefaultAdapter(activity);
//            if (nfc != null)
//            {
//                nfc.EnableReaderMode(activity, mLoyaltyCardReader, READER_FLAGS, null);
//            }
//        }

//        private void DisableReaderMode()
//        {
//            Log.Info(TAG, "Enabling reader mode");
//            Activity activity = this.Activity;
//            NfcAdapter nfc = NfcAdapter.GetDefaultAdapter(activity);
//            if (nfc != null)
//            {
//                nfc.DisableReaderMode(activity);
//            }
//        }


//        #region AccountCallback implementation
//        // This callback is run on a background thread, but updates to UI elements must be performed
//        // on the UI thread.
//        public void OnAccountRecieved(string account)
//        {
//            this.Activity.RunOnUiThread(() => mAccountField.Text = account);
//        }
//    }