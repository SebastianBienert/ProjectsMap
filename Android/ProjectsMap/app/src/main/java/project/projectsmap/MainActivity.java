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

    //Button clickShowMap, clickShowFloor;
    Button clickSearchDevelopers, clickSave;
    Toast toast;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        //clickShowMap = (Button) findViewById(R.id.buttonShowMap);
        //clickShowFloor = (Button) findViewById(R.id.buttonShowFloor);
        clickSearchDevelopers = (Button) findViewById(R.id.buttonSearchDevelopers);
        clickSave = (Button) findViewById(R.id.buttonSaveTest);

        if(!isNetworkAvailable()){
            toast = Toast.makeText(this,"Brak internetu", Toast.LENGTH_SHORT);
            toast.show();
        }else{
            toast = Toast.makeText(this,"Jest internet", Toast.LENGTH_SHORT);
            toast.show();
        }

        /*clickShowMap.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(MainActivity.this, MapActivity.class);
                startActivity(intent);
            }
        });*/
        clickSearchDevelopers.setOnClickListener(new View.OnClickListener() {
            public void onClick(View view) {
                Intent intent = new Intent(MainActivity.this, SearchDevelopersActivity.class);
                startActivity(intent);
            }
        });
        /*clickShowFloor.setOnClickListener(new View.OnClickListener() {
            public void onClick(View view) {
                Intent intent = new Intent(MainActivity.this, SearchFloorActivity.class);
                startActivity(intent);
            }
        });*/
        clickSave.setOnClickListener(new View.OnClickListener() {
            public void onClick(View view) {
                Intent intent = new Intent(MainActivity.this, SaveToFileActivity.class);
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

