package project.projectsmap;

import android.content.Context;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.TextView;

import java.util.ArrayList;

public class MainActivity extends AppCompatActivity {

    Button clickAllDeveloper;
    Button clickSerachDeveloper;
    Button clickSerachDeveloperAfterTechnology;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        clickAllDeveloper = (Button) findViewById(R.id.buttonAllDevelopers);

        clickAllDeveloper.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(MainActivity.this, AllDevelopers.class);
                startActivity(intent);
            }
        });


        clickSerachDeveloper = (Button) findViewById(R.id.buttonSearchDeveloper);

        clickSerachDeveloper.setOnClickListener(new View.OnClickListener() {
            public void onClick(View view) {
                Intent intent = new Intent(MainActivity.this, SerachDeveloper.class);
                startActivity(intent);
            }
        });

        clickSerachDeveloperAfterTechnology = (Button) findViewById(R.id.buttonSearchDeveloperAfterTechnology);
        clickSerachDeveloperAfterTechnology.setOnClickListener(new View.OnClickListener() {
            public void onClick(View view) {
                Intent intent = new Intent(MainActivity.this, SearchDevelopers.class);
                startActivity(intent);
            }
        });


    }

}

