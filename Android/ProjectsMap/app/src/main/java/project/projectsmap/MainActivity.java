package project.projectsmap;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Color;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.CompoundButton;
import android.widget.ListView;
import android.widget.Switch;
import android.widget.TextView;
import android.widget.Toast;

import java.io.BufferedReader;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.io.Serializable;
import java.io.Writer;
import java.util.ArrayList;

public class MainActivity extends AppCompatActivity implements Serializable {

    Button clickShowMap, clickShowFloor;
    Button clickSearchDevelopers, clickSave;
    Button clickSearchProjects;
    Button clickSynchronization;
    //boolean onlineWork;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        final String token = getIntent().getExtras().getString("token");

        clickShowMap = (Button) findViewById(R.id.buttonShowMap);
        clickShowFloor = (Button) findViewById(R.id.buttonShowFloor);
        clickSearchDevelopers = (Button) findViewById(R.id.buttonSearchDevelopers);
        clickSearchProjects = (Button) findViewById(R.id.buttonSearchProjects);
        clickSave = (Button) findViewById(R.id.buttonSaveTest);
        clickSynchronization = (Button) findViewById(R.id.buttonSynchronization);
        clickShowMap.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(MainActivity.this, ShowMapActivity.class);
                intent.putExtra("token", token);
                intent.putExtra("isOnline", GlobalVariable.getOnlineWork());
                startActivity(intent);
            }
        });
        clickSearchDevelopers.setOnClickListener(new View.OnClickListener() {
            public void onClick(View view) {
                Intent intent = new Intent(MainActivity.this, SearchDevelopersActivity.class);
                intent.putExtra("token", token);
                intent.putExtra("isOnline", GlobalVariable.getOnlineWork());
                startActivity(intent);
            }
        });
        clickSearchProjects.setOnClickListener(new View.OnClickListener() {
            public void onClick(View view) {
                Intent intent = new Intent(MainActivity.this, SearchProjectsActivity.class);
                intent.putExtra("token", token);
                intent.putExtra("isOnline", GlobalVariable.getOnlineWork());
                startActivity(intent);
            }
        });
        clickShowFloor.setOnClickListener(new View.OnClickListener() {
            public void onClick(View view) {
                Intent intent = new Intent(MainActivity.this, SearchFloorActivity.class);
                intent.putExtra("token", token);
                startActivity(intent);
            }
        });
        clickSave.setOnClickListener(new View.OnClickListener() {
            public void onClick(View view) {
                Intent intent = new Intent(MainActivity.this, SaveToFileActivity.class);
                intent.putExtra("token", token);
                startActivity(intent);
            }
        });
        clickSynchronization.setOnClickListener(new View.OnClickListener() {
            public void onClick(View view) {
                Intent intent = new Intent(MainActivity.this, Synchronization.class);
                intent.putExtra("token", token);
                intent.putExtra("work", GlobalVariable.getOnlineWork());
                startActivity(intent);
            }
        });
        if(!isNetworkAvailable()){
            GlobalVariable.setOnlineWork(false);
        }
    }
    private boolean isNetworkAvailable() {
        ConnectivityManager connectivityManager
                = (ConnectivityManager) getSystemService(Context.CONNECTIVITY_SERVICE);
        NetworkInfo activeNetworkInfo = connectivityManager.getActiveNetworkInfo();
        return activeNetworkInfo != null && activeNetworkInfo.isConnected();
    }
}

