package com.body;

import java.io.File;

import android.net.Uri;
import android.os.Bundle;
import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.view.Menu;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.Toast;
import android.provider.MediaStore;
import android.os.Environment;

public class MainActivity extends Activity 
{
	private static final int PHOTO_INTENT_REQUEST_CODE = 100;
	
	ImageView mImage;
	Context mContext;
	Uri mUri;
	
    @Override
    protected void onCreate(Bundle savedInstanceState) 
    {
        super.onCreate(savedInstanceState);
        mContext = this;
        mUri = null;
        setContentView(R.layout.activity_main);
        
        Button mButtonShot = (Button) findViewById(R.id.buttonSnapshot);
        mImage = (ImageView) findViewById(R.id.previewImage);
		mButtonShot.setOnClickListener(new View.OnClickListener() 
		{
			@Override
			public void onClick(View v) 
			{
				mUri = generateFileUri();
				if (mUri == null) 
				{
					Toast.makeText(mContext, "SD card not available", Toast.LENGTH_LONG).show();
					return;
				}

				Intent intent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
				intent.putExtra(MediaStore.EXTRA_OUTPUT, mUri);
				startActivityForResult(intent, PHOTO_INTENT_REQUEST_CODE);
			}
		});			
    }
    
	private Uri generateFileUri() 
	{
		if (!Environment.getExternalStorageState().equals(Environment.MEDIA_MOUNTED))
			return null;

		File path = new File (Environment.getExternalStorageDirectory(), "CameraTest");
		if (! path.exists())
		{
			if (! path.mkdirs())
			{
				return null;
			}
		}
			
		String timeStamp = String.valueOf(System.currentTimeMillis());
		File newFile = new File(path.getPath() + File.separator + timeStamp + ".jpg");
		return Uri.fromFile(newFile);
	}
	
	@Override
	protected void onRestoreInstanceState(Bundle savedInstanceState) {
	    super.onRestoreInstanceState(savedInstanceState);
	    // Read values from the "savedInstanceState"-object and put them in your preview
	}

	@Override
	protected void onSaveInstanceState(Bundle outState) {
	    // Save the values you need from your preview into "outState"-object
	    super.onSaveInstanceState(outState);
	    if(mUri != null)
	    {
	    	mImage = (ImageView) findViewById(R.id.previewImage);
	    	mImage.setImageURI(mUri);
	    	mImage.invalidate();
	    }
	}

    @Override
    public boolean onCreateOptionsMenu(Menu menu) 
    {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    }
    
	@Override
	public void onActivityResult (int requestCode, int resultCode, Intent data) 
	{
		if (requestCode == PHOTO_INTENT_REQUEST_CODE) 
		{
			if (resultCode == RESULT_OK) 
			{
				//Log.w ("MY", "bitmap: " + data.getExtras().get("data"));
				Toast.makeText(mContext, mUri.toString(), Toast.LENGTH_LONG).show();
				mImage.setImageURI(mUri);
			}
			else if (resultCode == RESULT_CANCELED) 
				Toast.makeText(mContext, "Capture cancelled", Toast.LENGTH_LONG).show();
			else 
				Toast.makeText(mContext, "Capture failed", Toast.LENGTH_LONG).show();
		}
	}
}
