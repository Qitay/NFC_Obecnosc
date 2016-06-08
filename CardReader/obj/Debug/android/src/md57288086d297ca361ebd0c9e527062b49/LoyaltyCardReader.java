package md57288086d297ca361ebd0c9e527062b49;


public class LoyaltyCardReader
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.nfc.NfcAdapter.ReaderCallback
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onTagDiscovered:(Landroid/nfc/Tag;)V:GetOnTagDiscovered_Landroid_nfc_Tag_Handler:Android.Nfc.NfcAdapter/IReaderCallbackInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("CardReader.LoyaltyCardReader, CardReader, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", LoyaltyCardReader.class, __md_methods);
	}


	public LoyaltyCardReader () throws java.lang.Throwable
	{
		super ();
		if (getClass () == LoyaltyCardReader.class)
			mono.android.TypeManager.Activate ("CardReader.LoyaltyCardReader, CardReader, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onTagDiscovered (android.nfc.Tag p0)
	{
		n_onTagDiscovered (p0);
	}

	private native void n_onTagDiscovered (android.nfc.Tag p0);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
