package com.body;

import java.io.File;
import java.io.IOException;
import java.io.OutputStream;

import android.net.Uri;
import android.net.sip.SipAudioCall.Listener;
import android.os.Bundle;
import android.os.Environment;
import android.provider.MediaStore;
import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.hardware.Camera;
import android.hardware.Camera.PictureCallback;
import android.util.Log;
import android.view.KeyEvent;
import android.view.SurfaceHolder;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.FrameLayout;
import android.widget.ImageButton;
import android.widget.LinearLayout;
import android.widget.Toast;

public class Capture extends Activity {

	ImageButton capture, recapture, save;
	Boolean inited = false;
	SmartCamera preview;
	Camera mCamera;
	FrameLayout mFrame;
	LinearLayout mButtons;
	Context mContext;
	Uri fileUri;
	Intent intent;
	byte[] pictureData;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_capture);
		Subscribe();
	}

	private void Subscribe() {
		intent = getIntent();
		mContext = this;
		mButtons = (LinearLayout) findViewById(R.id.buttons);
		capture = (ImageButton) findViewById(R.id.capture);
		OnClickListener listener = new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				mCamera.takePicture(null, null, null, mPictureCallback);
			}
		};
		capture.setOnClickListener(listener);

		recapture = (ImageButton) findViewById(R.id.recapture);
		recapture.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				hideConfirm();
				mCamera.startPreview();
				pictureData = null;
			}
		});

		save = (ImageButton) findViewById(R.id.finish);
		save.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				Uri pictureFile;

				if (intent.hasExtra(MediaStore.EXTRA_OUTPUT)) {
					pictureFile = (Uri) intent.getExtras().getParcelable(
							MediaStore.EXTRA_OUTPUT);
				} else
					pictureFile = generateFile();

				try {
					savePhotoInFile(pictureData, pictureFile);

				} catch (Exception e) {
					setResult(2, intent);
				}
				finish();
				return;
			}
		});
	}

	@Override
	public void onDestroy() {
		finish();
		super.onDestroy();
	}

	private Camera openCamera() {

		Camera cam = null;
		if (Camera.getNumberOfCameras() > 0) {
			try {
				cam = Camera.open(0);
			} catch (Exception exc) {
				//
			}
		}

		return cam;
	}

	private PictureCallback mPictureCallback = new PictureCallback() {

		@Override
		public void onPictureTaken(byte[] data, Camera camera) {

			pictureData = data;
			showConfirm();
		}
	};

	private void showConfirm() {
		preview.isCaptured = true;
		capture.setVisibility(View.INVISIBLE);
		mButtons.setVisibility(View.VISIBLE);
	}

	private void hideConfirm() {
		preview.isCaptured = false;
		preview.setCameraDisplayOrientation();
		mButtons.setVisibility(View.INVISIBLE);
		capture.setVisibility(View.VISIBLE);
	}

	private void savePhotoInFile(byte[] data, Uri pictureFile) throws Exception {

		if (pictureFile == null)
			throw new Exception();

		OutputStream os = getContentResolver().openOutputStream(pictureFile);
		os.write(data);
		os.close();
		intent.setData(pictureFile);
		setResult(RESULT_OK, intent);
	}

	@Override
	protected void onPause() {
		super.onPause();
		if (mCamera != null) {

			mFrame.removeView(preview);
			mCamera = null;
			preview = null;
		}
	}

	@Override
	protected void onResume() {
		super.onResume();
		mCamera = openCamera();
		if (mCamera == null) {
			Toast.makeText(this, "Opening camera failed", Toast.LENGTH_LONG)
					.show();
			return;
		}

		preview = new SmartCamera(this, mCamera);
		mFrame = (FrameLayout) findViewById(R.id.layout);
		mFrame.addView(preview, 0);
	}

	@Override
	public boolean onKeyDown(int keyCode, KeyEvent event) {
		if ((keyCode == KeyEvent.KEYCODE_BACK)) {
			finish();
		}
		return super.onKeyDown(keyCode, event);
	}

	private Uri generateFile() {
		
		if (!Environment.getExternalStorageState().equals(
				Environment.MEDIA_MOUNTED))
			return null;

		File path = new File(Environment.getExternalStorageDirectory(),
				"CameraTest");
		if (!path.exists()) {
			if (!path.mkdirs()) {
				return null;
			}
		}

		String timeStamp = String.valueOf(System.currentTimeMillis());
		File newFile = new File(path.getPath() + File.separator + timeStamp
				+ ".jpg");
		return Uri.fromFile(newFile);
	}

}
