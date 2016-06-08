package md558b710577273d55ff1148f91bdd1c90d;


public class MainActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer,
		android.nfc.NfcAdapter.ReaderCallback
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onTagDiscovered:(Landroid/nfc/Tag;)V:GetOnTagDiscovered_Landroid_nfc_Tag_Handler:Android.Nfc.NfcAdapter/IReaderCallbackInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("Obecnosc.MainActivity, Obecnosc, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MainActivity.class, __md_methods);
	}


	public MainActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MainActivity.class)
			mono.android.TypeManager.Activate ("Obecnosc.MainActivity, Obecnosc, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


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
