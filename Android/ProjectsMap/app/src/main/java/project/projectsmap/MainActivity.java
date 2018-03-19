package project.projectsmap;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import java.io.BufferedReader;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.io.Writer;
import java.util.ArrayList;

public class MainActivity extends AppCompatActivity {

    Button clickAllDeveloper;
    Button clickSerachDeveloper;
    Button clickSerachDeveloperAfterTechnology,clickSave;
    //Button clickPaint;
    Toast toast;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        clickAllDeveloper = (Button) findViewById(R.id.buttonAllDevelopers);
        clickSerachDeveloper = (Button) findViewById(R.id.buttonSearchDeveloper);
        clickSerachDeveloperAfterTechnology = (Button) findViewById(R.id.buttonSearchDeveloperAfterTechnology);
        //clickPaint = (Button) findViewById(R.id.buttonPaint);
        clickSave = (Button) findViewById(R.id.buttonSaveTest);

        if(!isNetworkAvailable()){
            toast = Toast.makeText(this,"Brak internetu", Toast.LENGTH_SHORT);
            toast.show();
        }else{
            toast = Toast.makeText(this,"Jest internet", Toast.LENGTH_SHORT);
            toast.show();
        }

        clickAllDeveloper.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(MainActivity.this, AllDevelopers.class);
                startActivity(intent);
            }
        });
        clickSerachDeveloper.setOnClickListener(new View.OnClickListener() {
            public void onClick(View view) {
                Intent intent = new Intent(MainActivity.this, SerachDeveloper.class);
                startActivity(intent);
            }
        });
        clickSerachDeveloperAfterTechnology.setOnClickListener(new View.OnClickListener() {
            public void onClick(View view) {
                Intent intent = new Intent(MainActivity.this, SearchDevelopers.class);
                startActivity(intent);
            }
        });
        /*clickPaint.setOnClickListener(new View.OnClickListener() {
            public void onClick(View view) {
                Intent intent = new Intent(MainActivity.this, PaintExample.class);
                startActivity(intent);
            }
        });*/
        clickSave.setOnClickListener(new View.OnClickListener() {
            public void onClick(View view) {
                Intent intent = new Intent(MainActivity.this, SaveToFile.class);
                startActivity(intent);
            }
        });
    }
    private boolean isNetworkAvailable() {
        ConnectivityManager connectivityManager
                = (ConnectivityManager) getSystemService(Context.CONNECTIVITY_SERVICE);
        NetworkInfo activeNetworkInfo = connectivityManager.getActiveNetworkInfo();
        return activeNetworkInfo != null && activeNetworkInfo.isConnected();
    }
}

