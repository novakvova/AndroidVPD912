package com.example.shop;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.util.Base64;
import android.view.View;
import android.widget.ImageView;

import com.bumptech.glide.Glide;
import com.bumptech.glide.request.RequestOptions;
import com.example.shop.application.HomeApplication;

public class RegisterActivity extends AppCompatActivity {

    int SELECT_CROPPER = 300;
    String base64;
    public ImageView userimage;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_register);
        userimage=findViewById(R.id.userimage);

    }

    public void handleSelectImageClick(View view) {
        Intent intent = new Intent(this, ChangeImageActivity.class);
        startActivityForResult(intent, SELECT_CROPPER);
    }

    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);

        if (resultCode == SELECT_CROPPER) {
            base64 = data.getStringExtra("base64");
            byte[] imageByteArray = Base64.decode(base64, Base64.DEFAULT);
            Glide.with(HomeApplication.getInstance())
                    .load(imageByteArray)
                    .apply(new RequestOptions().override(600,300))
                    .into(userimage);
        }
    }
}