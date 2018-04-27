package project.projectsmap;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.ListView;

import java.util.ArrayList;

/**
 * Created by Mateusz on 14.04.2018.
 */

public class ActivityShowProjectDevelopers extends AppCompatActivity {

    ListView listDevelopers;
    CustomAdapter adapter;
    ArrayList<Developer> arrayDevelopers = new ArrayList<Developer>();

    public void setArrayDevelopers(ArrayList<Developer> arrayDevelopers) {
        this.arrayDevelopers = arrayDevelopers;
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_show_project_developers);
        adapter = new CustomAdapter(this);
        listDevelopers = (ListView) findViewById(R.id.listDevelopers);
        listDevelopers.setAdapter(adapter);
        listDevelopers = (ListView) findViewById(R.id.listDevelopers);
        for(int i = 0; i < arrayDevelopers.size(); i++){
            adapter.list.add(arrayDevelopers.get(i).description());  // potem zmienić na długi i krótki opis
        }
        adapter.notifyDataSetChanged();
    }
}
