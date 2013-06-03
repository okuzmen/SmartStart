package com.body;

import java.io.FileNotFoundException;
import java.io.IOException;
import java.text.DecimalFormat;

import android.net.Uri;
import android.os.Bundle;
import android.os.Debug;
import android.provider.MediaStore;
import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import android.util.Log;
import android.view.Menu;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;

public class MainActivity extends Activity {
	ImageView mImage;
	Context mContext;
	Uri mUri;
	Bitmap bitmap;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		mContext = this;
		mUri = null;
		setContentView(R.layout.activity_main);

		Button mButtonShot = (Button) findViewById(R.id.buttonSnapshot);
		mImage = (ImageView) findViewById(R.id.previewImage);
		mButtonShot.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				final Intent launchIntent = new Intent(MainActivity.this,
						Capture.class);
				launchIntent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP
						| Intent.FLAG_ACTIVITY_SINGLE_TOP);
				startActivityForResult(launchIntent, 100);
			}
		});
	}

	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		// TODO Auto-generated method stub
		super.onActivityResult(requestCode, resultCode, data);
		if (resultCode == RESULT_OK) {
			RecycleImage(mImage);
			logHeap(Capture.class);
			mUri = data.getData();
	        
			try {
				bitmap = MediaStore.Images.Media.getBitmap(this.getContentResolver(), mUri);
			} catch (FileNotFoundException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			mImage.setImageDrawable(new BitmapDrawable(mContext.getResources(), bitmap));
		}
	}

	@Override
	protected void onRestoreInstanceState(Bundle savedInstanceState) {
		super.onRestoreInstanceState(savedInstanceState);
		// Read values from the "savedInstanceState"-object and put them in your
		// preview
	}

	@Override
	protected void onSaveInstanceState(Bundle outState) {
		// Save the values you need from your preview into "outState"-object
		super.onSaveInstanceState(outState);
		if (mUri != null) {
			mImage = (ImageView) findViewById(R.id.previewImage);
			mImage.setImageURI(mUri);
			mImage.invalidate();
		}
	}

	private void RecycleImage(ImageView imageView)
	{
		Drawable drawable = imageView.getDrawable();
		if (drawable instanceof BitmapDrawable) {
		    BitmapDrawable bitmapDrawable = (BitmapDrawable) drawable;
		    Bitmap bitmap = bitmapDrawable.getBitmap();
		    bitmap.recycle();
		}
		if (bitmap != null)
		bitmap.recycle();
	}
	
	@SuppressLint("UseValueOf")
	private void logHeap(Class clazz) {
	    Double allocated = new Double(Debug.getNativeHeapAllocatedSize())/new Double((1048576));
	    Double available = new Double(Debug.getNativeHeapSize())/1048576.0;
	    Double free = new Double(Debug.getNativeHeapFreeSize())/1048576.0;
	    DecimalFormat df = new DecimalFormat();
	    df.setMaximumFractionDigits(2);
	    df.setMinimumFractionDigits(2);

	    Log.d("SS", "debug. =================================");
	    Log.d("SS", "debug.heap native: allocated " + df.format(allocated) + "MB of " + df.format(available) + "MB (" + df.format(free) + "MB free) in [" + clazz.getName().replaceAll("com.myapp.android.","") + "]");
	    Log.d("SS", "debug.memory: allocated: " + df.format(new Double(Runtime.getRuntime().totalMemory()/1048576)) + "MB of " + df.format(new Double(Runtime.getRuntime().maxMemory()/1048576))+ "MB (" + df.format(new Double(Runtime.getRuntime().freeMemory()/1048576)) +"MB free)");
	    System.gc();
	    System.gc();	    
	    
	}
	
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	}
}
