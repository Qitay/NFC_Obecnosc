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


using System.IO;
using Android.Nfc;
using Android.Nfc.Tech;
using System.Security.Cryptography;

namespace Obecnosc
{
    public class CallbackClass : Java.Lang.Object, NfcAdapter.IReaderCallback
    {
        // "OK" status word sent in response to SELECT AID command (0x9000)
        private static readonly byte[] SELECT_OK_SW = { (byte)0x90, (byte)0x00 };

        // Weak reference to prevent retain loop. mAccountCallback is responsible for exiting
        // foreground mode before it becomes invalid (e.g. during onPause() or onStop()).
        private WeakReference<InfoCallback> callback;

        public interface InfoCallback
        {
            void InfoRecieved(string account);
        }

        public CallbackClass(WeakReference<InfoCallback> ourCallback)
        {
            callback = ourCallback;
        }


        /**
	     * Callback when a new tag is discovered by the system.
	     *
	     * <p>Communication with the card should take place here.
	     *
	     * @param tag Discovered tag
	     */
        public void OnTagDiscovered(Android.Nfc.Tag tag)
        {
            Console.WriteLine("New tag discovered");
            IsoDep isoDep = IsoDep.Get(tag);
            if (isoDep != null)
            {
                try
                {
                    // Connect to the remote NFC device
                    isoDep.Connect();

                    byte[] command = new byte[]{
                            (byte) 0x00, /* CLA = 00 (first interindustry command set) */
                            (byte) 0xA4, /* INS = A4 (SELECT) */
                            (byte) 0x04, /* P1  = 04 (select file by DF name) */
                            (byte) 0x0C, /* P2  = 0C (first or only file; no FCI) */
                            (byte) 0x07, /* Lc  = 7  (data/AID has 7 bytes) */
                                            /* AID = D6 16 00 00 30 01 01: */
                            (byte) 0xD6, (byte) 0x16, (byte) 0x00, (byte) 0x00,
                            (byte) 0x30, (byte) 0x01, (byte) 0x01,
                            (byte) 0x00
                    };
                    byte[] result = isoDep.Transceive(command);
                    // If AID is successfully selected, 0x9000 is returned as the status word (last 2
                    // bytes of the result) by convention. Everything before the status word is
                    // optional payload, which is used here to hold the account number.
                    int resultLength = result.Length;
                    byte[] statusWord = { result[resultLength - 2], result[resultLength - 1] };
                    byte[] payload = new byte[resultLength - 2];
                    Array.Copy(result, payload, resultLength - 2);
                    bool arrayEquals = SELECT_OK_SW.Length == statusWord.Length;

                    for (int i = 0; i < SELECT_OK_SW.Length && i < statusWord.Length && arrayEquals; i++)
                    {
                        arrayEquals = (SELECT_OK_SW[i] == statusWord[i]);
                        if (!arrayEquals)
                            break;
                    }
                    if (arrayEquals)
                    {
                        // The remote NFC device will immediately respond with its stored account number
                        string info = System.Text.Encoding.UTF8.GetString(result);
                        Console.WriteLine("Received: " + info);
                        // Inform CardReaderFragment of received account number
                        InfoCallback ourCallback;
                        if (callback.TryGetTarget(out ourCallback))
                        {
                            ourCallback.InfoRecieved(info);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error communicating with card: " + e.Message);
                }
            }
        }
        /**
     	* Utility class to convert a byte array to a hexadecimal string.
     	*
     	* @param bytes Bytes to convert
     	* @return String, containing hexadecimal representation.
     	*/
        public static string ByteArrayToHexString(byte[] bytes)
        {
            String s = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                s += bytes[i].ToString("X2");
            }
            return s;
        }

        /**
	     * Utility class to convert a hexadecimal string to a byte string.
	     *
	     * <p>Behavior with input strings containing non-hexadecimal characters is undefined.
	     *
	     * @param s String containing hexadecimal characters to convert
	     * @return Byte array generated from input
	     */
        private static byte[] HexStringToByteArray(string s)
        {
            int len = s.Length;
            if (len % 2 == 1)
            {
                throw new ArgumentException("Hex string must have even number of characters");
            }
            byte[] data = new byte[len / 2]; //Allocate 1 byte per 2 hex characters
            for (int i = 0; i < len; i += 2)
            {
                ushort val, val2;
                // Convert each chatacter into an unsigned integer (base-16)
                try
                {
                    val = (ushort)Convert.ToInt32(s[i].ToString() + "0", 16);
                    val2 = (ushort)Convert.ToInt32("0" + s[i + 1].ToString(), 16);
                }
                catch (Exception)
                {
                    continue;
                }


                data[i / 2] = (byte)(val + val2);
            }
            return data;
        }
    }
}